import { Vue, Component, Prop } from 'vue-property-decorator';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { InstituicaoCursoModel } from '../../../../../ezs-common/src/model/server/instituicao-curso.model';
import { InstituicaoCursoPeriodoModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-periodo.model';
import { InstituicaoCursoTurmaModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-turma.model';
import { DayOfWeekModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-periodo-professor-periodo-aula.model';
import { DayOfWeekEnumLabel } from '../../../module/constant/enum-label.constant';
import { CursoModel } from '../../../../../ezs-common/src/model/server/curso.model';
import { CursoGradeModel } from '../../../../../ezs-common/src/model/server/curso-grade.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
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
            this.ui.cursos = await FACTORY_CONSTANT.CursoFactory.all();
            if (this.operation === RouterPathType.upd) {
                this.model = await FACTORY_CONSTANT.InstituicaoFactory.detailInstituicaoCurso(this.$route.params.id, this.$route.params.idInstituicaoCurso);
            }
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
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
                        await FACTORY_CONSTANT.InstituicaoFactory.addInstituicaoCurso(this.$route.params.id, this.model);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await FACTORY_CONSTANT.InstituicaoFactory.updateInstituicaoCurso(this.$route.params.id, this.model);
                        break;
                    }
            }
            NotifyUtil.successG(I18N_ERROR_GENERIC.MODELO_SALVAR, ApplicationService.getLanguage());
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getTablePeriodo() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry({
                label: (item) => 'Remover',
                method: (item) => this.removePeriodo(item),
                btnClass: (item) => ['btn-danger'],
                iconClass: (item) => ['fa', 'fa-times']
            })
        ];
        let columns = [
            new CardTableColumn({
                value: (item: InstituicaoCursoPeriodoModel) => item.inicio,
                label: () => 'Início'
            }),
            new CardTableColumn({
                value: (item: InstituicaoCursoPeriodoModel) => item.fim,
                label: () => 'Fim'
            }),
            new CardTableColumn({
                value: (item: InstituicaoCursoPeriodoModel) => {
                    return DayOfWeekEnumLabel.map((day) => {
                        if (this.isDiaSemanaSelected(day.value, item)) {
                            return `<span class="badge badge-primary mx-2">${day.labelShort}</span>`;
                        }
                    }).join('');
                },
                label: () => 'Dias'
            })
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
            new CardTableMenuEntry({
                label: (item) => 'Remover',
                method: (item) => this.removeTurma(item),
                btnClass: (item) => ['btn-danger'],
                iconClass: (item) => ['fa', 'fa-times']
            })
        ];
        let columns = [
            new CardTableColumn({
                value: (item: InstituicaoCursoTurmaModel) => item.nome,
                label: () => 'Nome'
            }),
            new CardTableColumn({
                value: (item: InstituicaoCursoTurmaModel) => item.descricao,
                label: () => 'Descrição'
            })
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
            this.ui.cursoGrades = await FACTORY_CONSTANT.CursoFactory.allCursoGrade(curso.id);
        }
        else {
            this.ui.cursoGrades = [];
        }
    }

}