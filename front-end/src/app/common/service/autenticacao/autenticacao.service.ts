import { BroadcastEventBus } from '../../vue/broadcast/broadcast.event-bus';
import { BroadcastEvent } from '../../vue/broadcast/broadcast.events';
import { UsuarioFactory } from "../../factory/usuario/usuario.factory";

export class AutenticacaoService {

    private static usuarioInfo: Object;

    public static async autenticar(autenticao: Object) {
        this.usuarioInfo = new Object();
        let retorno = UsuarioFactory.autenticar();
        try {
            let retorno = await UsuarioFactory.autenticar();
            BroadcastEventBus.$emit(BroadcastEvent.AUTENTICADO);
        }
        catch (error) {

        }
    }

    public static desautenticar() {
        this.usuarioInfo = undefined;
        BroadcastEventBus.$emit(BroadcastEvent.NOT_AUTENTICADO);
    }

    public static isAutenticado() {
        return this.usuarioInfo ? true : false;
    }

}