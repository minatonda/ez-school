import { RouteConfig } from 'vue-router';

export enum RouterPath {

    ROOT = '/',
    ABOUT = '/about',

    USUARIO_AUTENTICACAO = '/usuario/autenticacao',
    USUARIO = '/usuario',
    USUARIO_ADD = '/usuario/add',
    USUARIO_UPD = '/usuario/upd/:id',
    USUARIO_ADD_EXTERNAL = '/usuario/cadastro',

    ALUNO = '/aluno',
    ALUNO_ADD = '/aluno/add',
    ALUNO_UPD = '/aluno/upd/:id',

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

    INSTITUICAO_CURSO = '/instituicao/:idInstituicao/curso',
    INSTITUICAO_CURSO_ADD = '/instituicao/:idInstituicao/curso/add',
    INSTITUICAO_CURSO_UPD = '/instituicao/:idInstituicao/curso/:id/upd'

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