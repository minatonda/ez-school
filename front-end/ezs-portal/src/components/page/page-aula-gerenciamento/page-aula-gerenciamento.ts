import { Vue, Component } from 'vue-property-decorator';
import { AlunoModel } from '../../../../../ezs-common/src/model/server/aluno.model';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { AppRouter } from '../../../app.router';
import { I18N_MESSAGE } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { ApplicationService } from '../../../module/service/application.service';
import { Factory } from '../../../module/constant/factory.constant';


interface UI {
    alunos: Array < AlunoModel > ;
    observacao: string;
}

@Component({
    template: require('./page-aula-gerenciamento.html')
})
export class PageAulaGerenciamentoComponent extends Vue {

    ui: UI = {
        alunos: undefined,
        observacao: undefined
    };

    async created() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.ui.alunos = await Factory.UsuarioFactory.allAluno('m');
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.CONSULTAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
            AppRouter.back();
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

}