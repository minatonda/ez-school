import { Vue, Component} from 'vue-property-decorator';
import { AppRouter } from '../../../app.router';
import { BaseRouteConfig } from '../../../../../ezs-common/src/model/client/base-route-config.model';

@Component({
    template: require('./page-home.html')
})
export class PageHomeComponent extends Vue {

    public getRotas() {
        return AppRouter.getMenuPermitido();
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