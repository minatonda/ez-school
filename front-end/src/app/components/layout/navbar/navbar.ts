import Vue from 'vue';
import { Component, watch } from 'vue-property-decorator';
import { Link } from './link';
import { BroadcastEventBus } from '../../../common/vue/broadcast/broadcast.event-bus';
import { BroadcastEvent } from '../../../common/vue/broadcast/broadcast.events';
import { AutenticacaoService } from '../../../common/service/autenticacao/autenticacao.service';

@Component({
    template: require('./navbar.html')
})
export class NavbarComponent extends Vue {

    mounted() {
        BroadcastEventBus.$on(BroadcastEvent.AUTENTICADO, () => {
            this.autenticado = true;
        });
        BroadcastEventBus.$on(BroadcastEvent.NOT_AUTENTICADO, () => {
            this.autenticado = false;
        });
    }

    private autenticado = AutenticacaoService.isAutenticado();

    public desautenticar() {
        AutenticacaoService.desautenticar();
    }

    public isAutenticado() {
        return this.autenticado;
    }
    
}