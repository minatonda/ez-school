import { Vue, Component, Prop } from 'vue-property-decorator';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { InstituicaoCursoModel } from '../../../../../ezs-common/src/model/server/instituicao-curso.model';
import { InstituicaoCursoPeriodoModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-periodo.model';
import { InstituicaoCursoTurmaModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-turma.model';
import { DayOfWeekModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-periodo-professor-periodo-aula.model';
import { DayOfWeekEnumLabel } from '../../../module/constant/enum-label.constant';
import { CursoModel } from '../../../../../ezs-common/src/model/server/curso.model';
import { CursoGradeModel } from '../../../../../ezs-common/src/model/server/curso-grade.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_MESSAGE } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';

interface UI {
    cursos: Array < CursoModel > ;
    cursoGrades: Array < CursoGradeModel > ;
    instituicaoCursoPeriodo: InstituicaoCursoPeriodoModel;
    instituicaoCursoTurma: InstituicaoCursoTurmaModel;
    instituicaoButtons: Array < any > ;
}

@Component({
    template: require('./page-instituicao-curso.html')
})
export class PageInstituicaoCursoComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: InstituicaoCursoModel = new InstituicaoCursoModel();

    clearCursoGrade = false;

    ui: UI = {
        cursos: undefined,
        cursoGrades: undefined,
        instituicaoCursoPeriodo: new InstituicaoCursoPeriodoModel(),
        instituicaoCursoTurma: new InstituicaoCursoTurmaModel(),
        instituicaoButtons: DayOfWeekEnumLabel
    };

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.ui.cursos = await Factory.CursoFactory.all();
            if (this.operation === RouterPathType.upd) {
                this.model = await Factory.InstituicaoFactory.detailInstituicaoCurso(this.$route.params.id, this.$route.params.idInstituicaoCurso);
            }
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.CONSULTAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
            AppRouter.back();
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    async save() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RouterPathType.add):
                    {
                        await Factory.InstituicaoFactory.addInstituicaoCurso(this.$route.params.id, this.model);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await Factory.InstituicaoFactory.updateInstituicaoCurso(this.$route.params.id, this.model);
                        break;
                    }
            }
            NotifyUtil.notifyI18N(I18N_MESSAGE.MODELO_SALVAR, ApplicationService.getLanguage(), NOTIFY_TYPE.SUCCESS);
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.MODELO_SALVAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
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
            new CardTableColumn((item: InstituicaoCursoPeriodoModel) => item.inicio, () => 'Início'),
            new CardTableColumn((item: InstituicaoCursoPeriodoModel) => item.fim, () => 'Fim'),
            new CardTableColumn((item: InstituicaoCursoPeriodoModel) => {
                return DayOfWeekEnumLabel.map((day) => {
                    if (this.isDiaSemanaSelected(day.value, item)) {
                        return `<span class="badge badge-primary mx-2">${day.labelShort}</span>`;
                    }
                }).join('');
            }, () => 'Dias'),
        ];
        return { itens: this.model.periodos, columns: columns, menu: menu };
    }
    public addPeriodo(periodo: InstituicaoCursoPeriodoModel) {
        let periodoAdd = Object.assign({}, periodo);
        periodoAdd.diaSemana = periodo.diaSemana.slice();
        this.model.periodos.push(periodoAdd);
    }
    public removePeriodo(item: InstituicaoCursoPeriodoModel) {
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
            new CardTableColumn((item: InstituicaoCursoTurmaModel) => item.nome, () => 'Nome'),
            new CardTableColumn((item: InstituicaoCursoTurmaModel) => item.descricao, () => 'Descrição')
        ];
        return { itens: this.model.turmas, columns: columns, menu: menu };
    }
    public addTurma(item: InstituicaoCursoTurmaModel) {
        this.model.turmas.push(Object.assign({}, item));
    }
    public removeTurma(item: InstituicaoCursoTurmaModel) {
        this.model.turmas.splice(this.model.turmas.indexOf(item), 1);
    }

    public toggleDiaSemana(dia: DayOfWeekModel, periodo: InstituicaoCursoPeriodoModel) {
        if (this.isDiaSemanaSelected(dia, periodo)) {
            periodo.diaSemana.splice(periodo.diaSemana.indexOf(dia), 1);
        }
        else {
            periodo.diaSemana.push(dia);
        }
    }

    public isDiaSemanaSelected(dia: DayOfWeekModel, periodo: InstituicaoCursoPeriodoModel) {
        return periodo.diaSemana.indexOf(dia) > -1;
    }

    public async onCursoChanged(curso) {
        this.clearCursoGrade = true;
        setImmediate(() => { this.clearCursoGrade = false; });
        if (curso) {
            this.ui.cursoGrades = await Factory.CursoFactory.allCursoGrade(curso.id);
        }
        else {
            this.ui.cursoGrades = [];
        }
    }

}