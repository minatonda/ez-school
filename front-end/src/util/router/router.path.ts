import { RouteConfig } from 'vue-router';

export enum RouterPath {

    ROOT = '/',
    ABOUT = '/about',

    USUARIO_AUTENTICACAO = '/usuario/autenticacao',
    USUARIO = '/usuario',
    USUARIO_ADD = '/usuario/add',
    USUARIO_UPD = '/usuario/upd/:id',
    USUARIO_ADD_EXTERNAL = '/usuario/cadastro',

    CURSO = '/curso',
    CURSO_ADD = '/curso/add',
    CURSO_UPD = '/curso/upd/:id',

    MATERIA = '/materia',
    MATERIA_ADD = '/materia/add',
    MATERIA_UPD = '/materia/upd/:id',

    INSTITUICAO = '/instituicao',
    INSTITUICAO_ADD = '/instituicao/add',
    INSTITUICAO_UPD = '/instituicao/upd/:id',

    INSTITUICAO_CATEGORIA = '/instituicao-categoria',
    INSTITUICAO_CATEGORIA_ADD = '/instituicao-categoria/add',
    INSTITUICAO_CATEGORIA_UPD = '/instituicao-categoria/upd/:id',

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