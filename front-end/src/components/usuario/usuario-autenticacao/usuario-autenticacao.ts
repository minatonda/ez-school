import { Vue } from 'vue-property-decorator';
import { Component } from 'vue-property-decorator';
import { BroadcastEventBus, BroadcastEvent } from '../../../util/broadcast/broadcast.event-bus';
import { UsuarioFactory } from '../../../util/factory/usuario/usuario.factory';
import { Usuario } from '../../../util/factory/usuario/usuario';
import { AutenticacaoService } from '../../../util/service/autenticacao/autenticacao.service';

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