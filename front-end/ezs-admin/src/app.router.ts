import { AppBroadcastEventBus, AppBroadcastEvent } from './app.broadcast-event-bus';
import { BaseError } from '../../ezs-common/src/error/base.error';
import { RedirectError } from '../../ezs-common/src/error/redirect.error';
import { AutenticacaoService } from './module/service/autenticacao.service';
import { BaseRouteConfig } from '../../ezs-common/src/model/client/base-route-config.model';
import { AppRouterPath } from './app.router.path';
import { COMPONENT_ROUTE_CONSTANT } from './module/constant/component-route.constant';
import { AutenticaoServiceInterface } from '../../ezs-common/src/service/autenticacao.service.interface';
import { BaseRouter } from '../../ezs-common/src/base.router';
import { I18NUtil } from '../../ezs-common/src/util/i18n/i18n.util';
import { I18N_ERROR_GENERIC } from '../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from './module/service/application.service';
import { NotifyUtil } from '../../ezs-common/src/util/notify/notify.util';

class Router extends BaseRouter {

    constructor(config: Array<BaseRouteConfig>) {
        super(config);
        this.afterEach(this.onAfterEach);
        this.beforeEach(this.onBeforeEach);
    }

    private onAfterEach() {
        AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
    }

    private onBeforeEach = async (to, from, next) => {


        AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);

        setImmediate(async () => {
            try {

                if (AutenticacaoService.isAutenticado() && !ApplicationService.isReady() && !ApplicationService.isLoading()) {
                    ApplicationService.configureDefaults();
                }

                while (AutenticacaoService.isAutenticado() && !ApplicationService.isReady() && ApplicationService.isLoading()) {
                    await new Promise(resolve => setTimeout(resolve, 1000));
                }

                if (AutenticacaoService.isAutenticado() && this.isRoutePermitidoWhenAutenticado(to.name, to.params)) {
                    next();
                }
                else if (AutenticacaoService.isAutenticado()) {
                    let template = I18NUtil.getTemplateMessageGeneric(I18N_ERROR_GENERIC.ACESSO_NEGADO, ApplicationService.getLanguage());
                    throw new BaseError('unauthorized', template.title, template.message);
                }
                else if (this.isRoutePermitidoWhenNotAutenticado(to.name)) {
                    next();
                }
            }
            catch (error) {

                console.error(error);
                switch ((error.constructor)) {
                    case (RedirectError):
                        {
                            this.push((error as RedirectError).url);
                            break;
                        }
                    default:
                        {
                            NotifyUtil.error((error as BaseError).title, (error as BaseError).message);
                            break;
                        }
                }

                next(false);
                AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);

            }

        });

    }


    private isRoutePermitidoWhenAutenticado = (path, params) => {
        switch (path) {
            case (AppRouterPath.USUARIO_AUTENTICACAO):
                {
                    let template = I18NUtil.getTemplateMessageGeneric(I18N_ERROR_GENERIC.JA_AUTENTICADO, ApplicationService.getLanguage());
                    throw new BaseError('already-authenticated', template.title, template.message);
                }
            case (AppRouterPath.ROOT):
                {
                    return true;
                }
            case (AppRouterPath.INSTITUICAO_UPD):
            case (AppRouterPath.INSTITUICAO_COLABORADOR):
            case (AppRouterPath.INSTITUICAO_COLABORADOR_ADD):
            case (AppRouterPath.INSTITUICAO_COLABORADOR_UPD):
            case (AppRouterPath.INSTITUICAO_COLABORADOR_PERFIL):
            case (AppRouterPath.INSTITUICAO_COLABORADOR_PERFIL_ADD):
            case (AppRouterPath.INSTITUICAO_COLABORADOR_PERFIL_UPD):
            case (AppRouterPath.INSTITUICAO_CURSO):
            case (AppRouterPath.INSTITUICAO_CURSO_ADD):
            case (AppRouterPath.INSTITUICAO_CURSO_UPD):
            case (AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA):
            case (AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA_ADD):
            case (AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA_UPD):
                {
                    return ApplicationService.isAdmin() || (ApplicationService.getInstituicoes().some(x => x.id.toString() === params.id.toString()) && ApplicationService.getViews().indexOf(path) > -1);
                }
            default:
                {
                    return ApplicationService.isAdmin() || ApplicationService.getViews().indexOf(path) > -1;
                }
        }
    }

    private isRoutePermitidoWhenNotAutenticado = (path) => {
        switch (path) {
            case (AppRouterPath.USUARIO_AUTENTICACAO):
                {
                    return true;
                }
            default:
                {
                    let template = I18NUtil.getTemplateMessageGeneric(I18N_ERROR_GENERIC.JA_AUTENTICADO, ApplicationService.getLanguage());
                    throw new RedirectError(template.title, template.message, AppRouterPath.USUARIO_AUTENTICACAO);
                }
        }
    }

    public getMenuPermitido = () => {
        return this.configs.filter(x => {
            return x.menu && ApplicationService.getViews().indexOf(x.path) > -1;
        });
    }

}

export const AppRouter = new Router(COMPONENT_ROUTE_CONSTANT);