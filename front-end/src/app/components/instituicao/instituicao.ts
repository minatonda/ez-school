import Vue from 'vue';
import Component from 'vue-class-component';

import { Instituicao } from '../../common/factory/instituicao/instituicao';
import { InstituicaoFactory } from '../../common/factory/instituicao/instituicao.factory';
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
    template: require('./instituicao.html')
})
export class InstituicaoComponent extends Vue {

    @prop()
    alias: string;
    
    lista: Array<Instituicao> = [];

    constructor() {
        super();
    }

    async created() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.lista = await InstituicaoFactory.all();
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
        return this.lista;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => RouterManager.redirectRoute(RoutePath.INSTITUICAO_UPD, item),
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
            InstituicaoFactory.del(item.id);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER, true);
        }
    }

}