import { AppRouter } from '../../app.router';
import { BaseRouteConfig } from '../../../../ezs-common/src/model/client/base-route-config.model';
import { RouterPathType } from '../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouterPath } from '../../app.router.path';
import { PageHomeComponent } from '../../components/page/page-home';
import { PageProfessorAulaGerenciamentoNotaComponent } from '../../components/page/page-professor-aula-gerenciamento-nota';
import { PageAutenticacaoComponent } from '../../components/page/page-autenticacao';
import { PageProfessorAulaGerenciamentoAusenciaComponent } from '../../components/page/page-professor-aula-gerenciamento-ausencia';
import { PageAlunoHistoricoCursoComponent } from '../../components/page/page-aluno-historico-curso';
import { PageAlunoFeedComponent } from '../../components/page/page-aluno-feed';
import { PageProfessorFeedComponent } from '../../components/page/page-professor-feed';
import { PageUsuarioContaComponent } from '../../components/page/page-usuario-conta';

export const COMPONENT_ROUTE_CONSTANT: Array<BaseRouteConfig> = [
    { menu: true, type: RouterPathType.otr, path: AppRouterPath.ROOT, name: AppRouterPath.ROOT, component: PageHomeComponent, alias: 'Home' },
    
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.ALUNO_FEED, name: AppRouterPath.ALUNO_FEED, component: PageAlunoFeedComponent, alias: 'Feed' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.ALUNO_HISTORICO_CURSO, name: AppRouterPath.ALUNO_HISTORICO_CURSO, component: PageAlunoHistoricoCursoComponent, alias: 'Histórico de Curso' },
    
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.PROFESSOR_FEED, name: AppRouterPath.PROFESSOR_FEED, component: PageProfessorFeedComponent, alias: 'Feed' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.PROFESSOR_AULA_GERENCIAMENTO_NOTA, name: AppRouterPath.PROFESSOR_AULA_GERENCIAMENTO_NOTA, component: PageProfessorAulaGerenciamentoNotaComponent, alias: 'Gerenciamento de Nota' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.PROFESSOR_AULA_GERENCIAMENTO_AUSENCIA, name: AppRouterPath.PROFESSOR_AULA_GERENCIAMENTO_AUSENCIA, component: PageProfessorAulaGerenciamentoAusenciaComponent, alias: 'Gerenciamento de Ausências' },
    
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.AUTENTICACAO, name: AppRouterPath.AUTENTICACAO, component: PageAutenticacaoComponent, alias: 'Autenticação' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.USUARIO_CONTA, name: AppRouterPath.USUARIO_CONTA, component: PageUsuarioContaComponent, alias: 'Minha Conta' },


];