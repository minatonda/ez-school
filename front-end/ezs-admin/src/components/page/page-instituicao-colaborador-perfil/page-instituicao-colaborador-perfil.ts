import { Vue, Component, Prop } from 'vue-property-decorator';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';
import { InstituicaoColaboradorPerfilModel } from '../../../../../ezs-common/src/model/server/instituicao-colaborador-perfil.model';
import { UsuarioInfoModel } from '../../../../../ezs-common/src/model/server/usuario-info.model';
import { ENUM_CONTANT } from '../../../../../ezs-common/src/constant/enum.contant';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { TreeViewModel } from '../../../../../ezs-common/src/model/server/tree-view.model';
import { BaseModel } from '../../../../../ezs-common/src/model/server/base.model';
import { AppRouterPath } from '../../../app.router.path';

enum ModalOperation {
    add = 0,
    update = 1
}

interface UI {
    queryUsuario: any;
    usuarioInfoLabel: any;
    rolesLabel: any;
    roles: TreeViewModel<string>;
}

@Component({
    template: require('./page-instituicao-colaborador-perfil.html')
})
export class PageInstituicaoColaboradorPerfilComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    ui: UI = {
        roles: undefined,

        rolesLabel(item: BaseModel<string>) {
            return ApplicationService.getEnumLabels(ENUM_CONTANT.BASE_ROLE).find(x => {
                return x.value === item.id;
            }).label;
        },

        queryUsuario: async (term) => {
            let itens = await FACTORY_CONSTANT.UsuarioFactory.allByTermo(term, false, false);
            return itens;
        },

        usuarioInfoLabel: (item: UsuarioInfoModel) => {
            let labelObj = {} as any;
            labelObj.key = item.label;
            labelObj.label = `<div><span>${item.label}</span><div><div><span style="float:left;">${item.rg}</span><span style="float:right;">${item.cpf}</span></div>`;
            return labelObj;
        }
    };

    model: InstituicaoColaboradorPerfilModel = new InstituicaoColaboradorPerfilModel();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.ui.roles = await FACTORY_CONSTANT.InstituicaoFactory.allRoles();
            if (this.operation === RouterPathType.upd) {
                this.model = await FACTORY_CONSTANT.InstituicaoFactory.detailInstituicaoColaboradorPerfil(this.$route.params.id, this.$route.params.idInstituicaoColaboradorPerfil);
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
                        await FACTORY_CONSTANT.InstituicaoFactory.addInstituicaoColaboradorPerfil(this.$route.params.id, this.model);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await FACTORY_CONSTANT.InstituicaoFactory.updateInstituicaoColaboradorPerfil(this.$route.params.id, this.model);
                        break;
                    }
            }
            NotifyUtil.successG(I18N_ERROR_GENERIC.MODELO_SALVAR, ApplicationService.getLanguage());
            AppRouter.push({ name: AppRouterPath.INSTITUICAO_COLABORADOR_PERFIL, params: { id: this.$route.params.id } });
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage(), I18N_ERROR_GENERIC.MODELO_SALVAR_FALHA);
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

}