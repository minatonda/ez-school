import Vue from 'vue';
import Component from 'vue-class-component';

import { Materia } from '../../common/factory/materia/materia';
import { MateriaFactory } from '../../common/factory/materia/materia.factory';
import { CardTableMenu } from '../common/card-table/types/card-table-menu';
import { CardTableMenuEntry } from '../common/card-table/types/card-table-menu-entry';
import { CardTableColumn } from '../common/card-table/types/card-table-column';
import { BroadcastEventBus } from '../../common/vue/broadcast/broadcast.event-bus';
import { BroadcastEvent } from '../../common/vue/broadcast/broadcast.events';
import { RouterManager } from '../../common/vue/router/router.manager';
import { RoutePath } from '../../common/vue/router/route-path';
import { Notify } from '../../common/modules/notify/notify';
import { prop } from 'vue-property-decorator/lib/vue-property-decorator';

@Component({
    template: require('./materia.html')
})
export class MateriaComponent extends Vue {

    @prop()
    alias: string;
    
    lista: Array<Materia> = [];

    constructor() {
        super();
    }

    async created() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.lista = await MateriaFactory.all();
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getColumns() {
        return [
            new CardTableColumn((item: Materia) => item.nome, () => 'Nome'),
            new CardTableColumn((item: Materia) => item.descricao, () => 'Descrição')
        ];
    }

    public getItens() {
        return this.lista;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => RouterManager.redirectRoute(RoutePath.MATERIA_UPD, item),
                (item) => 'Atualizar',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            ),
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
            MateriaFactory.del(item.id);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER, true);
        }
    }

}