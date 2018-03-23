import { Component, Vue } from 'vue-property-decorator';
import { NavbarComponent } from './components/layout/navbar/index';
import { LoaderCompactComponent } from '../../ezs-common/src/component/loader-compact/loader-compact';
import { AppRouter } from './app.router';
import { AutenticacaoService } from './module/service/autenticacao.service';
import { ApplicationService } from './module/service/application.service';
import { AppBroadcastEventBus, AppBroadcastEvent } from './app.broadcast-event-bus';
import { AppRouterPath } from './app.router.path';
import { I18N_LANG } from '../../ezs-common/src/constant/i18n-template-messages.contant';
import { FACTORY_CONSTANT } from './module/constant/factory.constant';

@Component({
    router: AppRouter,
    components: {
        navbarComponent: NavbarComponent,
        loaderCompactRootComponent: LoaderCompactComponent
    }
})
export class AppComponent extends Vue {

    showLoader = true;
    intervalRefresh: any;

    async created() {
        this.registerBroadcastEvents();
        ApplicationService.setLanguage(I18N_LANG.ptBR);
    }

    async beforeMount() {

    }

    registerBroadcastEvents() {
        AppBroadcastEventBus.$on(AppBroadcastEvent.EXIBIR_LOADER, () => {
            this.showLoader = true;
        });

        AppBroadcastEventBus.$on(AppBroadcastEvent.ESCONDER_LOADER, () => {
            this.showLoader = false;
        });

        AppBroadcastEventBus.$on(AppBroadcastEvent.AUTENTICADO, async () => {
            try {
                AppRouter.push(AppRouterPath.ROOT);
            }
            catch (e) {
                AutenticacaoService.desautenticar();
                ApplicationService.resetDefaults();
            }
            finally {
            }
        });

        AppBroadcastEventBus.$on(AppBroadcastEvent.DESAUTENTICADO, () => {
            ApplicationService.resetDefaults();
            AppRouter.push(AppRouterPath.USUARIO_AUTENTICACAO);
        });
    }

    routerStyle() {
        let style = { 'min-height': '100%' };
        if ((this as any).showLoader) {
            style['position'] = 'absolute';
            style['min-height'] = '100%';
            style['max-height'] = '100%';
            style['width'] = '100%';
            style['overflow'] = 'hidden';
            style['display'] = 'none';
        }
        return style;
    }

    isShowNavbar() {
        return AutenticacaoService.isAutenticado() && ApplicationService.isReady();
    }

}