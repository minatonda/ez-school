import { Vue, Component, Prop } from 'vue-property-decorator';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { UsuarioModel } from '../../../../../ezs-common/src/model/server/usuario.model';
import { UsuarioInfoModel } from '../../../../../ezs-common/src/model/server/usuario-info.model';
import { CategoriaProfissionalModel } from '../../../../../ezs-common/src/model/server/categoria-profissional.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AreaInteresseModel } from '../../../../../ezs-common/src/model/server/area-interesse.model';
import { ENUM_CONTANT } from '../../../../../ezs-common/src/constant/enum.contant';

interface UI {
    areaInteresse: AreaInteresseModel;
    categoriaProfissionais: Array<CategoriaProfissionalModel>;
    query: any;
    usuarioInfoLabel: any;
    rolesLabel: any;
    roles: Array<number>;
}

@Component({
    template: require('./page-usuario.html')
})
export class PageUsuarioComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    ui: UI = {
        categoriaProfissionais: undefined,
        areaInteresse: new AreaInteresseModel(),

        roles: undefined,

        rolesLabel(item: number) {
            return ApplicationService.getEnumLabels(ENUM_CONTANT.BASE_ROLE).find(x => {
                return x.value === parseInt(item.toString());
            }).label;
        },

        query: async (term) => {
            let itens = await FACTORY_CONSTANT.UsuarioFactory.allByTermo(term, false, false);
            return itens;
        },

        usuarioInfoLabel: (item: UsuarioInfoModel) => {
            let labelObj = {} as any;
            labelObj.key = item.label;
            labelObj.label = `<div><span>${item.label}</span><div><div><span style="float:left;">${item.rg}</span><span style="float:right;">${item.cpf}</span></div>`;
            return labelObj;
        },
    };

    model: UsuarioModel = new UsuarioModel();

    constructor() {
        super();
    }

    created() {
        this.model.usuarioInfo = new UsuarioInfoModel();
    }

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.ui.categoriaProfissionais = await FACTORY_CONSTANT.CategoriaProfissionalFactory.all();
            this.ui.roles = await FACTORY_CONSTANT.UsuarioFactory.allRoles();
            if (this.operation === RouterPathType.upd) {
                this.model = await FACTORY_CONSTANT.UsuarioFactory.detail(this.$route.params.id);
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
                        await FACTORY_CONSTANT.UsuarioFactory.add(this.model);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await FACTORY_CONSTANT.UsuarioFactory.update(this.model);
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

    addCategoriaProfissional(areaInteresse: AreaInteresseModel) {
        this.model.usuarioInfo.areaInteresses.push(Object.assign({}, areaInteresse));
        this.$forceUpdate();
    }

    removeAreaInteresse(item) {
        this.model.usuarioInfo.areaInteresses.splice(this.model.usuarioInfo.areaInteresses.indexOf(item), 1);
        this.$forceUpdate();
    }

    addRole(enumerable: any) {
        this.model.usuarioInfo.roles.push(enumerable);
        this.$forceUpdate();
    }

    removeRole(item) {
        this.model.usuarioInfo.roles.splice(this.model.usuarioInfo.roles.indexOf(item), 1);
        this.$forceUpdate();
    }

    getTableAreaInteresse() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry({
                label: (item) => 'Remover',
                method: (item) => this.removeAreaInteresse(item),
                btnClass: (item) => ['btn-danger'],
                iconClass: (item) => ['fa', 'fa-times']
            })
        ];
        let columns = [
            new CardTableColumn({
                value: (item: AreaInteresseModel) => item.categoriaProfissional.descricao,
                label: () => 'Aréa de Interesse'
            }),
        ];
        return { columns: columns, menu: menu };
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