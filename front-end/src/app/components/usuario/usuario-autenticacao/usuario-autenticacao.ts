import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { AutenticacaoService } from '../../../common/service/autenticacao/autenticacao.service';
import { Usuario } from '../../../common/factory/usuario/usuario';
import { BroadcastEventBus } from '../../../common/vue/broadcast/broadcast.event-bus';
import { BroadcastEvent } from '../../../common/vue/broadcast/broadcast.events';
import { UsuarioFactory } from '../../../common/factory/usuario/usuario.factory';

@Component({
    template: require('./usuario-autenticacao.html')
})
export class UsuarioAutenticacaoComponent extends Vue {

    public model = new Usuario();
    constructor() {
        super();
    }

    mounted() {

    }

    public async autenticar() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            let retorno = await UsuarioFactory.autenticar(this.model, true);
            AutenticacaoService.autenticar(retorno);
        }
        catch (e) {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }
}