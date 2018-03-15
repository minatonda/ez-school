import { Vue, Component, Prop } from 'vue-property-decorator';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';

@Component({
    template: require('./page-instituicao.html')
})
export class PageInstituicaoComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: InstituicaoModel = new InstituicaoModel();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RouterPathType.upd) {
                this.model = await FACTORY_CONSTANT.InstituicaoFactory.detail(this.$route.params.id);
            }
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
            AppRouter.back();
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    async save() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RouterPathType.add):
                    {
                        await FACTORY_CONSTANT.InstituicaoFactory.add(this.model);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await FACTORY_CONSTANT.InstituicaoFactory.update(this.model);
                        break;
                    }
            }
            NotifyUtil.successG(I18N_ERROR_GENERIC.MODELO_SALVAR, ApplicationService.getLanguage());
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

}