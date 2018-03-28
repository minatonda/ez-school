import { RouteConfig } from 'vue-router';

export enum AppRouterPath {

    ROOT = '/',
    ALUNO_FEED = '/aluno/feed',
    ALUNO_HISTORICO_CURSO = '/aluno/historico-curso/:idInstituicaoCursoOcorrencia',
    PROFESSOR_FEED = '/professor/feed',
    PROFESSOR_AULA_GERENCIAMENTO_NOTA = '/professor/aula/gerenciamento-nota/:idInstituicaoCursoOcorrenciaPeriodoProfessor',
    PROFESSOR_AULA_GERENCIAMENTO_AUSENCIA = '/professor/gerenciamento-ausencia/:idInstituicaoCursoOcorrenciaPeriodoProfessor',
    USUARIO_CONTA = '/usuario/conta',
    AUTENTICACAO = '/usuario/autenticacao'



}