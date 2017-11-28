import { Vue } from 'vue-property-decorator';
import { Component } from 'vue-property-decorator';
import { BroadcastEventBus, BroadcastEvent } from '../../../module/broadcast.event-bus';
import { AutenticacaoService } from '../../../module/service/autenticacao.service';
import { UsuarioFactory } from '../../../module/factory/usuario.factory';
import { Usuario } from '../../../module/model/server/usuario';

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