import { Vue } from 'vue-property-decorator';
import { NavbarComponent } from './components/layout/navbar/navbar';
import { LoaderCompactComponent } from './components/common/loader-compact/loader-compact';
import { BroadcastEventBus, BroadcastEvent } from './module/broadcast.event-bus';
import { Router } from './router';
import { RouterPath } from './module/model/client/route-path';
import { COMPONENTS_ROUTE_CONSTANT } from './module/constant/components-route.constant';
import { COMPONENTS_GLOBAL_CONSTANT } from './module/constant/components-global.constant';

import VueRouter from 'vue-router';
import BootstrapVue from 'bootstrap-vue';

import './sass/main.scss';

Vue.use(VueRouter);
Vue.use(BootstrapVue);

COMPONENTS_GLOBAL_CONSTANT.forEach((component) => {
    Vue.component(component.alias, component.component);
});

new Vue({
    el: '#app-main',
    router: Router.generateRouter(COMPONENTS_ROUTE_CONSTANT),
    components: {
        navbarComponent: NavbarComponent,
        loaderCompactRootComponent: LoaderCompactComponent
    },
    data: {
        showLoader: false
    },
    created: function() {

        BroadcastEventBus.$on(BroadcastEvent.EXIBIR_LOADER, () => {
            (this as any).showLoader = true;
        });
        BroadcastEventBus.$on(BroadcastEvent.ESCONDER_LOADER, () => {
            (this as any).showLoader = false;
        });

    },
});