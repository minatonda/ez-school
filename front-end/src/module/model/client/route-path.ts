import { RouteConfig } from 'vue-router';

export enum RouterPath {

    ROOT = '/',
        ABOUT = '/about',

        USUARIO_AUTENTICACAO = '/usuario/autenticacao',
        USUARIO = '/usuario',
        USUARIO_ADD = '/usuario/add',
        USUARIO_UPD = '/usuario/:id/upd',
        USUARIO_ALUNO = '/usuario/:id/aluno',
        USUARIO_PROFESSOR = '/usuario/:id/professor',
        USUARIO_ADD_EXTERNAL = '/usuario/cadastro',

        CURSO = '/curso',
        CURSO_ADD = '/curso/add',
        CURSO_UPD = '/curso/:id/upd',

        MATERIA = '/materia',
        MATERIA_ADD = '/materia/add',
        MATERIA_UPD = '/materia/:id/upd',

        INSTITUICAO = '/instituicao',
        INSTITUICAO_ADD = '/instituicao/add',
        INSTITUICAO_UPD = '/instituicao/:id/upd',

        INSTITUICAO_CATEGORIA = '/instituicao-categoria',
        INSTITUICAO_CATEGORIA_ADD = '/instituicao-categoria/add',
        INSTITUICAO_CATEGORIA_UPD = '/instituicao-categoria/:id/upd/',

        INSTITUICAO_CURSO = '/instituicao/:id/curso',
        INSTITUICAO_CURSO_ADD = '/instituicao/:id/curso/add',
        INSTITUICAO_CURSO_UPD = '/instituicao/:id/curso/:idInstituicaoCurso/upd',

        INSTITUICAO_CURSO_OCORRENCIA = '/instituicao/:id/curso/:idInstituicaoCurso/ocorrencia',
        INSTITUICAO_CURSO_OCORRENCIA_ADD = '/instituicao/:id/curso/:idInstituicaoCurso/ocorrencia/add',
        INSTITUICAO_CURSO_OCORRENCIA_UPD = '/instituicao/:id/curso/:idInstituicaoCurso/ocorrencia/:idInstituicaoCursoOcorrencia/upd',

        CATEGORIA_PROFISSIONAL = '/categoria-profissinal',
        CATEGORIA_PROFISSIONAL_ADD = '/categoria-profissinal/add',
        CATEGORIA_PROFISSIONAL_UPD = '/categoria-profissinal/:id/upd'


}

export enum RouterPathType {

    add = 'add',
        upd = 'upd',
        list = 'list',
        otr = 'otr',

}

export interface RouterConfig extends RouteConfig {
    type: RouterPathType;
    menu: boolean;
}