import { Vue, Component } from 'vue-property-decorator';
import Vue2Filters from 'vue2-filters';
import VueRouter from 'vue-router';
import VeeValidate from 'vee-validate';
import BootstrapVue from 'bootstrap-vue';
import VueTheMask from 'vue-the-mask';
import 'moment/locale/pt-br.js';

import { COMPONENT_GLOBAL_CONSTANT } from './module/constant/component-global.constant';
import { AppComponent } from './app';
import './sass/main.scss';

Vue.use(VueRouter);

Vue.use(BootstrapVue);

Vue.use(Vue2Filters);

Vue.use(VueTheMask);

COMPONENT_GLOBAL_CONSTANT.forEach((component) => {
    Vue.component(component.alias, component.component);
});

let app = new AppComponent({ el: '#app-main' });