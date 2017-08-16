import { Component } from 'vue/types';
import { ComponentSpecification } from './component-specification.viewmodel';
import { CardTableComponent } from '../../../components/common/card-table/card-table';

export class ComponentsConfig {

    public static getGlobalComponents() {
        return [
            new ComponentSpecification('card-table', CardTableComponent)
        ];
    }

}