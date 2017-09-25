import { RoutePathType } from './route-path-type';
import { RouteConfig } from 'vue-router/types';

export interface RouteConfigBase extends RouteConfig {
    type: RoutePathType;
}