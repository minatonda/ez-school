import { Vue, Component } from 'vue-property-decorator';
import { AutenticacaoModel } from '../../../../ezs-common/src/model/server/autenticacao.model';
import { AppRouterPath } from '../../app.router.path';
import { AutenticaoServiceInterface } from '../../../../ezs-common/src/service/autenticacao.service.interface';
import { AppRouter } from '../../app.router';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../app.broadcast-event-bus';

interface Data {
    autenticacao: AutenticacaoModel;
    autenticado: boolean;
}

@Component({})
class Service extends Vue implements AutenticaoServiceInterface {

    data: Data = {
        autenticacao: undefined,
        autenticado: false
    };

    public autenticar(autenticao?: AutenticacaoModel) {
        if (autenticao) {
            this.setAutenticacao(autenticao);
        }
        this.data.autenticado = true;
        AppBroadcastEventBus.$emit(AppBroadcastEvent.AUTENTICADO);
    }

    public desautenticar() {
        this.setAutenticacao(undefined);
        this.data.autenticado = false;
        AppBroadcastEventBus.$emit(AppBroadcastEvent.DESAUTENTICADO);
    }

    public isAutenticado() {
        return this.data.autenticado && !!this.getAutenticacao();
    }

    public setAutenticacao(autenticacao: AutenticacaoModel) {
        localStorage.setItem('autenticacao', JSON.stringify(autenticacao));
    }

    public getAutenticacao() {
        return this.data.autenticacao || JSON.parse(localStorage.getItem('autenticacao')) as AutenticacaoModel;
    }

    public getToken() {
        return this.getAutenticacao().access_token;
    }

}
export const AutenticacaoService = new Service();