import { Component } from 'vue';
import { LoaderCompactComponent } from '../../components/common/loader-compact/loader-compact';
import { CardTableComponent } from '../../components/common/card-table/card-table';
import { CursoGradeManagementComponent } from '../../components/curso/curso-grade/management/curso-grade-management';
import { SelectorComponent } from '../../components/common/selector/selector';
import { ComponentConfiguration } from '../model/client/component-configuration';
import Datepicker from 'vuejs-datepicker';

export const COMPONENTS_GLOBAL_CONSTANT: Array < ComponentConfiguration > = [
    { alias: 'loader-compact-component', component: LoaderCompactComponent },
    { alias: 'card-table-component', component: CardTableComponent },
    { alias: 'curso-grade-management-component', component: CursoGradeManagementComponent },
    { alias: 'selector', component: SelectorComponent },
    { alias: 'datepicker', component: Datepicker }
];