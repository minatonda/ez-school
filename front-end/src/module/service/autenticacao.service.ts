import { Vue, Component } from 'vue-property-decorator';
import { Autenticacao } from '../model/server/autenticacao';
import { BroadcastEvent, BroadcastEventBus } from '../broadcast.event-bus';
import { Router } from '../../router';
import { RouterPath } from '../model/client/route-path';

interface Data {
    autenticacaoInfo: Autenticacao;
}

@Component({})
class Service extends Vue {

    data: Data = {
        autenticacaoInfo: undefined
    };


    public autenticar(autenticao: Autenticacao) {
        this.data.autenticacaoInfo = autenticao;
        Router.redirectRoute(RouterPath.ROOT);
    }

    public desautenticar() {
        this.data.autenticacaoInfo = undefined;
        Router.redirectRoute(RouterPath.USUARIO_AUTENTICACAO);
    }

    public isAutenticado() {
        return !!this.data.autenticacaoInfo;
    }

    public getToken() {
        return this.data.autenticacaoInfo.access_token;
    }

}
export const AutenticacaoService = new Service();