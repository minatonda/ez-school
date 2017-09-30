import { RouterConfig, RouterPath, RouterPathType } from './router.path';
import { RouteConfig, RouterOptions } from 'vue-router';

export class RouterManagerBuilder {
    public static buildRouteOptions(listRouteBase: Array<RouterConfig>) {
        let options: RouterOptions = {
            routes: this.buildRouteOptionsRoutes(listRouteBase)
        };
        return options;
    }
    private static buildRouteOptionsRoutes(listRouteBase: Array<RouterConfig>) {
        let routes: Array<RouteConfig> = [];
        for (let route of listRouteBase) {
            routes.push({
                path: this.buildRouterPath(route.path as RouterPath, route.type),
                component: route.component,
                alias: route.alias,
                name: route.name,
                props: { operation: route.type, alias: route.alias }
            });
        }
        return routes;
    }
    private static buildRouterPath(path: RouterPath, pathType: RouterPathType) {
        if (pathType === RouterPathType.upd) {
            return path + '/:id';
        }
        if (pathType === RouterPathType.ext) {
            let arrayPath = path.split('/');
            arrayPath.splice(2, 0, ':id');
            return arrayPath.join('/');
        }
        else {
            return path;
        }
    }
}