import { Vue, Component } from 'vue-property-decorator';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { AutenticacaoService } from '../../../module/service/autenticacao.service';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { UsuarioModel } from '../../../../../ezs-common/src/model/server/usuario.model';
import { NotifyUtil } from '../../../../../ezs-common/src/util/notify/notify.util';
import { ApplicationService } from '../../../module/service/application.service';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';

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
            let retorno = await FACTORY_CONSTANT.UsuarioFactory.autenticar(this.model);
            AutenticacaoService.autenticar(retorno);
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage(), I18N_ERROR_GENERIC.AUTENTICAR_FALHA);
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }
}