import { VueRouter } from 'vue-router/types/router';
import { RedirectError } from '../../modules/error/redirect.error';
import { AutenticacaoService } from '../../service/autenticacao/autenticacao.service';
import { RoutePath } from './router.config';

export class RouterManager {

    private static router: VueRouter;

    public static configureRouter(router: VueRouter) {
        RouterManager.router = router;
        RouterManager.router.beforeEach(RouterManager.beforeEach);
    }

    private static beforeEach(to, from, next) {
        try {
            RouterManager.resolveRoute(to, from, next);
        }
        catch (error) {
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

    private static resolveRoute(to, from, next) {
        switch (AutenticacaoService.isAutenticado()) {
            case (true):
                {
                    if (RouterManager.isRoutePermitidoWhenAutenticado(to.path)) {
                        next();
                    }
                    break;
                }
            case (false):
                {
                    if (RouterManager.isRoutePermitidoWhenNotAutenticado(to.path)) {
                        next();
                    }
                    break;
                }
        }
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

    public static redirectRoute(path: string) {
        RouterManager.router.push(path);
    }

}