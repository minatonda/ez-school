import { BroadcastEventBus } from '../../vue/broadcast/broadcast.event-bus';
import { BroadcastEvent } from '../../vue/broadcast/broadcast.events';
import { UsuarioInfoViewModel } from '../../view-model/usuario/usuario-info.viewmodel';
import { UsuarioAutenticacaoViewModel } from '../../view-model/usuario/usuario-autenticacao.viewmodel';

export class AutenticacaoService {

    private static usuarioInfo: UsuarioInfoViewModel;

    public static autenticar(autenticao: UsuarioAutenticacaoViewModel) {
        this.usuarioInfo = new UsuarioInfoViewModel();
        BroadcastEventBus.$emit(BroadcastEvent.AUTENTICADO);
    }

    public static desautenticar() {
        this.usuarioInfo = undefined;
        BroadcastEventBus.$emit(BroadcastEvent.NOT_AUTENTICADO);
    }

    public static isAutenticado() {
        return this.usuarioInfo ? true : false;
    }

}