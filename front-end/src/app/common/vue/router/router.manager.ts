import { RedirectError } from '../../modules/error/redirect.error';
import { AutenticacaoService } from '../../service/autenticacao/autenticacao.service';
import { BaseError } from '../../modules/error/base.error';
import { RoutePath } from './route-path';
import { RoutePathType } from './route-path-type';
import { ROUTER_OPTIONS, ROUTES_CONFIG_BASE } from './router.constants';
import VueRouter from 'vue-router';
import { BroadcastEventBus } from '../broadcast/broadcast.event-bus';
import { BroadcastEvent } from '../broadcast/broadcast.events';

export class RouterManager {

    private static router: VueRouter;

    public static generateRouter() {
        RouterManager.router = new VueRouter(ROUTER_OPTIONS);
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
                    };
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

    private static isVisivelParaUsuario(path) {
        let invisibleRoutes = [RoutePath.USUARIO_AUTENTICACAO];
        return invisibleRoutes.indexOf(path) === -1;
    }

    public static needParameter(path: string) {
        let routeConfig = this.getRouteConfig(path);
        return routeConfig.type === RoutePathType.upd || routeConfig.type === RoutePathType.ext;
    }

    public static getRouteConfig(path: string) {
        return ROUTES_CONFIG_BASE.find(x => x.path === path || x.name === path);
    }

    private static isRoutePermitidoWhenAutenticado(path) {
        switch (path) {
            case (RoutePath.USUARIO_AUTENTICACAO):
                {
                    throw new RedirectError('usuario está autenticado', '/');
                };
            default:
                {
                    return true;
                };
        }
    }

    private static isRoutePermitidoWhenNotAutenticado(path) {
        let _message = 'usuário não autenticado.';
        switch (path) {
            case (RoutePath.USUARIO_ADD_EXTERNAL):
            case (RoutePath.USUARIO_AUTENTICACAO):
                {
                    return true;
                };
            default:
                {
                    throw new RedirectError(_message, RoutePath.USUARIO_AUTENTICACAO);
                };
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