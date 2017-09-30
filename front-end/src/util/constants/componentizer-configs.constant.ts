import { Component } from 'vue';
import { LoaderCompactComponent } from '../../components/common/loader-compact/loader-compact';
import { CardTableComponent } from '../../components/common/card-table/card-table';
import { CursoGradeMngComponent } from '../../components/curso/add-upd/mng/curso-grade-mng';
import { ComponentizerConfig } from '../componentizer/componentizer.config';
import Multiselect from 'vue-multiselect';

export const COMPONENTIZER_CONFIGS_GLOBAL: Array<ComponentizerConfig> = [
    { alias: 'loader-compact-component', component: LoaderCompactComponent },
    { alias: 'card-table-component', component: CardTableComponent },
    { alias: 'curso-grade-mng-component', component: CursoGradeMngComponent },
    { alias: 'multiselect', component: Multiselect }
];