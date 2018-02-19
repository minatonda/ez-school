import { Vue, Component, Prop } from 'vue-property-decorator';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { MateriaModel } from '../../../../../ezs-common/src/model/server/materia.model';
import { MateriaRelacionamentoModel } from '../../../../../ezs-common/src/model/server/materia-relacionamento.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_MESSAGE } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';

interface UI {
    materiaRelacionada: MateriaRelacionamentoModel;
    materias: Array<MateriaModel>;
}

@Component({
    template: require('./page-materia.html')
})
export class PageMateriaComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;
    
    model: MateriaModel = new MateriaModel();
    ui: UI = { materias: new Array<MateriaModel>(), materiaRelacionada: new MateriaRelacionamentoModel() };
    
    constructor() {
        super();
    }

    created() {

    }
    
    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RouterPathType.upd) {
                this.model = await Factory.MateriaFactory.detail(this.$route.params.id); 
            }
            this.ui.materias = await Factory.MateriaFactory.all();
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
                case (RouterPathType.add): {
                    await Factory.MateriaFactory.add(this.model);
                    break;
                }
                case (RouterPathType.upd): {
                    await Factory.MateriaFactory.update(this.model);
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
    
    public addMateriaRelacionada(materiaRelacionada: MateriaRelacionamentoModel) {
        this.model.materiasRelacionadas.push(Object.assign(new MateriaRelacionamentoModel(), materiaRelacionada));
    }

    public getColumns() {
        return [
            new CardTableColumn((item: MateriaRelacionamentoModel) => item.materiaPai.descricao, () => 'Materias Relacionadas'),
        ];
    }

    public getItens() {
        return this.model.materiasRelacionadas;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => this.removeMateriaRelacionada(item),
                (item) => 'Remover',
                (item) => ['fa', 'fa-times'],
                (item) => ['btn-danger']
            )
        ];
        return menu;
    }

    removeMateriaRelacionada(materiaRelacionada: MateriaRelacionamentoModel) {
        this.model.materiasRelacionadas.splice(this.model.materiasRelacionadas.indexOf(materiaRelacionada), 1);
    }

}