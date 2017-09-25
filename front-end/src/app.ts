import * as Vue from 'vue';
import VueRouter from 'vue-router';
import Vuex from 'vuex';
import BootstrapVue from 'bootstrap-vue';
import { RouterManager } from './app/common/vue/router/router.manager';
import { BroadcastEvent } from './app/common/vue/broadcast/broadcast.events';
import { BroadcastEventBus } from './app/common/vue/broadcast/broadcast.event-bus';
import { ComponentsConfig } from './app/common/vue/global-components/components.config';
import { NavbarComponent } from './app/components/layout/navbar/navbar';
import { Store } from './app/common/vue/store/store';
import { LoaderCompactComponent } from './app/components/common/loader-compact/loader-compact';
import { RoutePath } from './app/common/vue/router/route-path';

import money from 'v-money';
Vue.use(money, {
    precision: 2,
    decimal: ',',
    thousands: '.',
    prefix: 'R$ '
});

Vue.use(Vuex);
Vue.use(VueRouter);
Vue.use(BootstrapVue);

// configure store
let store = new Vuex.Store(Store);
// configure router
let router = RouterManager.generateRouter();

// configure global components
for (let componentSpecification of ComponentsConfig.getGlobalComponents()) {
    Vue.component(componentSpecification.alias, componentSpecification.component);
}

// configure root instance
new Vue({
    el: '#app-main',
    router: router,
    store: store,
    components: {
        navbarComponent: NavbarComponent,
        loaderCompactRootComponent: LoaderCompactComponent
    },
    data: {
        showLoader: false
    },
    created: function () {
        let $data = this.$data as any;

        BroadcastEventBus.$on(BroadcastEvent.AUTENTICADO, () => {
            RouterManager.redirectRoute(RoutePath.ROOT);
        });
        BroadcastEventBus.$on(BroadcastEvent.NOT_AUTENTICADO, () => {
            RouterManager.redirectRoute(RoutePath.USUARIO_AUTENTICACAO);
        });

        BroadcastEventBus.$on(BroadcastEvent.EXIBIR_LOADER, () => {
            $data.showLoader = true;
        });
        BroadcastEventBus.$on(BroadcastEvent.ESCONDER_LOADER, () => {
            $data.showLoader = false;
        });
    },
});