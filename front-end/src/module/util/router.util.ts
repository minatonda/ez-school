import { RouteConfig, RouterOptions } from 'vue-router';
import { RouterConfig } from '../model/client/route-path';

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
                path: route.path,
                component: route.component,
                alias: route.alias,
                name: route.name,
                props: { operation: route.type, alias: route.alias }
            });
        }
        return routes;
    }
}