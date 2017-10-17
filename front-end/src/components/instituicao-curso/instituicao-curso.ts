import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { BroadcastEventBus, BroadcastEvent } from '../../util/broadcast/broadcast.event-bus';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../common/card-table/card-table.types';
import { RouterManager } from '../../util/router/router.manager';
import { RouterPath } from '../../util/router/router.path';
import { InstituicaoFactory } from '../../util/factory/instituicao/instituicao.factory';
import { InstituicaoCurso } from '../../util/factory/instituicao/instituicao-curso';
import * as moment from 'moment';

interface UI {
    lista: Array < InstituicaoCurso > ;
}

@Component({
    template: require('./instituicao-curso.html')
})
export class InstituicaoCursoComponent extends Vue {

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
            this.ui.lista = await InstituicaoFactory.allCurso(this.$route.params.id);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getColumns() {
        return [
            new CardTableColumn((item: InstituicaoCurso) => item.curso.nome, () => 'Nome'),
            new CardTableColumn((item: InstituicaoCurso) => item.dataFim, () => 'Início')
        ];
    }

    public getItens() {
        return this.ui.lista;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => RouterManager.redirectRoute(RouterPath.INSTITUICAO_CURSO_UPD, {
                    id: this.$route.params.id,
                    idCurso: item.curso.id,
                    dataInicio: moment(item.dataInicio).format('DD-MM-YYYY')
                }),
                (item) => 'Atualizar',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            ),
            new CardTableMenuEntry(
                (item: InstituicaoCurso) => RouterManager.redirectRoute(RouterPath.INSTITUICAO_CURSO_OCORRENCIA, {
                    id: this.$route.params.id,
                    idCurso: item.curso.id,
                    dataInicio: moment(item.dataInicio).format('DD-MM-YYYY')
                }),
                (item) => 'Gerenciar Ocorrências',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            )
        ];
        return menu;
    }

    public doNew() {
        RouterManager.redirectRoute(RouterPath.INSTITUICAO_CURSO_ADD, { idInstituicao: this.$route.params.idInstituicao });
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