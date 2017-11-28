import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../common/card-table/card-table.types';
import { BroadcastEventBus, BroadcastEvent } from '../../module/broadcast.event-bus';
import { Router } from '../../router';
import { RouterPath } from '../../module/model/client/route-path';
import { InstituicaoFactory } from '../../module/factory/instituicao.factory';
import { InstituicaoCursoOcorrencia } from '../../module/model/server/instituicao-curso-ocorrencia';
import * as moment from 'moment';

interface UI {
    lista: Array < InstituicaoCursoOcorrencia > ;
}

@Component({
    template: require('./instituicao-curso-ocorrencia.html')
})
export class InstituicaoCursoOcorrenciaComponent extends Vue {

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
            this.ui.lista = await InstituicaoFactory.allInstituicaoCursoOcorrencia(this.$route.params.id, this.$route.params.idInstituicaoCurso);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getColumns() {
        return [
            new CardTableColumn((item: InstituicaoCursoOcorrencia) => { return moment(item.dataInicio).format('DD/MM/YYYY'); }, () => 'Data de Início'),
            new CardTableColumn((item: InstituicaoCursoOcorrencia) => { return item.dataFim ? moment(item.dataFim).format('DD/MM/YYYY') : undefined; }, () => 'Data de Fim')
        ];
    }

    public getItens() {
        return this.ui.lista;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item: InstituicaoCursoOcorrencia) => Router.redirectRoute(RouterPath.INSTITUICAO_CURSO_OCORRENCIA_UPD, {
                    id: this.$route.params.id,
                    idInstituicaoCurso: this.$route.params.idInstituicaoCurso,
                    idInstituicaoCursoOcorrencia: item.id
                }),
                (item) => 'Atualizar',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            ),
            new CardTableMenuEntry(
                (item: InstituicaoCursoOcorrencia) => Router.redirectRoute(RouterPath.INSTITUICAO_CURSO_OCORRENCIA, {
                    id: this.$route.params.id,
                    idInstituicaoCurso: this.$route.params.idInstituicaoCurso,
                    dataInicio: this.$route.params.dataInicio,
                    dataInicioOcorrencia: moment(item.dataInicio).format('DD-MM-YYYY')
                }),
                (item) => 'Gerenciar Períodos',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            )
        ];
        return menu;
    }

    public doNew() {
        Router.redirectRoute(RouterPath.INSTITUICAO_CURSO_OCORRENCIA_ADD, { idInstituicao: this.$route.params.idInstituicao, idInstituicaoCurso: this.$route.params.idInstituicaoCurso });
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