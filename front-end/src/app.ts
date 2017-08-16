import * as Vue from 'vue';
import VueRouter from 'vue-router';
import Vuex from 'vuex';
import BootstrapVue from 'bootstrap-vue';
import { RouterConfig, RoutePath } from './app/common/vue/router/router.config';
import { RouterManager } from './app/common/vue/router/router.manager';
import { BroadcastEvent } from './app/common/vue/broadcast/broadcast.events';
import { BroadcastEventBus } from './app/common/vue/broadcast/broadcast.event-bus';
import { ComponentsConfig } from './app/common/vue/global-components/components.config';
import { NavbarComponent } from './app/components/layout/navbar/navbar';

Vue.use(VueRouter);
Vue.use(BootstrapVue);

// configure router
let router = new VueRouter(RouterConfig);
RouterManager.configureRouter(router);

// configure global components
for (let componentSpecification of ComponentsConfig.getGlobalComponents()) {
    Vue.component(componentSpecification.alias, componentSpecification.component);
}

// configure root instance
new Vue({
    el: '#app-main',
    router: router,
    components: {
        navbarComponent: NavbarComponent
    },
    beforeCreate: function () {
        BroadcastEventBus.$on(BroadcastEvent.AUTENTICADO, function () {
            RouterManager.redirectRoute(RoutePath.ROOT);
        });
        BroadcastEventBus.$on(BroadcastEvent.NOT_AUTENTICADO, function () {
            RouterManager.redirectRoute(RoutePath.USUARIO_AUTENTICACAO);
        });
    },
});