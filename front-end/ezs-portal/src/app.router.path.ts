import { RouteConfig } from 'vue-router';

export enum AppRouterPath {

    ROOT = '/',
    AUTENTICACAO = '/usuario/autenticacao',
    HISTORICO_CURSO = '/aluno-historico-curso/:idInstituicaoCursoOcorrencia',
    AULA_GERENCIAMENTO_NOTA = '/aula/gerenciamento-nota/:idInstituicaoCursoOcorrenciaPeriodoProfessor',
    AULA_GERENCIAMENTO_AUSENCIA = '/aula/gerenciamento-ausencia/:idInstituicaoCursoOcorrenciaPeriodoProfessor'

}