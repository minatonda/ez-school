import { Vue, Component, Prop } from 'vue-property-decorator';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { CategoriaProfissionalModel } from '../../../../../ezs-common/src/model/server/categoria-profissional.model';
import { AlunoModel } from '../../../../../ezs-common/src/model/server/aluno.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_MESSAGE } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';

interface UI {
    categoriaProfissional: CategoriaProfissionalModel;
    categoriaProfissionais: Array<CategoriaProfissionalModel>;
}

@Component({
    template: require('./page-usuario-aluno.html')
})
export class PageUsuarioAlunoComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: AlunoModel = new AlunoModel();
    ui: UI = { categoriaProfissionais: new Array<CategoriaProfissionalModel>(), categoriaProfissional: undefined };
    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.model = await Factory.UsuarioFactory.detailAluno(this.$route.params.id);
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
            await Factory.UsuarioFactory.updateAluno(this.$route.params.id, this.model);
            NotifyUtil.notifyI18N(I18N_MESSAGE.MODELO_SALVAR, ApplicationService.getLanguage(), NOTIFY_TYPE.SUCCESS);
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.MODELO_SALVAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    public addCategoriaProfissional(categoriaProfissional: CategoriaProfissionalModel) {
        this.model.categoriaProfissionais.push(categoriaProfissional);
    }

    public getColumns() {
        return [
            new CardTableColumn((item: CategoriaProfissionalModel) => item.descricao, () => 'Areas de Interesse'),
        ];
    }

    public getItens() {
        return this.model.categoriaProfissionais;
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
        this.model.categoriaProfissionais.splice(this.model.categoriaProfissionais.indexOf(item), 1);
    }

}