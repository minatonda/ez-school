import { Vue, Component, Prop } from 'vue-property-decorator';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { InstituicaoCategoriaModel } from '../../../../../ezs-common/src/model/server/instituicao-categoria.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';
import { AppRouterPath } from '../../../app.router.path';

@Component({
    template: require('./page-instituicao-categoria.html')
})
export class PageInstituicaoCategoriaComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: InstituicaoCategoriaModel = new InstituicaoCategoriaModel();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RouterPathType.upd) {
                this.model = await FACTORY_CONSTANT.InstituicaoCategoriaFactory.detail(this.$route.params.id);
            }
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage(), I18N_ERROR_GENERIC.CONSULTAR_FALHA);
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
                        await FACTORY_CONSTANT.InstituicaoCategoriaFactory.add(this.model);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await FACTORY_CONSTANT.InstituicaoCategoriaFactory.update(this.model);
                        break;
                    }
            }
            NotifyUtil.successG(I18N_ERROR_GENERIC.MODELO_SALVAR, ApplicationService.getLanguage());
            AppRouter.push(AppRouterPath.INSTITUICAO);
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage(), I18N_ERROR_GENERIC.MODELO_SALVAR_FALHA);
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

}