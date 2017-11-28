import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../common/card-table/card-table.types';
import { BroadcastEventBus, BroadcastEvent } from '../../module/broadcast.event-bus';
import { Router } from '../../router';
import { RouterPath } from '../../module/model/client/route-path';
import { UsuarioFactory } from '../../module/factory/usuario.factory';
import { Usuario } from '../../module/model/server/usuario';

interface UI {
    lista: Array < Usuario > ;
}

@Component({
    template: require('./usuario.html')
})
export class UsuarioComponent extends Vue {

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
            this.ui.lista = await UsuarioFactory.all();
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getColumns() {
        return [
            new CardTableColumn((item: Usuario) => item.username, () => 'Username'),
            new CardTableColumn((item: Usuario) => item.usuarioInfo && item.usuarioInfo.nome || '', () => 'Nome')
        ];
    }

    public getItens() {
        return this.ui.lista;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => Router.redirectRoute(RouterPath.USUARIO_UPD, item),
                (item) => 'Atualizar',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            ),
            new CardTableMenuEntry(
                (item) => Router.redirectRoute(RouterPath.USUARIO_ALUNO, item),
                (item) => 'Aluno',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            ),
            new CardTableMenuEntry(
                (item) => Router.redirectRoute(RouterPath.USUARIO_PROFESSOR, item),
                (item) => 'Professor',
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
        Router.redirectRoute(RouterPath.USUARIO_ADD);
    }

    public remove(item) {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER, true);
            UsuarioFactory.disable(item.id);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER, true);
        }
    }

}