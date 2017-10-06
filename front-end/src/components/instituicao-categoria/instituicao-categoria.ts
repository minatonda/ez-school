import { Component, Vue, Prop } from 'vue-property-decorator';
import { BroadcastEventBus, BroadcastEvent } from '../../util/broadcast/broadcast.event-bus';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../common/card-table/card-table.types';
import { RouterManager } from '../../util/router/router.manager';
import { RouterPath } from '../../util/router/router.path';
import { InstituicaoCategoriaFactory } from '../../util/factory/instituicao-categoria/instituicao-categoria.factory';
import { InstituicaoCategoria } from '../../util/factory/instituicao-categoria/instituicao-categoria';

@Component({
    template: require('./instituicao-categoria.html')
})
export class InstituicaoCategoriaComponent extends Vue {

    @Prop()
    alias: string;
    
    lista: Array<InstituicaoCategoria> = [];

    constructor() {
        super();
    }

    async created() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.lista = await InstituicaoCategoriaFactory.all();
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
        return this.lista;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => RouterManager.redirectRoute(RouterPath.INSTITUICAO_UPD, item),
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

    public doNew(){
        RouterManager.redirectRoute(RouterPath.INSTITUICAO_CATEGORIA_ADD);
    }

    public remove(item) {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER, true);
            InstituicaoCategoriaFactory.del(item.id);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER, true);
        }
    }

}