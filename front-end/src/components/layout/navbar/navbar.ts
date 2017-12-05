import { Vue } from 'vue-property-decorator';
import { Component } from 'vue-property-decorator';
import { BroadcastEventBus, BroadcastEvent } from '../../../module/broadcast.event-bus';
import { AutenticacaoService } from '../../../module/service/autenticacao.service';
import { Router } from '../../../router';
import { RouterConfig } from '../../../module/model/client/route-path';

@Component({
    template: require('./navbar.html')
})
export class NavbarComponent extends Vue {

    public desautenticar() {
        AutenticacaoService.desautenticar();
    }

    public isAutenticado() {
        return AutenticacaoService.isAutenticado();
    }

    public getRotas() {
        return Router.getRouteConfigsMenu();
    }

    public getRotasLabel(route: RouterConfig) {
        return route.alias;
    }

    public aoSelecionarRota(route: RouterConfig) {
        if (route) {
            Router.redirectRoute(route.path);
        }
    }

}