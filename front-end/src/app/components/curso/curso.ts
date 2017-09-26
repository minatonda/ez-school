import Vue from 'vue';
import Component from 'vue-class-component';

import { Curso } from '../../common/factory/curso/curso';
import { CursoFactory } from '../../common/factory/curso/curso.factory';
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
    template: require('./curso.html')
})
export class CursoComponent extends Vue {

    @prop()
    alias: string;
    
    lista: Array<Curso> = [];

    constructor() {
        super();
    }

    async created() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.lista = await CursoFactory.all();
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getColumns() {
        return [
            new CardTableColumn((item: Curso) => item.nome, () => 'Nome'),
            new CardTableColumn((item: Curso) => item.descricao, () => 'Descrição')
        ];
    }

    public getItens() {
        return this.lista;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => RouterManager.redirectRoute(RoutePath.EMPRESA_UPD, item),
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
            CursoFactory.del(item.id);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER, true);
        }
    }

}