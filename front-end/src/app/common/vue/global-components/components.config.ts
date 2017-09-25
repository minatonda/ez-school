import { Component } from 'vue/types';
import { ComponentSpecification } from './component-specification.viewmodel';
import { CardTableComponent } from '../../../components/common/card-table/card-table';
import { LoaderCompactComponent } from '../../../components/common/loader-compact/loader-compact';
import Multiselect from 'vue-multiselect';

export class ComponentsConfig {

    public static getGlobalComponents() {
        return [
            new ComponentSpecification('loader-compact-component', LoaderCompactComponent),
            new ComponentSpecification('card-table-component', CardTableComponent),
            new ComponentSpecification('multiselect', Multiselect),
        ];
    }

}