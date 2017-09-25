import { VueRouter, RouteConfig } from 'vue-router/types/router';
import { Vue } from 'vue/types/vue';
import { HomeComponent } from '../../../components/home/home';
import { UsuarioAutenticacaoComponent } from '../../../components/usuario/usuario-autenticacao/usuario-autenticacao';
import { RouterOptions } from 'vue-router/types';
import { RouteConfigBase } from './route-config-base';
import { RoutePathType } from './route-path-type';
import { RoutePath } from './route-path';

import { UsuarioComponent } from '../../../components/usuario/usuario';
import { UsuarioAddUpdComponent } from '../../../components/usuario/add-upd/usuario-add-upd';

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

    { type: RoutePathType.list, path: RoutePath.USUARIO, name: RoutePath.USUARIO, component: UsuarioComponent, alias: 'Usuário - Listar' },
    { type: RoutePathType.add, path: RoutePath.USUARIO_ADD, name: RoutePath.USUARIO_ADD, component: UsuarioAddUpdComponent, alias: 'Usuário - Adicionar' },
    { type: RoutePathType.upd, path: RoutePath.USUARIO_UPD, name: RoutePath.USUARIO_UPD, component: UsuarioAddUpdComponent, alias: 'Usuário - Atualizar' },
    { type: RoutePathType.otr, path: RoutePath.USUARIO_AUTENTICACAO, name: RoutePath.USUARIO_AUTENTICACAO, component: UsuarioAutenticacaoComponent, alias: 'Autenticação' }
];

export const ROUTER_OPTIONS = Build.buildRouteOptions(ROUTES_CONFIG_BASE);
