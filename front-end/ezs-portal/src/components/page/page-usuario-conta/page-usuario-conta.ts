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
import { TreeViewModel } from '../../../../../ezs-common/src/model/server/tree-view.model';
import { BaseModel } from '../../../../../ezs-common/src/model/server/base.model';
import { AppRouterPath } from '../../../app.router.path';

interface UI {
    categoriaProfissionais: Array<CategoriaProfissionalModel>;
    areaInteresse: AreaInteresseModel;
    query: any;
    usuarioInfoLabel: any;
}

@Component({
    template: require('./page-usuario-conta.html')
})
export class PageUsuarioContaComponent extends Vue {

    @Prop()
    alias: string;

    ui: UI = {
        categoriaProfissionais: undefined,
        areaInteresse: new AreaInteresseModel(),
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
            this.model = await FACTORY_CONSTANT.UsuarioFactory.detail(ApplicationService.getUsuarioInfo().id);
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
            await FACTORY_CONSTANT.UsuarioFactory.update(this.model);
            NotifyUtil.successG(I18N_ERROR_GENERIC.MODELO_SALVAR, ApplicationService.getLanguage());
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage(), I18N_ERROR_GENERIC.MODELO_SALVAR_FALHA);
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

}