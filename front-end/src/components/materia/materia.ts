import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../common/card-table/card-table.types';
import { BroadcastEventBus, BroadcastEvent } from '../../module/broadcast.event-bus';
import { Router } from '../../router';
import { RouterPath } from '../../module/model/client/route-path';
import { MateriaFactory } from '../../module/factory/materia.factory';
import { Materia } from '../../module/model/server/materia';

interface UI {
    lista: Array < Materia > ;
}

@Component({
    template: require('./materia.html')
})
export class MateriaComponent extends Vue {

    @Prop()
    alias: string;

    ui: UI = {
        lista: undefined
    };

    constructor() {
        super();
    }

    async created() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.ui.lista = await MateriaFactory.all();
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
        return this.ui.lista;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => Router.redirectRoute(RouterPath.MATERIA_UPD, item),
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

    public doNew() {
        Router.redirectRoute(RouterPath.MATERIA_ADD);
    }

    public async remove(item) {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER, true);
            await MateriaFactory.disable(item.id);
            this.ui.lista.splice(this.ui.lista.indexOf(item), 1);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER, true);
        }
    }

}