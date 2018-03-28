import { Vue, Component } from 'vue-property-decorator';
import { AutenticacaoService } from '../../../module/service/autenticacao.service';
import { AppRouter } from '../../../app.router';
import { BaseRouteConfig } from '../../../../../ezs-common/src/model/client/base-route-config.model';
import { DropDownItem } from '../../../../../ezs-common/src/component/dropdown/dropdown';
import { ApplicationService, ApplicationMode } from '../../../module/service/application.service';
import { AppRouterPath } from '../../../app.router.path';

interface UI {
    userDropdownItens: Array<DropDownItem>;
}

@Component({
    template: require('./navbar.html')
})
export class NavbarComponent extends Vue {

    ui: UI = {
        userDropdownItens: [
            {
                text: 'Logoff',
                item: this.desautenticar
            },
            {
                text: 'Modo Professor',
                item: this.ativarModoProfessor
            },
            {
                text: 'Modo Aluno',
                item: this.ativarModoAluno
            },
            {
                text: 'Minha Conta',
                item: this.goToUsuarioConta
            },
        ]
    };

    desautenticar() {
        AutenticacaoService.desautenticar();
    }

    ativarModoProfessor() {
        ApplicationService.setApplicationMode(ApplicationMode.PROFESSOR);
    }

    ativarModoAluno() {
        ApplicationService.setApplicationMode(ApplicationMode.ALUNO);
    }

    goToUsuarioConta() {
        AppRouter.push(AppRouterPath.USUARIO_CONTA);
    }

    getRotas() {
        return AppRouter.getMenu();
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