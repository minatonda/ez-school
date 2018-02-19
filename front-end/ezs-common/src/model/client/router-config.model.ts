import { RouteConfig } from "vue-router";
import { RouterPathType } from "./router-path-type.model";

export interface RouterConfig extends RouteConfig {
    type: RouterPathType;
    menu: boolean;
}