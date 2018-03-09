import { RouteConfig } from 'vue-router';

export enum AppRouterPath {

    ROOT = '/',
        ROOT_ALUNO = '/feed-aluno',
        ROOT_PROFESSOR = '/feed-professor',
        AUTENTICACAO = '/usuario/autenticacao',
        AULA_GERENCIAMENTO_NOTA = '/aula/gerenciamento-nota/:idInstituicaoCursoOcorrenciaPeriodoProfessor'

}