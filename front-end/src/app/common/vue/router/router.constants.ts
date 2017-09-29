import { VueRouter, RouteConfig } from 'vue-router/types/router';
import { Vue } from 'vue/types/vue';

import { RouterOptions } from 'vue-router/types';
import { RouteConfigBase } from './route-config-base';
import { RoutePathType } from './route-path-type';
import { RoutePath } from './route-path';

import { HomeComponent } from '../../../components/home/home';

import { UsuarioComponent } from '../../../components/usuario/usuario';
import { UsuarioAddUpdComponent } from '../../../components/usuario/add-upd/usuario-add-upd';
import { UsuarioAutenticacaoComponent } from '../../../components/usuario/usuario-autenticacao/usuario-autenticacao';

import { CursoComponent } from '../../../components/curso/curso';
import { CursoAddUpdComponent } from '../../../components/curso/add-upd/curso-add-upd';

import { MateriaComponent } from '../../../components/materia/materia';
import { MateriaAddUpdComponent } from '../../../components/materia/add-upd/materia-add-upd';

import { InstituicaoComponent } from '../../../components/instituicao/instituicao';
import { InstituicaoAddUpdComponent } from '../../../components/instituicao/add-upd/instituicao-add-upd';
import { InstituicaoCategoriaComponent } from '../../../components/instituicao-categoria/instituicao-categoria';
import { InstituicaoCategoriaAddUpdComponent } from '../../../components/instituicao-categoria/add-upd/instituicao-categoria-add-upd';

class Build {
    public static buildRouteOptions(listRouteBase: Array<RouteConfigBase>) {
        let options: RouterOptions = {
            routes: this.buildRouteOptionsRoutes(listRouteBase)
        };
        return options;
    }
    private static buildRouteOptionsRoutes(listRouteBase: Array<RouteConfigBase>) {
        let routes: Array<RouteConfig> = [];
        for (let route of listRouteBase) {
            routes.push({
                path: this.buildRoutePath(route.path as RoutePath, route.type),
                component: route.component,
                alias: route.alias,
                name: route.name,
                props: { operation: route.type, alias: route.alias }
            });
        }
        return routes;
    }
    private static buildRoutePath(path: RoutePath, pathType: RoutePathType) {
        if (pathType === RoutePathType.upd) {
            return path + '/:id';
        }
        if (pathType === RoutePathType.ext) {
            let arrayPath = path.split('/');
            arrayPath.splice(2, 0, ':id');
            return arrayPath.join('/');
        }
        else {
            return path;
        }
    }
};

export const ROUTES_CONFIG_BASE: Array<RouteConfigBase> = [
    { type: RoutePathType.otr, path: RoutePath.ROOT, name: RoutePath.ROOT, component: HomeComponent, alias: 'Home' },

    { type: RoutePathType.list, path: RoutePath.USUARIO, name: RoutePath.USUARIO, component: UsuarioComponent, alias: 'Usuários' },
    { type: RoutePathType.add, path: RoutePath.USUARIO_ADD, name: RoutePath.USUARIO_ADD, component: UsuarioAddUpdComponent, alias: 'Usuário - Adicionar' },
    { type: RoutePathType.upd, path: RoutePath.USUARIO_UPD, name: RoutePath.USUARIO_UPD, component: UsuarioAddUpdComponent, alias: 'Usuário - Atualizar' },
    { type: RoutePathType.nsel, path: RoutePath.USUARIO_AUTENTICACAO, name: RoutePath.USUARIO_AUTENTICACAO, component: UsuarioAutenticacaoComponent, alias: 'Autenticação' },

    { type: RoutePathType.list, path: RoutePath.CURSO, name: RoutePath.CURSO, component: CursoComponent, alias: 'Cursos' },
    { type: RoutePathType.add, path: RoutePath.CURSO_ADD, name: RoutePath.CURSO_ADD, component: CursoAddUpdComponent, alias: 'Curso - Adicionar' },
    { type: RoutePathType.upd, path: RoutePath.CURSO_UPD, name: RoutePath.CURSO_UPD, component: CursoAddUpdComponent, alias: 'Curso - Atualizar' },

    { type: RoutePathType.list, path: RoutePath.MATERIA, name: RoutePath.MATERIA, component: MateriaComponent, alias: 'Materias' },
    { type: RoutePathType.add, path: RoutePath.MATERIA_ADD, name: RoutePath.MATERIA_ADD, component: MateriaAddUpdComponent, alias: 'Materia - Adicionar' },
    { type: RoutePathType.upd, path: RoutePath.MATERIA_UPD, name: RoutePath.MATERIA_UPD, component: MateriaAddUpdComponent, alias: 'Materia - Atualizar' },

    { type: RoutePathType.list, path: RoutePath.INSTITUICAO, name: RoutePath.INSTITUICAO, component: InstituicaoComponent, alias: 'Instituições' },
    { type: RoutePathType.add, path: RoutePath.INSTITUICAO_ADD, name: RoutePath.INSTITUICAO_ADD, component: InstituicaoAddUpdComponent, alias: 'Instituição - Adicionar' },
    { type: RoutePathType.upd, path: RoutePath.INSTITUICAO_UPD, name: RoutePath.INSTITUICAO_UPD, component: InstituicaoAddUpdComponent, alias: 'Instituição - Atualizar' },

    { type: RoutePathType.list, path: RoutePath.INSTITUICAO_CATEGORIA, name: RoutePath.INSTITUICAO_CATEGORIA, component: InstituicaoCategoriaComponent, alias: 'Instituição - Categorias' },
    { type: RoutePathType.add, path: RoutePath.INSTITUICAO_CATEGORIA_ADD, name: RoutePath.INSTITUICAO_CATEGORIA_ADD, component: InstituicaoCategoriaAddUpdComponent, alias: 'Instituição Categoria - Adicionar' },
    { type: RoutePathType.upd, path: RoutePath.INSTITUICAO_CATEGORIA_UPD, name: RoutePath.INSTITUICAO_CATEGORIA_UPD, component: InstituicaoCategoriaAddUpdComponent, alias: 'Instituição Categoria - Atualizar' }

];

export const ROUTER_OPTIONS = Build.buildRouteOptions(ROUTES_CONFIG_BASE);
