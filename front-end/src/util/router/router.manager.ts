import VueRouter from 'vue-router';
import { RouterManagerBuilder } from './router.manager.builder';
import { RouterConfig, RouterPath, RouterPathType } from './router.path';
import { BroadcastEventBus, BroadcastEvent } from '../broadcast/broadcast.event-bus';
import { BaseError, RedirectError } from '../error/error';
import { AutenticacaoService } from '../service/autenticacao/autenticacao.service';


export class RouterManager {

    private static router: VueRouter;
    private static config: Array<RouterConfig>;

    public static generateRouter(config: Array<RouterConfig>) {
        this.config = config;
        RouterManager.router = new VueRouter(RouterManagerBuilder.buildRouteOptions(config));
        RouterManager.router.beforeEach(RouterManager.beforeEach);
        return RouterManager.router;
    }

    private static beforeEach(to, from, next) {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (RouterManager.isPermitido(to.path)) {
                next();
                BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
            }
            else {
                throw new BaseError('Acesso Negado', 'Você não possui privilégios para acessar este recurso');
            }
        }
        catch (error) {
            next(false);
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
            switch ((error.constructor)) {
                case (RedirectError):
                    {
                        RouterManager.redirectRoute((error as RedirectError).url);
                        break;
                    }
                default:
                    {
                        RouterManager.redirectRoute('erro-na-aplicacao');
                        break;
                    }
            }
        }
    }

    private static isPermitido(path) {
        switch (AutenticacaoService.isAutenticado()) {
            case (true):
                {
                    return RouterManager.isRoutePermitidoWhenAutenticado(path);
                }
            case (false):
                {
                    return RouterManager.isRoutePermitidoWhenNotAutenticado(path);
                }
        }
    }

    public static isSelectable(path) {
        let routeConfig = this.getRouteConfig(path);
        return routeConfig.type !== RouterPathType.nsel;
    }

    public static needParameter(path: string) {
        let routeConfig = this.getRouteConfig(path);
        return routeConfig.type === RouterPathType.upd || routeConfig.type === RouterPathType.ext;
    }

    public static getRouteConfig(path: string) {
        return this.config.find(x => x.path === path || x.name === path);
    }

    public static getRouteConfigs() {
        return this.config;
    }

    private static isRoutePermitidoWhenAutenticado(path) {
        switch (path) {
            case (RouterPath.USUARIO_AUTENTICACAO):
                {
                    throw new RedirectError('usuario está autenticado', '/');
                }
            default:
                {
                    return true;
                }
        }
    }

    private static isRoutePermitidoWhenNotAutenticado(path) {
        let _message = 'usuário não autenticado.';
        switch (path) {
            case (RouterPath.USUARIO_ADD_EXTERNAL):
            case (RouterPath.USUARIO_AUTENTICACAO):
                {
                    return true;
                }
            default:
                {
                    throw new RedirectError(_message, RouterPath.USUARIO_AUTENTICACAO);
                }
        }
    }

    public static redirectRoute(path: string, params?: any) {
        if (params) {
            RouterManager.router.push({ name: path, params: params });
        }
        else {
            RouterManager.router.push({ path: path });
        }
    }

    public static redirectRoutePrevious() {
        RouterManager.router.back();
    }

}