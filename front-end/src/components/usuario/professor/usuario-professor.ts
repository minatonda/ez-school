import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType } from '../../../util/router/router.path';
import { BroadcastEventBus, BroadcastEvent } from '../../../util/broadcast/broadcast.event-bus';
import { RouterManager } from '../../../util/router/router.manager';
import { Professor } from '../../../util/factory/usuario/professor';
import { Usuario } from '../../../util/factory/usuario/usuario';
import { UsuarioInfo } from '../../../util/factory/usuario/usuario-info';
import { UsuarioFactory } from '../../../util/factory/usuario/usuario.factory';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../../common/card-table/card-table.types';
import { CategoriaProfissional } from '../../../util/factory/categoria-profissional/categoria-profissional';
import { CategoriaProfissionalFactory } from '../../../util/factory/categoria-profissional/categoria-profissional.factory';

interface UI {
    categoriaProfissional: CategoriaProfissional;
    categoriaProfissionais: Array<CategoriaProfissional>;
}

@Component({
    template: require('./usuario-professor.html')
})
export class UsuarioProfessorComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: Professor = new Professor();
    ui: UI = { categoriaProfissionais: new Array<CategoriaProfissional>(), categoriaProfissional: undefined };
    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.model = await UsuarioFactory.detailProfessor(this.$route.params.id);
            this.ui.categoriaProfissionais = await CategoriaProfissionalFactory.all();
        }
        catch (e) {
            RouterManager.redirectRoutePrevious();
        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public addCategoriaProfissional(categoriaProfissional: CategoriaProfissional) {
        this.model.categoriaProfissionais.push(categoriaProfissional);
    }

    async save() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            await UsuarioFactory.updateProfessor(this.$route.params.id, this.model, true);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }
    
    public getColumns() {
        return [
            new CardTableColumn((item: CategoriaProfissional) => item.descricao, () => 'Areas de Interesse'),
        ];
    }

    public getItens() {
        return this.model.categoriaProfissionais;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
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
            CategoriaProfissionalFactory.disable(item.ID);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER, true);
        }
    }

}