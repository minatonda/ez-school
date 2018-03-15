import { Vue, Component } from 'vue-property-decorator';
import { AutenticacaoService } from '../../../module/service/autenticacao.service';
import { AppRouter } from '../../../app.router';
import { BaseRouteConfig } from '../../../../../ezs-common/src/model/client/base-route-config.model';
import { DropDownItem } from '../../../../../ezs-common/src/component/dropdown/dropdown';

interface UI {
    userDropdownItens: Array < DropDownItem > ;
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

    public desautenticar() {
        AutenticacaoService.desautenticar();
    }

    public getRotas() {
        return AppRouter.getMenu();
    }

    public getRotasLabel(route: BaseRouteConfig) {
        return route.alias;
    }

    public aoSelecionarRota(route: BaseRouteConfig) {
        if (route) {
            AppRouter.push(route.path);
        }
    }

}