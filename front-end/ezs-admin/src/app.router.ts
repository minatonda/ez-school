import { AppBroadcastEventBus, AppBroadcastEvent } from './app.broadcast-event-bus';
import { BaseError } from '../../ezs-common/src/error/base.error';
import { RedirectError } from '../../ezs-common/src/error/redirect.error';
import { AutenticacaoService } from './module/service/autenticacao.service';
import { BaseRouteConfig } from '../../ezs-common/src/model/client/base-route-config.model';
import { AppRouterPath } from './app.router.path';
import { COMPONENT_ROUTE_CONSTANT } from './module/constant/component-route.constant';
import { AutenticaoServiceInterface } from '../../ezs-common/src/service/autenticacao.service.interface';
import { BaseRouter } from '../../ezs-common/src/base.router';

class Router extends BaseRouter {

    constructor(config: Array<BaseRouteConfig>, AutenticacaoService: AutenticaoServiceInterface) {
        super(config, AutenticacaoService);
        this.afterEach(this.onAfterEach);
        this.beforeEach(this.onBeforeEach);
    }

    private onAfterEach() {
        AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
    }

    private onBeforeEach = (to, from, next) => {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            if (this.isPermitido(to.path)) {
                next();
                AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
            }
            else {
                throw new BaseError('Acesso Negado', 'Você não possui privilégios para acessar este recurso');
            }
        }
        catch (error) {
            next(false);
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
            switch ((error.constructor)) {
                case (RedirectError):
                    {
                        this.push((error as RedirectError).url);
                        break;
                    }
                default:
                    {
                        this.push('erro-na-aplicacao');
                        break;
                    }
            }
        }
    }

    private isPermitido = (path) => {
        switch (this.autenticacaoService.isAutenticado()) {
            case (true):
                {
                    return this.isRoutePermitidoWhenAutenticado(path);
                }
            case (false):
                {
                    return this.isRoutePermitidoWhenNotAutenticado(path);
                }
        }
    }

    private isRoutePermitidoWhenAutenticado = (path) => {
        switch (path) {
            case (AppRouterPath.USUARIO_AUTENTICACAO):
                {
                    throw new RedirectError('usuario está autenticado', '/');
                }
            default:
                {
                    return true;
                }
        }
    }

    private isRoutePermitidoWhenNotAutenticado = (path) => {
        let _message = 'usuário não autenticado.';
        switch (path) {
            case (AppRouterPath.USUARIO_AUTENTICACAO):
                {
                    return true;
                }
            default:
                {
                    throw new RedirectError(_message, AppRouterPath.USUARIO_AUTENTICACAO);
                }
        }
    }

}

export const AppRouter = new Router(COMPONENT_ROUTE_CONSTANT, AutenticacaoService);