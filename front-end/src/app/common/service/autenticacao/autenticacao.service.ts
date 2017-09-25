import { BroadcastEventBus } from '../../vue/broadcast/broadcast.event-bus';
import { BroadcastEvent } from '../../vue/broadcast/broadcast.events';
import { UsuarioFactory } from '../../factory/usuario/usuario.factory';
import { Usuario } from '../../factory/usuario/usuario';
import { Autenticacao } from '../../factory/usuario/autenticacao';

export class AutenticacaoService {

    private static autenticacaoInfo: Autenticacao;

    public static autenticar(autenticao: Autenticacao) {
        this.autenticacaoInfo = autenticao;
        BroadcastEventBus.$emit(BroadcastEvent.AUTENTICADO);
    }

    public static desautenticar() {
        this.autenticacaoInfo = undefined;
        BroadcastEventBus.$emit(BroadcastEvent.NOT_AUTENTICADO);
    }

    public static isAutenticado() {
        return this.autenticacaoInfo ? true : false;
    }

    public static getToken() {
        return this.autenticacaoInfo.access_token;
    }

}