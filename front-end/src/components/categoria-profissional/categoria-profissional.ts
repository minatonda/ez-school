import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { BroadcastEventBus, BroadcastEvent } from '../../module/broadcast.event-bus';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../common/card-table/card-table.types';
import { Router } from '../../router';
import { RouterPath } from '../../module/model/client/route-path';
import { CategoriaProfissionalFactory } from '../../module/factory/categoria-profissional.factory';
import { CategoriaProfissional } from '../../module/model/server/categoria-profissional';

interface UI {
    lista: Array < CategoriaProfissional > ;
}

@Component({
    template: require('./categoria-profissional.html')
})
export class CategoriaProfissionalComponent extends Vue {

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
            this.ui.lista = await CategoriaProfissionalFactory.all();
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getColumns() {
        return [
            new CardTableColumn((item: CategoriaProfissional) => item.nome, () => 'Nome'),
            new CardTableColumn((item: CategoriaProfissional) => item.descricao, () => 'Descrição')
        ];
    }

    public getItens() {
        return this.ui.lista;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => Router.redirectRoute(RouterPath.CATEGORIA_PROFISSIONAL_UPD, item),
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
        Router.redirectRoute(RouterPath.CATEGORIA_PROFISSIONAL_ADD);
    }

    public remove(item) {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER, true);
            CategoriaProfissionalFactory.disable(item.id, true);
            this.ui.lista.splice(this.ui.lista.indexOf(item), 1);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER, true);
        }
    }

}