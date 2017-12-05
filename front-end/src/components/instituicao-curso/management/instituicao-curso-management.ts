import { Vue } from 'vue-property-decorator';
import { Component, Prop, Watch } from 'vue-property-decorator';
import { BroadcastEventBus, BroadcastEvent } from '../../../module/broadcast.event-bus';
import { Router } from '../../../router';
import { RouterPathType } from '../../../module/model/client/route-path';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../common/card-table/card-table.types';
import { DayOfWeekEnumLabel } from '../../../module/constant/enum-label.constant';
import { CursoFactory } from '../../../module/factory/curso.factory';
import { InstituicaoFactory } from '../../../module/factory/instituicao.factory';
import { Curso } from '../../../module/model/server/curso';
import { CursoGrade } from '../../../module/model/server/curso-grade';
import { InstituicaoCursoPeriodo } from '../../../module/model/server/instituicao-curso-periodo';
import { InstituicaoCursoTurma } from '../../../module/model/server/instituicao-curso-turma';
import { DayOfWeek } from '../../../module/model/server/instituicao-curso-ocorrencia-periodo-professor-periodo-aula';
import { InstituicaoCurso } from '../../../module/model/server/instituicao-curso';

interface UI {
    cursos: Array < Curso > ;
    cursoGrades: Array < CursoGrade > ;
    instituicaoCursoPeriodo: InstituicaoCursoPeriodo;
    instituicaoCursoTurma: InstituicaoCursoTurma;
    instituicaoButtons: Array < any > ;
}

@Component({
    template: require('./instituicao-curso-management.html')
})
export class InstituicaoCursoManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: InstituicaoCurso = new InstituicaoCurso();

    clearCursoGrade = false;

    ui: UI = {
        cursos: undefined,
        cursoGrades: undefined,
        instituicaoCursoPeriodo: new InstituicaoCursoPeriodo(),
        instituicaoCursoTurma: new InstituicaoCursoTurma(),
        instituicaoButtons: DayOfWeekEnumLabel
    };

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.ui.cursos = await CursoFactory.all();
            if (this.operation === RouterPathType.upd) {
                this.model = await InstituicaoFactory.detailInstituicaoCurso(this.$route.params.id, this.$route.params.idInstituicaoCurso, true);
            }
        }
        catch (e) {
            Router.redirectRoutePrevious();
        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getTablePeriodo() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => this.removePeriodo(item),
                (item) => 'Remover',
                (item) => ['fa', 'fa-times'],
                (item) => ['btn-danger']
            )
        ];
        let columns = [
            new CardTableColumn((item: InstituicaoCursoPeriodo) => item.inicio, () => 'Início'),
            new CardTableColumn((item: InstituicaoCursoPeriodo) => item.fim, () => 'Fim'),
            new CardTableColumn((item: InstituicaoCursoPeriodo) => {
                return DayOfWeekEnumLabel.map((day) => {
                    if (this.isDiaSemanaSelected(day.value, item)) {
                        return `<span class="badge badge-primary mx-2">${day.labelShort}</span>`;
                    }
                }).join('');
            }, () => 'Dias'),
        ];
        return { itens: this.model.periodos, columns: columns, menu: menu };
    }
    public addPeriodo(periodo: InstituicaoCursoPeriodo) {
        let periodoAdd = Object.assign({}, periodo);
        periodoAdd.diaSemana = periodo.diaSemana.slice();
        this.model.periodos.push(periodoAdd);
    }
    public removePeriodo(item: InstituicaoCursoPeriodo) {
        this.model.periodos.splice(this.model.periodos.indexOf(item), 1);
    }

    public getTableTurma() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => this.removeTurma(item),
                (item) => 'Remover',
                (item) => ['fa', 'fa-times'],
                (item) => ['btn-danger']
            )
        ];
        let columns = [
            new CardTableColumn((item: InstituicaoCursoTurma) => item.nome, () => 'Nome'),
            new CardTableColumn((item: InstituicaoCursoTurma) => item.descricao, () => 'Descrição')
        ];
        return { itens: this.model.turmas, columns: columns, menu: menu };
    }
    public addTurma(item: InstituicaoCursoTurma) {
        this.model.turmas.push(Object.assign({}, item));
    }
    public removeTurma(item: InstituicaoCursoTurma) {
        this.model.turmas.splice(this.model.turmas.indexOf(item), 1);
    }

    public toggleDiaSemana(dia: DayOfWeek, periodo: InstituicaoCursoPeriodo) {
        if (this.isDiaSemanaSelected(dia, periodo)) {
            periodo.diaSemana.splice(periodo.diaSemana.indexOf(dia), 1);
        }
        else {
            periodo.diaSemana.push(dia);
        }
    }

    public isDiaSemanaSelected(dia: DayOfWeek, periodo: InstituicaoCursoPeriodo) {
        return periodo.diaSemana.indexOf(dia) > -1;
    }

    public async onCursoChanged(curso) {
        this.clearCursoGrade = true;
        setImmediate(() => { this.clearCursoGrade = false; });
        if (curso) {
            this.ui.cursoGrades = await CursoFactory.allGrade(curso.id);
        }
        else {
            this.ui.cursoGrades = [];
        }
    }

    async save() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RouterPathType.add):
                    {
                        await InstituicaoFactory.addInstituicaoCurso(this.$route.params.id, this.model, true);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await InstituicaoFactory.updateInstituicaoCurso(this.$route.params.id, this.model, true);
                        break;
                    }
            }
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

}