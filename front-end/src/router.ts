import VueRouter from 'vue-router';
import { RouterConfig, RouterPath } from './module/model/client/route-path';
import { RouterManagerBuilder } from './module/util/router.util';
import { BroadcastEventBus, BroadcastEvent } from './module/broadcast.event-bus';
import { BaseError, RedirectError } from './module/model/client/error';
import { AutenticacaoService } from './module/service/autenticacao.service';


export class Router {

    private static router: VueRouter;
    private static config: Array<RouterConfig>;

    public static generateRouter(config: Array<RouterConfig>) {
        this.config = config;
        Router.router = new VueRouter(RouterManagerBuilder.buildRouteOptions(config));
        Router.router.beforeEach(Router.beforeEach);
        return Router.router;
    }

    private static beforeEach(to, from, next) {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (Router.isPermitido(to.path)) {
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
                        Router.redirectRoute((error as RedirectError).url);
                        break;
                    }
                default:
                    {
                        Router.redirectRoute('erro-na-aplicacao');
                        break;
                    }
            }
        }
    }

    private static isPermitido(path) {
        switch (AutenticacaoService.isAutenticado()) {
            case (true):
                {
                    return Router.isRoutePermitidoWhenAutenticado(path);
                }
            case (false):
                {
                    return Router.isRoutePermitidoWhenNotAutenticado(path);
                }
        }
    }

    public static getRouteConfig(path: string) {
        return this.config.find(x => x.path === path || x.name === path);
    }

    public static getRouteConfigs() {
        return this.config;
    }

    public static getRouteConfigsMenu() {
        return this.config.filter(x => x.menu);
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
            Router.router.push({ name: path, params: params });
        }
        else {
            Router.router.push({ path: path });
        }
    }

    public static redirectRoutePrevious() {
        Router.router.back();
    }

}