import { Component, Vue, Prop } from 'vue-property-decorator';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../common/card-table/card-table.types';
import { BroadcastEventBus, BroadcastEvent } from '../../module/broadcast.event-bus';
import { Router } from '../../router';
import { RouterPath } from '../../module/model/client/route-path';
import { InstituicaoCategoriaFactory } from '../../module/factory/instituicao-categoria.factory';
import { InstituicaoCategoria } from '../../module/model/server/instituicao-categoria';

interface UI {
    lista: Array < InstituicaoCategoria > ;
}

@Component({
    template: require('./instituicao-categoria.html')
})
export class InstituicaoCategoriaComponent extends Vue {

    @Prop()
    alias: string;

    ui: UI = {
        lista: undefined
    };

    model: InstituicaoCategoria = new InstituicaoCategoria();

    constructor() {
        super();
    }

    async created() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.ui.lista = await InstituicaoCategoriaFactory.all();
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getColumns() {
        return [
            new CardTableColumn((item: InstituicaoCategoria) => item.nome, () => 'Nome'),
            new CardTableColumn((item: InstituicaoCategoria) => item.descricao, () => 'Descrição')
        ];
    }

    public getItens() {
        return this.ui.lista;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => Router.redirectRoute(RouterPath.INSTITUICAO_CATEGORIA_UPD, item),
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
        Router.redirectRoute(RouterPath.INSTITUICAO_CATEGORIA_ADD);
    }

    public remove(item) {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER, true);
            InstituicaoCategoriaFactory.disable(item.id, true);
            this.ui.lista.splice(this.ui.lista.indexOf(item), 1);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER, true);
        }
    }

}