import { Vue, Component } from 'vue-property-decorator';
import { Autenticacao } from '../model/server/autenticacao';
import { BroadcastEvent, BroadcastEventBus } from '../broadcast.event-bus';
import { Router } from '../../router';
import { RouterPath } from '../model/client/route-path';

interface Data {
    autenticacao: Autenticacao;
    autenticado: boolean;
}

@Component({})
class Service extends Vue {

    data: Data = {
        autenticacao: undefined,
        autenticado: false
    };

    public autenticar(autenticao?: Autenticacao) {
        if (autenticao) {
            this.setAutenticacao(autenticao);
        }
        this.data.autenticado = true;
        Router.redirectRoute(RouterPath.ROOT);
    }

    public desautenticar() {
        this.setAutenticacao(undefined);
        this.data.autenticado = true;
        Router.redirectRoute(RouterPath.USUARIO_AUTENTICACAO);
    }

    public isAutenticado() {
        return this.data.autenticado && !!this.getAutenticacao();
    }

    public setAutenticacao(autenticacao: Autenticacao) {
        localStorage.setItem('autenticacao', JSON.stringify(autenticacao));
    }

    public getAutenticacao() {
        return this.data.autenticacao || JSON.parse(localStorage.getItem('autenticacao')) as Autenticacao;
    }

    public getToken() {
        return this.getAutenticacao().access_token;
    }

}
export const AutenticacaoService = new Service();