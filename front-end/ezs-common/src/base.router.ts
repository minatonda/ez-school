import VueRouter, { RouteConfig } from 'vue-router';
import { RouterOptions } from 'vue-router/types/router';
import { BaseRouteConfig } from './model/client/base-route-config.model';
import { AutenticaoServiceInterface } from './service/autenticacao.service.interface';

export class BaseRouter extends VueRouter {

    protected configs: Array<BaseRouteConfig>;

    constructor(config: Array<BaseRouteConfig>) {
        super();
        config.forEach(x => {
            x.props = Object.assign({ operation: x.type, alias: x.alias }, x.props);
        });
        this.configs = config;
        this.addRoutes(config);
    }

    public getRouteConfigs = () => {
        return this.configs;
    }

    public getRouteConfigByPath = (path: string) => {
        return this.configs.find(x => x.path === path || x.name === path);
    }

    public getMenu = () => {
        return this.configs.filter(x => x.menu);
    }

}