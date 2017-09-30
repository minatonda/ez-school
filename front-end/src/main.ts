import { Vue } from 'vue-property-decorator';
import VueRouter from 'vue-router';
import BootstrapVue from 'bootstrap-vue';
import { ROUTER_CONFIGS_CONSTANT } from './util/constants/router-configs.constant';
import { COMPONENTIZER_CONFIGS_GLOBAL } from './util/constants/componentizer-configs.constant';
import { RouterManager } from './util/router/router.manager';
import { NavbarComponent } from './components/layout/navbar/navbar';
import { LoaderCompactComponent } from './components/common/loader-compact/loader-compact';
import { ComponentizerManager } from './util/componentizer/componentizer.manager';
import { BroadcastEventBus, BroadcastEvent } from './util/broadcast/broadcast.event-bus';
import { RouterPath } from './util/router/router.path';

import './sass/main.scss';

Vue.use(VueRouter);
Vue.use(BootstrapVue);

// configure router
let router = RouterManager.generateRouter(ROUTER_CONFIGS_CONSTANT);
ComponentizerManager.registerGlobal(COMPONENTIZER_CONFIGS_GLOBAL);
// configure global components

// configure root instance
new Vue({
  el: '#app-main',
  router: router,
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
      RouterManager.redirectRoute(RouterPath.ROOT);
    });
    BroadcastEventBus.$on(BroadcastEvent.NOT_AUTENTICADO, () => {
      RouterManager.redirectRoute(RouterPath.USUARIO_AUTENTICACAO);
    });

    BroadcastEventBus.$on(BroadcastEvent.EXIBIR_LOADER, () => {
      $data.showLoader = true;
    });
    BroadcastEventBus.$on(BroadcastEvent.ESCONDER_LOADER, () => {
      $data.showLoader = false;
    });

  },
});