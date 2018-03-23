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

enum ModalOperation {
    add = 0,
    update = 1
}

interface UI {
    queryUsuario: any;
    usuarioInfoLabel: any;
    rolesLabel: any;
    roles: Array<number>;
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

        rolesLabel(item: number) {
            return ApplicationService.getEnumLabels(ENUM_CONTANT.BASE_ROLE).find(x => {
                return x.value === parseInt(item.toString());
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
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    addRole(enumerable: any) {
        this.model.roles.push(enumerable);
        this.$forceUpdate();
    }

    removeRole(item) {
        this.model.roles.splice(this.model.roles.indexOf(item), 1);
        this.$forceUpdate();
    }

    getTableRole() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry({
                label: (item) => 'Remover',
                method: (item) => this.removeRole(item),
                btnClass: (item) => ['btn-danger'],
                iconClass: (item) => ['fa', 'fa-times']
            })
        ];
        let columns = [
            new CardTableColumn({
                value: (item: number) => {
                    return this.ui.rolesLabel(item);
                },
                label: () => 'Regra'
            })
        ];
        return { columns: columns, menu: menu };
    }

}