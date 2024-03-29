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
import { ApplicationService, ApplicationMode } from './module/service/application.service';
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
            case (AppRouterPath.PROFESSOR_AULA_GERENCIAMENTO_NOTA):
            case (AppRouterPath.PROFESSOR_AULA_GERENCIAMENTO_AUSENCIA):
                {
                    return ApplicationService.isAdmin() || !!ApplicationService.getInstituicaoBusinessAulasByProfessor().find(x => x.idInstituicaoCursoOcorrenciaPeriodoProfessor.toString() === params.idInstituicaoCursoOcorrenciaPeriodoProfessor.toString());
                }
            case (AppRouterPath.ROOT):
                {
                    this.redirectFeed(path, params);
                }
            default:
                {
                    return ApplicationService.isAdmin() || ApplicationService.getViews().indexOf(path) > -1;
                }
        }
    }

    private redirectFeed = (path, params) => {
        if (ApplicationService.isApplicationMode(ApplicationMode.ALUNO)) {
            throw new RedirectError('Modo Aluno Ativado', 'Redirecionando para feed de alunos', AppRouterPath.ALUNO_FEED);
        }
        else if (ApplicationService.isApplicationMode(ApplicationMode.PROFESSOR)) {
            throw new RedirectError('Modo Professor Ativado', 'Redirecionando para feed de professores', AppRouterPath.PROFESSOR_FEED);
        }
    }

    private isRoutePermitidoWhenNotAutenticado = (path) => {
        switch (path) {
            case (AppRouterPath.AUTENTICACAO):
                {
                    return true;
                }
            default:
                {
                    let template = I18NUtil.getTemplateMessageGeneric(I18N_ERROR_GENERIC.JA_AUTENTICADO, ApplicationService.getLanguage());
                    throw new RedirectError(template.title, template.message, AppRouterPath.AUTENTICACAO);
                }
        }
    }

}

export const AppRouter = new Router(COMPONENT_ROUTE_CONSTANT);