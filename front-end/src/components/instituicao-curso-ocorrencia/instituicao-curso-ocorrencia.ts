import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { BroadcastEventBus, BroadcastEvent } from '../../util/broadcast/broadcast.event-bus';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../common/card-table/card-table.types';
import { RouterManager } from '../../util/router/router.manager';
import { RouterPath } from '../../util/router/router.path';
import { InstituicaoFactory } from '../../util/factory/instituicao/instituicao.factory';
import { InstituicaoCursoOcorrencia } from '../../util/factory/instituicao/instituicao-curso-ocorrencia';
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
            this.ui.lista = await InstituicaoFactory.allCursoOcorrencia(this.$route.params.id, this.$route.params.idCurso, this.$route.params.dataInicio);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getColumns() {
        return [
            new CardTableColumn((item: InstituicaoCursoOcorrencia) => item.dataInicio, () => 'Data de Início'),
            new CardTableColumn((item: InstituicaoCursoOcorrencia) => item.dataFim, () => 'Data de Fim')
        ];
    }

    public getItens() {
        return this.ui.lista;
    }

    public getMenu() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item: InstituicaoCursoOcorrencia) => RouterManager.redirectRoute(RouterPath.INSTITUICAO_CURSO_OCORRENCIA, {
                    id: this.$route.params.id,
                    idCurso: this.$route.params.idCurso,
                    dataInicio: this.$route.params.dataInicio,
                    dataInicioOcorrencia: moment(item.dataInicio).format('DD-MM-YYYY')
                }),
                (item) => 'Atualizar',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            )
        ];
        return menu;
    }

    public doNew() {
        RouterManager.redirectRoute(RouterPath.INSTITUICAO_CURSO_OCORRENCIA_ADD, { idInstituicao: this.$route.params.idInstituicao, idCurso: this.$route.params.idCurso });
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