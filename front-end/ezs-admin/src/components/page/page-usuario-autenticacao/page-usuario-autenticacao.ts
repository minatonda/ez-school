import { Vue, Component } from 'vue-property-decorator';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { AutenticacaoService } from '../../../module/service/autenticacao.service';
import { Factory } from '../../../module/constant/factory.constant';
import { UsuarioModel } from '../../../../../ezs-common/src/model/server/usuario.model';

@Component({
    template: require('./page-usuario-autenticacao.html')
})
export class PageUsuarioAutenticacaoComponent extends Vue {

    public model = new UsuarioModel();
    constructor() {
        super();
    }

    mounted() {

    }

    public async autenticar() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            let retorno = await Factory.UsuarioFactory.autenticar(this.model);
            AutenticacaoService.autenticar(retorno);
        }
        catch (e) {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }
}