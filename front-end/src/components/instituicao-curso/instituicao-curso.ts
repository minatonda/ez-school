import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../common/card-table/card-table.types';
import { BroadcastEventBus, BroadcastEvent } from '../../module/broadcast.event-bus';
import { Router } from '../../router';
import { RouterPath } from '../../module/model/client/route-path';
import { InstituicaoFactory } from '../../module/factory/instituicao.factory';
import { InstituicaoCurso } from '../../module/model/server/instituicao-curso';

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
                (item) => Router.redirectRoute(RouterPath.INSTITUICAO_CURSO_UPD, {
                    id: this.$route.params.id,
                    idInstituicaoCurso: item.id
                }),
                (item) => 'Atualizar',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            ),
            new CardTableMenuEntry(
                (item: InstituicaoCurso) => Router.redirectRoute(RouterPath.INSTITUICAO_CURSO_OCORRENCIA, {
                    id: this.$route.params.id,
                    idInstituicaoCurso: item.curso.id,
                    dataInicio: moment(item.dataInicio).format('DD-MM-YYYY')
                }),
                (item) => 'Gerenciar Ocorrências',
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
        Router.redirectRoute(RouterPath.INSTITUICAO_CURSO_ADD, { idInstituicao: this.$route.params.idInstituicao });
    }

    public remove(item) {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            InstituicaoFactory.disableInstituicaoCurso(this.$route.params.id, item.id, true);
            this.ui.lista.splice(this.ui.lista.indexOf(item), 1);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

}