import { AppRouter } from '../../app.router';
import { BaseRouteConfig } from '../../../../ezs-common/src/model/client/base-route-config.model';
import { RouterPathType } from '../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouterPath } from '../../app.router.path';
import { PageHomeComponent } from '../../components/page/page-home/index';
import { PageAulaGerenciamentoComponent } from '../../components/page/page-aula-gerenciamento/index';
import { PageHomeAlunoComponent } from '../../components/page/page-home-aluno/index';
import { PageHomeProfessorComponent } from '../../components/page/page-home-professor/index';
import { PageAutenticacaoComponent } from '../../components/page/page-autenticacao/index';

export const COMPONENT_ROUTE_CONSTANT: Array < BaseRouteConfig > = [
    { menu: true, type: RouterPathType.otr, path: AppRouterPath.ROOT, name: AppRouterPath.ROOT, component: PageHomeComponent, alias: 'Home' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.ROOT_ALUNO, name: AppRouterPath.ROOT_ALUNO, component: PageHomeAlunoComponent, alias: 'Home' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.ROOT_PROFESSOR, name: AppRouterPath.ROOT_PROFESSOR, component: PageHomeProfessorComponent, alias: 'Home' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.AUTENTICACAO, name: AppRouterPath.AUTENTICACAO, component: PageAutenticacaoComponent, alias: 'Autenticação' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.GERENCIAMENTO_AULA, name: AppRouterPath.GERENCIAMENTO_AULA, component: PageAulaGerenciamentoComponent, alias: 'Gerenciamento de Aula' },

];