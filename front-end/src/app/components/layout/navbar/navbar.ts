import Vue from 'vue';
import _ from 'lodash';
import { Component } from 'vue-property-decorator';
import { BroadcastEventBus } from '../../../common/vue/broadcast/broadcast.event-bus';
import { BroadcastEvent } from '../../../common/vue/broadcast/broadcast.events';
import { AutenticacaoService } from '../../../common/service/autenticacao/autenticacao.service';
import { ROUTES_CONFIG_BASE } from '../../../common/vue/router/router.constants';
import { RouteConfigBase } from '../../../common/vue/router/route-config-base';
import { RouterManager } from '../../../common/vue/router/router.manager';

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
        return ROUTES_CONFIG_BASE.filter(r => !RouterManager.needParameter(r.path));
    }

    public getRotasLabel(route: RouteConfigBase) {
        return route.alias;
    }

    public aoSelecionarRota(route: RouteConfigBase) {
        if (route) {
            RouterManager.redirectRoute(route.path);
        }
    }

}