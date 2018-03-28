import { Vue, Component } from 'vue-property-decorator';
import { AutenticacaoService } from '../../../module/service/autenticacao.service';
import { AppRouter } from '../../../app.router';
import { BaseRouteConfig } from '../../../../../ezs-common/src/model/client/base-route-config.model';
import { DropDownItem } from '../../../../../ezs-common/src/component/dropdown/dropdown';
import { AppBroadcastEvent, AppBroadcastEventBus } from '../../../app.broadcast-event-bus';

interface UI {
    userDropdownItens: Array<DropDownItem>;
}

@Component({
    template: require('./navbar.html')
})
export class NavbarComponent extends Vue {

    ui: UI = {
        userDropdownItens: [{
            text: 'Logoff',
            item: this.desautenticar
        }]
    };

    desautenticar() {
        AutenticacaoService.desautenticar();
    }

    getRotas() {
        return AppRouter.getMenuPermitido();
    }

    getRotasLabel(route: BaseRouteConfig) {
        return route.alias;
    }

    aoSelecionarRota(route: BaseRouteConfig) {
        if (route) {
            AppRouter.push(route.path);
        }
    }

}