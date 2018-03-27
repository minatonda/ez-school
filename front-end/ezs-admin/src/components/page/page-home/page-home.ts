import { Vue, Component } from 'vue-property-decorator';
import { AppRouter } from '../../../app.router';
import { BaseRouteConfig } from '../../../../../ezs-common/src/model/client/base-route-config.model';
import { ApplicationService } from '../../../module/service/application.service';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';
import { AppRouterPath } from '../../../app.router.path';

@Component({
    template: require('./page-home.html')
})
export class PageHomeComponent extends Vue {

    getRotas() {
        return AppRouter.getMenuPermitido();
    }

    getRotasLabel(route: BaseRouteConfig) {
        return route.alias;
    }

    aoSelecionarRota(route: BaseRouteConfig) {
        AppRouter.push(route.path);
    }

    getInstituicaoFromUser() {
        return ApplicationService.getInstituicoes();
    }

    doGoGerenciamentoCursos(instituicao: InstituicaoModel) {
        AppRouter.push({ name: AppRouterPath.INSTITUICAO_CURSO, params: { id: instituicao.id.toString() } });
    }

    doGoGerenciamentoColaboradores(instituicao: InstituicaoModel) {
        AppRouter.push({ name: AppRouterPath.INSTITUICAO_COLABORADOR, params: { id: instituicao.id.toString() } });
    }

    doGoGerenciamentoPerfil(instituicao: InstituicaoModel) {
        AppRouter.push({ name: AppRouterPath.INSTITUICAO_COLABORADOR_PERFIL, params: { id: instituicao.id.toString() } });
    }

}