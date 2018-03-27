import { Vue, Component, Prop } from 'vue-property-decorator';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { MateriaModel } from '../../../../../ezs-common/src/model/server/materia.model';
import { MateriaRelacionamentoModel } from '../../../../../ezs-common/src/model/server/materia-relacionamento.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';
import { AppRouterPath } from '../../../app.router.path';

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
                this.model = await FACTORY_CONSTANT.MateriaFactory.detail(this.$route.params.id); 
            }
            this.ui.materias = await FACTORY_CONSTANT.MateriaFactory.all();
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
                case (RouterPathType.add): {
                    await FACTORY_CONSTANT.MateriaFactory.add(this.model);
                    break;
                }
                case (RouterPathType.upd): {
                    await FACTORY_CONSTANT.MateriaFactory.update(this.model);
                    break;
                }
            }
            NotifyUtil.successG(I18N_ERROR_GENERIC.MODELO_SALVAR, ApplicationService.getLanguage());
            AppRouter.push(AppRouterPath.MATERIA);
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage(), I18N_ERROR_GENERIC.MODELO_SALVAR_FALHA);
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
            new CardTableColumn({
                value: (item: MateriaRelacionamentoModel) => item.materiaPai.descricao,
                label: () => 'Matéria'
            })
        ];
    }

    public getItens() {
        return this.model.materiasRelacionadas;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry({
                label: (item) => 'Remover',
                method: this.removeMateriaRelacionada,
                btnClass: (item) => ['btn-danger'],
                iconClass: (item) => ['fa', 'fa-times']
            })
        ];
        return menu;
    }

    removeMateriaRelacionada(materiaRelacionada: MateriaRelacionamentoModel) {
        this.model.materiasRelacionadas.splice(this.model.materiasRelacionadas.indexOf(materiaRelacionada), 1);
    }

}