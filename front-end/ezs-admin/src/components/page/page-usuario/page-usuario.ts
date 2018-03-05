import { Vue, Component, Prop } from 'vue-property-decorator';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { UsuarioModel } from '../../../../../ezs-common/src/model/server/usuario.model';
import { UsuarioInfoModel } from '../../../../../ezs-common/src/model/server/usuario-info.model';
import { CategoriaProfissionalModel } from '../../../../../ezs-common/src/model/server/categoria-profissional.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_MESSAGE } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AreaInteresseModel } from '../../../../../ezs-common/src/model/server/area-interesse.model';

interface UI {
    areaInteresse: AreaInteresseModel;
    categoriaProfissionais: Array < CategoriaProfissionalModel > ;
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
        areaInteresse: new AreaInteresseModel()
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
            this.ui.categoriaProfissionais = await Factory.CategoriaProfissionalFactory.all();
            if (this.operation === RouterPathType.upd) {
                this.model = await Factory.UsuarioFactory.detail(this.$route.params.id);
            }
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.CONSULTAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
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
                        await Factory.UsuarioFactory.add(this.model);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await Factory.UsuarioFactory.update(this.model);
                        break;
                    }
            }
            NotifyUtil.notifyI18N(I18N_MESSAGE.MODELO_SALVAR, ApplicationService.getLanguage(), NOTIFY_TYPE.SUCCESS);
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.MODELO_SALVAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    public addCategoriaProfissional(areaInteresse: AreaInteresseModel) {
        this.model.usuarioInfo.areaInteresses.push(Object.assign({}, areaInteresse));
    }

    public getColumns() {
        return [
            new CardTableColumn((item: AreaInteresseModel) => item.categoriaProfissional.descricao, () => 'Areas de Interesse'),
        ];
    }

    public getItens() {
        return this.model.usuarioInfo.areaInteresses;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => this.remove(item),
                (item) => 'Remover',
                (item) => ['fa', 'fa-times'],
                (item) => ['btn-danger']
            )
        ];
        return menu;
    }

    public remove(item) {
        this.model.usuarioInfo.areaInteresses.splice(this.model.usuarioInfo.areaInteresses.indexOf(item), 1);
    }

}