import { Component } from 'vue/types';

export class ComponentSpecification {

    constructor(alias: string, component: Component) {
        this.alias = alias;
        this.component = component;
    }

    alias: string;
    component: Component;
}