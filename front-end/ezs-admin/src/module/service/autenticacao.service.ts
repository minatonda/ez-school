import { Vue, Component } from 'vue-property-decorator';
import { AutenticacaoModel } from '../../../../ezs-common/src/model/server/autenticacao.model';
import { AppRouterPath } from '../../app.router.path';
import { AutenticaoServiceInterface } from '../../../../ezs-common/src/service/autenticacao.service.interface';
import { AppRouter } from '../../app.router';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../app.broadcast-event-bus';
import { LOCAL_STORAGE_CONSTANT_ID } from '../constant/local-storage-id.contant';

interface Data {
    autenticacao: AutenticacaoModel;
    autenticado: boolean;
    views: Array < string > ;
}

@Component({})
class Service extends Vue implements AutenticaoServiceInterface {


    public autenticar(autenticao: AutenticacaoModel) {
        if (autenticao) {
            this.setAutenticacao(autenticao);
        }
        AppBroadcastEventBus.$emit(AppBroadcastEvent.AUTENTICADO);
    }

    public desautenticar() {
        this.setAutenticacao(null);
        AppBroadcastEventBus.$emit(AppBroadcastEvent.DESAUTENTICADO);
    }

    public isAutenticado() {
        return !!this.getAutenticacao();
    }

    public setAutenticacao(autenticacao: AutenticacaoModel) {
        localStorage.setItem(LOCAL_STORAGE_CONSTANT_ID.AUTENTICACAO, JSON.stringify(autenticacao || null));
    }

    public getAutenticacao() {
        return JSON.parse(localStorage.getItem(LOCAL_STORAGE_CONSTANT_ID.AUTENTICACAO)) as AutenticacaoModel;
    }

    public getToken() {
        return this.getAutenticacao().access_token;
    }

}
export const AutenticacaoService = new Service();