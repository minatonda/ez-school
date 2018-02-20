import { Vue, Component } from 'vue-property-decorator';
import { AutenticacaoService } from '../../../module/service/autenticacao.service';
import { AppRouter } from '../../../app.router';
import { BaseRouteConfig } from '../../../../../ezs-common/src/model/client/base-route-config.model';

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