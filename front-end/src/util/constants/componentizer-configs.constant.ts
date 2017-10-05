import { Component } from 'vue';
import { LoaderCompactComponent } from '../../components/common/loader-compact/loader-compact';
import { CardTableComponent } from '../../components/common/card-table/card-table';
import { ComponentizerConfig } from '../componentizer/componentizer.config';
import { CursoGradeManagementComponent } from '../../components/curso/curso-grade/management/curso-grade-management';
import Multiselect from 'vue-multiselect';

export const COMPONENTIZER_CONFIGS_GLOBAL: Array<ComponentizerConfig> = [
    { alias: 'loader-compact-component', component: LoaderCompactComponent },
    { alias: 'card-table-component', component: CardTableComponent },
    { alias: 'curso-grade-management-component', component: CursoGradeManagementComponent },
    { alias: 'multiselect', component: Multiselect }
];