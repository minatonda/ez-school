import { Vue, Component } from 'vue-property-decorator';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { AutenticacaoService } from '../../../module/service/autenticacao.service';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { UsuarioModel } from '../../../../../ezs-common/src/model/server/usuario.model';

@Component({
    template: require('./page-autenticacao.html')
})
export class PageAutenticacaoComponent extends Vue {

    public model = new UsuarioModel();
    constructor() {
        super();
    }

    mounted() {

    }

    public async autenticar() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            let retorno = await FACTORY_CONSTANT.UsuarioFactory.autenticar(this.model);
            AutenticacaoService.autenticar(retorno);
        }
        catch (e) {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }
}