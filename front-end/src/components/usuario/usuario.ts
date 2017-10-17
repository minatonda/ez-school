import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { BroadcastEventBus, BroadcastEvent } from '../../util/broadcast/broadcast.event-bus';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../common/card-table/card-table.types';
import { RouterManager } from '../../util/router/router.manager';
import { RouterPath } from '../../util/router/router.path';
import { UsuarioFactory } from '../../util/factory/usuario/usuario.factory';
import { Usuario } from '../../util/factory/usuario/usuario';

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
                (item) => RouterManager.redirectRoute(RouterPath.USUARIO_UPD, item),
                (item) => 'Atualizar',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            ),
            new CardTableMenuEntry(
                (item) => RouterManager.redirectRoute(RouterPath.USUARIO_ALUNO, item),
                (item) => 'Aluno',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            ),
            new CardTableMenuEntry(
                (item) => RouterManager.redirectRoute(RouterPath.USUARIO_PROFESSOR, item),
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
        RouterManager.redirectRoute(RouterPath.USUARIO_ADD);
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