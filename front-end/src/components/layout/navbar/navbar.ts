import { Vue } from 'vue-property-decorator';
import { Component } from 'vue-property-decorator';
import { BroadcastEventBus, BroadcastEvent } from '../../../util/broadcast/broadcast.event-bus';
import { RouterManager } from '../../../util/router/router.manager';
import { RouterConfig } from '../../../util/router/router.path';
import { AutenticacaoService } from '../../../util/service/autenticacao/autenticacao.service';

@Component({
    template: require('./navbar.html')
})
export class NavbarComponent extends Vue {

    mounted() {
        BroadcastEventBus.$on(BroadcastEvent.AUTENTICADO, () => {
            this.autenticado = true;
        });
        BroadcastEventBus.$on(BroadcastEvent.NOT_AUTENTICADO, () => {
            this.autenticado = false;
        });
    }

    private autenticado = AutenticacaoService.isAutenticado();

    public desautenticar() {
        AutenticacaoService.desautenticar();
    }

    public isAutenticado() {
        return this.autenticado;
    }

    public getRotas() {
        return RouterManager.getRouteConfigsMenu();
    }

    public getRotasLabel(route: RouterConfig) {
        return route.alias;
    }

    public aoSelecionarRota(route: RouterConfig) {
        if (route) {
            RouterManager.redirectRoute(route.path);
        }
    }

}