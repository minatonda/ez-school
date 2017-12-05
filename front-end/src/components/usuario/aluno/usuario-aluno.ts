import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType } from '../../../module/model/client/route-path';
import { BroadcastEventBus, BroadcastEvent } from '../../../module/broadcast.event-bus';
import { Router } from '../../../router';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../../common/card-table/card-table.types';
import { CategoriaProfissionalFactory } from '../../../module/factory/categoria-profissional.factory';
import { CategoriaProfissional } from '../../../module/model/server/categoria-profissional';
import { UsuarioFactory } from '../../../module/factory/usuario.factory';
import { Professor } from '../../../module/model/server/professor';
import { Aluno } from '../../../module/model/server/aluno';

interface UI {
    categoriaProfissional: CategoriaProfissional;
    categoriaProfissionais: Array<CategoriaProfissional>;
}

@Component({
    template: require('./usuario-aluno.html')
})
export class UsuarioAlunoComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: Aluno = new Aluno();
    ui: UI = { categoriaProfissionais: new Array<CategoriaProfissional>(), categoriaProfissional: undefined };
    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.model = await UsuarioFactory.detailAluno(this.$route.params.id);
            this.ui.categoriaProfissionais = await CategoriaProfissionalFactory.all();
        }
        catch (e) {
            Router.redirectRoutePrevious();
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
            await UsuarioFactory.updateAluno(this.$route.params.id, this.model, true);
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