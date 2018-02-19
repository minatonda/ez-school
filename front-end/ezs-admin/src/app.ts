import { Component, Vue } from 'vue-property-decorator';
import { NavbarComponent } from './components/layout/navbar/index';
import { LoaderCompactComponent } from '../../ezs-common/src/component/loader-compact/loader-compact';
import { AppRouter } from './app.router';
import { AutenticacaoService } from './module/service/autenticacao.service';
import { ApplicationService } from './module/service/application.service';
import { AppBroadcastEventBus, AppBroadcastEvent } from './app.broadcast-event-bus';
import { AppRouterPath } from './app.router.path';
import { I18N_LANG } from '../../ezs-common/src/constant/i18n-template-messages.contant';

@Component({
    router: AppRouter,
    components: {
        navbarComponent: NavbarComponent,
        loaderCompactRootComponent: LoaderCompactComponent
    }
})
export class AppComponent extends Vue {

    showLoader = false;
    intervalRefresh: any;

    async created() {
        this.registerBroadcastEvents();
        ApplicationService.setLanguage(I18N_LANG.ptBR);
    }

    async beforeMount() {
        this.tryAutoAuthentication();
    }

    async tryAutoAuthentication() {
        // AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
        // try {
        //     await AutenticacaoService.authenticate();
        //     AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        // }
        // catch (e) {
        //     AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        // }
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
            }
        });

        AppBroadcastEventBus.$on(AppBroadcastEvent.DESAUTENTICADO, () => {
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

    isAutenticado = AutenticacaoService.isAutenticado;

}