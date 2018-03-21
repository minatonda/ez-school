import { AppRouter } from '../../app.router';
import { BaseRouteConfig } from '../../../../ezs-common/src/model/client/base-route-config.model';
import { RouterPathType } from '../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouterPath } from '../../app.router.path';
import { PageHomeComponent } from '../../components/page/page-home/index';
import { PageAulaGerenciamentoNotaComponent } from '../../components/page/page-aula-gerenciamento-nota/index';
import { PageAutenticacaoComponent } from '../../components/page/page-autenticacao/index';
import { PageAulaGerenciamentoAusenciaComponent } from '../../components/page/page-aula-gerenciamento-ausencia/index';
import { PageHistoricoCursoComponent } from '../../components/page/page-historico-curso/index';

export const COMPONENT_ROUTE_CONSTANT: Array<BaseRouteConfig> = [
    { menu: true, type: RouterPathType.otr, path: AppRouterPath.ROOT, name: AppRouterPath.ROOT, component: PageHomeComponent, alias: 'Home' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.AUTENTICACAO, name: AppRouterPath.AUTENTICACAO, component: PageAutenticacaoComponent, alias: 'Autenticação' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.HISTORICO_CURSO, name: AppRouterPath.HISTORICO_CURSO, component: PageHistoricoCursoComponent, alias: 'Autenticação' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.AULA_GERENCIAMENTO_NOTA, name: AppRouterPath.AULA_GERENCIAMENTO_NOTA, component: PageAulaGerenciamentoNotaComponent, alias: 'Gerenciamento de Nota' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.AULA_GERENCIAMENTO_AUSENCIA, name: AppRouterPath.AULA_GERENCIAMENTO_AUSENCIA, component: PageAulaGerenciamentoAusenciaComponent, alias: 'Gerenciamento de Ausências' },
];