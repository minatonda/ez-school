import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { BroadcastEventBus, BroadcastEvent } from '../../util/broadcast/broadcast.event-bus';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../common/card-table/card-table.types';
import { RouterManager } from '../../util/router/router.manager';
import { RouterPath } from '../../util/router/router.path';
import { InstituicaoFactory } from '../../util/factory/instituicao/instituicao.factory';
import { Instituicao } from '../../util/factory/instituicao/instituicao';

interface UI {
    lista: Array < Instituicao > ;
}

@Component({
    template: require('./instituicao.html')
})
export class InstituicaoComponent extends Vue {

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
            this.ui.lista = await InstituicaoFactory.all();
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getColumns() {
        return [
            new CardTableColumn((item: Instituicao) => item.nome, () => 'Nome'),
            new CardTableColumn((item: Instituicao) => item.cnpj, () => 'CNPJ')
        ];
    }

    public getItens() {
        return this.ui.lista;
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
                (item) => RouterManager.redirectRoute(RouterPath.INSTITUICAO_CURSO, { id: item.id }),
                (item) => 'Gerenciar Cursos',
                (item) => ['fa', 'fa-book'],
                (item) => ['btn-primary']
            ),
            // new CardTableMenuEntry(
            //     (item) => RouterManager.redirectRoute(RouterPath.CURSO_UPD, item),
            //     (item) => 'Gerenciar Pessoas',
            //     (item) => ['fa', 'fa-user'],
            //     (item) => ['btn-primary']
            // ),
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
            InstituicaoFactory.disable(item.id);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER, true);
        }
    }

}