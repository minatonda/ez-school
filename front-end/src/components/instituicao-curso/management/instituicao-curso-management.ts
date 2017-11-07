import { Vue } from 'vue-property-decorator';
import { Component, Prop, Watch } from 'vue-property-decorator';
import { RouterPathType } from '../../../util/router/router.path';
import { BroadcastEventBus, BroadcastEvent } from '../../../util/broadcast/broadcast.event-bus';
import { RouterManager } from '../../../util/router/router.manager';
import { InstituicaoCurso } from '../../../util/factory/instituicao/instituicao-curso';
import { InstituicaoFactory } from '../../../util/factory/instituicao/instituicao.factory';
import { Curso } from '../../../util/factory/curso/curso';
import { CursoGrade } from '../../../util/factory/curso/curso-grade';
import { CursoFactory } from '../../../util/factory/curso/curso.factory';
import { CardTableColumn, CardTableMenuEntry, CardTableMenu } from '../../common/card-table/card-table.types';
import { InstituicaoCursoOcorrencia } from '../../../util/factory/instituicao/instituicao-curso-ocorrencia';
import { InstituicaoCursoPeriodo } from '../../../util/factory/instituicao/instituicao-curso-periodo';
import { InstituicaoCursoTurma } from '../../../util/factory/instituicao/instituicao-curso-turma';

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
        instituicaoButtons: [
            { label: 'Seg', key: 'seg' },
            { label: 'Ter', key: 'ter' },
            { label: 'Qua', key: 'qua' },
            { label: 'Qui', key: 'qui' },
            { label: 'Sex', key: 'sex' },
            { label: 'Sab', key: 'sab' },
            { label: 'Dom', key: 'dom' },
        ]
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
                this.model = await InstituicaoFactory.detailCurso(this.$route.params.id, this.$route.params.idCurso, true);
            }
        }
        catch (e) {
            RouterManager.redirectRoutePrevious();
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
                return this.ui.instituicaoButtons.map((button) => {
                    if (item[button.key]) {
                        return `<span class="badge badge-primary mx-2">${button.label}</span>`;
                    }
                }).join('');
            }, () => 'Dias'),
        ];
        return { itens: this.model.periodos, columns: columns, menu: menu };
    }
    public addPeriodo(periodo: InstituicaoCursoPeriodo) {
        this.model.periodos.push(Object.assign({}, periodo));
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
                        await InstituicaoFactory.addCurso(this.$route.params.id, this.model, true);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await InstituicaoFactory.updateCurso(this.$route.params.id, this.model, true);
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