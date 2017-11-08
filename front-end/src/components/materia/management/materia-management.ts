import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType } from '../../../util/router/router.path';
import { BroadcastEventBus, BroadcastEvent } from '../../../util/broadcast/broadcast.event-bus';
import { RouterManager } from '../../../util/router/router.manager';
import { MateriaFactory } from '../../../util/factory/materia/materia.factory';
import { Materia } from '../../../util/factory/materia/materia';
import { CardTableMenuEntry, CardTableMenu, CardTableColumn } from '../../common/card-table/card-table.types';

interface UI {
    materiaRelacionada: Materia;
    materias: Array<Materia>;
}

@Component({
    template: require('./materia-management.html')
})
export class MateriaManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: Materia = new Materia();
    ui: UI = { materias: new Array<Materia>(), materiaRelacionada: undefined };
    
    constructor() {
        super();
    }

    created() {

    }
    
    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RouterPathType.upd) {
                this.model = await MateriaFactory.detail(this.$route.params.id, true); 
            }
            this.ui.materias = await MateriaFactory.all();
        }
        catch (e) {
            RouterManager.redirectRoutePrevious();
        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }
    
    public addMateriaRelacionada(materiaRelacionada: Materia) {
        this.model.materiasRelacionadas.push(materiaRelacionada);
    }

    async save() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RouterPathType.add): {
                    await MateriaFactory.add(this.model, true);
                    break;
                }
                case (RouterPathType.upd): {
                    await MateriaFactory.update(this.model, true);
                    break;
                }
            }
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getColumns() {
        return [
            new CardTableColumn((item: Materia) => item.descricao, () => 'Materias Relacionadas'),
        ];
    }

    public getItens() {
        return this.model.materiasRelacionadas;
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
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER, true);
            MateriaFactory.disable(item.ID);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER, true);
        }
    }

}