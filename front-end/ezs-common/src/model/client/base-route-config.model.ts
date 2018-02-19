import { RouteConfig } from 'vue-router';
import { RouterPathType } from './router-path-type.model';

export interface BaseRouteConfig extends RouteConfig {
    type?: RouterPathType;
    menu?: boolean;
}