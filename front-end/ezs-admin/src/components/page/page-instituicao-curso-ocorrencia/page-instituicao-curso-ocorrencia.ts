import { Vue, Component, Prop } from 'vue-property-decorator';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { InstituicaoCursoOcorrenciaModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia.model';
import { InstituicaoCursoOcorrenciaPeriodoModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-periodo.model';
import { InstituicaoCursoOcorrenciaPeriodoAlunoModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-periodo-aluno.model';
import { InstituicaoCursoOcorrenciaPeriodoProfessorModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-periodo-professor.model';
import { InstituicaoCursoTurmaModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-turma.model';
import { InstituicaoCursoPeriodoModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-periodo.model';
import { InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel, DayOfWeekModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-periodo-professor-periodo-aula.model';
import { DayOfWeekEnumLabel } from '../../../module/constant/enum-label.constant';
import { CursoGradeMateriaModel } from '../../../../../ezs-common/src/model/server/curso-grade-materia.model';
import { EnumLabel } from '../../../../../ezs-common/src/model/client/enum-label.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';
import { UsuarioInfoModel } from '../../../../../ezs-common/src/model/server/usuario-info.model';
import * as moment from 'moment';

enum ModalOperation {
    add = 0,
    update = 1
}

enum InstituicaoCursoPeriodoOcorrenciaChildTab {
    aluno = 'ALUNO',
    professor = 'PROFESSOR'
}

interface UI {

    cursoGradeMaterias: Array<CursoGradeMateriaModel>;
    instituicaoCursoTurmas: Array<InstituicaoCursoTurmaModel>;
    instituicaoCursoPeriodos: Array<InstituicaoCursoPeriodoModel>;

    instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel;

    instituicaoCUrsoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel;
    instituicaoCUrsoOcorrenciaPeriodoAlunoUpdating: InstituicaoCursoOcorrenciaPeriodoAlunoModel;

    instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel;
    instituicaoCursoOcorrenciaPeriodoProfessorUpdating: InstituicaoCursoOcorrenciaPeriodoProfessorModel;

    instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula: InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel;

    instituicaoCursoOcorrenciaPeriodoCollapse: Array<boolean>;
    instituicaoCursoOcorrenciaPeriodoTab: Array<InstituicaoCursoPeriodoOcorrenciaChildTab>;

    dayOfWeek: DayOfWeekModel;
    dayOfWeeks: Array<EnumLabel>;

    queryAluno: any;
    queryProfessor: any;

    usuarioInfoLabel: any;

    dialogInstituicaoCursoPeriodoOperation: ModalOperation;
    dialogInstituicaoCursoPeriodoAlunoOperation: ModalOperation;
    dialogInstituicaoCursoPeriodoProfessorOperation: ModalOperation;
}

@Component({
    template: require('./page-instituicao-curso-ocorrencia.html')
})
export class PageInstituicaoCursoOcorrenciaComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    ui: UI = {
        cursoGradeMaterias: undefined,
        instituicaoCursoTurmas: undefined,
        instituicaoCursoPeriodos: undefined,

        instituicaoCursoOcorrenciaPeriodo: new InstituicaoCursoOcorrenciaPeriodoModel(),

        instituicaoCUrsoOcorrenciaPeriodoAluno: new InstituicaoCursoOcorrenciaPeriodoAlunoModel(),
        instituicaoCUrsoOcorrenciaPeriodoAlunoUpdating: new InstituicaoCursoOcorrenciaPeriodoAlunoModel(),

        instituicaoCursoOcorrenciaPeriodoProfessor: new InstituicaoCursoOcorrenciaPeriodoProfessorModel(),
        instituicaoCursoOcorrenciaPeriodoProfessorUpdating: new InstituicaoCursoOcorrenciaPeriodoProfessorModel(),

        instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula: undefined,

        instituicaoCursoOcorrenciaPeriodoCollapse: new Array(),
        instituicaoCursoOcorrenciaPeriodoTab: new Array(),

        dayOfWeek: undefined,
        dayOfWeeks: DayOfWeekEnumLabel,

        queryAluno: async (term) => {
            let itens = await FACTORY_CONSTANT.UsuarioFactory.allByTermo(term, false, false);
            return itens;
        },
        queryProfessor: async (term) => {
            let itens = await FACTORY_CONSTANT.UsuarioFactory.allByTermo(term, false, false);
            return itens;
        },

        usuarioInfoLabel: (item: UsuarioInfoModel) => {
            let labelObj = {} as any;
            labelObj.key = item.label;
            labelObj.label = `<div><span>${item.label}</span><div><div><span style="float:left;">${item.rg}</span><span style="float:right;">${item.cpf}</span></div>`;
            return labelObj;
        },

        dialogInstituicaoCursoPeriodoOperation: ModalOperation.add,
        dialogInstituicaoCursoPeriodoAlunoOperation: ModalOperation.add,
        dialogInstituicaoCursoPeriodoProfessorOperation: ModalOperation.add,
    };

    model: InstituicaoCursoOcorrenciaModel = new InstituicaoCursoOcorrenciaModel();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RouterPathType.upd) {
                this.model = await FACTORY_CONSTANT.InstituicaoFactory.detailInstituicaoCursoOcorrencia(this.$route.params.id, this.$route.params.idInstituicaoCurso, this.$route.params.idInstituicaoCursoOcorrencia);
            }
            this.ui.cursoGradeMaterias = await FACTORY_CONSTANT.InstituicaoFactory.allCursoGradeMaterias(this.$route.params.id, this.$route.params.idInstituicaoCurso);
            this.ui.instituicaoCursoTurmas = await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoCursoTurma(this.$route.params.id, this.$route.params.idInstituicaoCurso);
            this.ui.instituicaoCursoPeriodos = await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoCursoPeriodo(this.$route.params.id, this.$route.params.idInstituicaoCurso);
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
                        await FACTORY_CONSTANT.InstituicaoFactory.addInstituicaoCursoOcorrencia(this.$route.params.id, this.$route.params.idInstituicaoCurso, this.model);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await FACTORY_CONSTANT.InstituicaoFactory.updateInstituicaoCursoOcorrencia(this.$route.params.id, this.$route.params.idInstituicaoCurso, this.model);
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

    isSelectPeriodoAula() {
        return !!this.ui.instituicaoCursoOcorrenciaPeriodo && !!this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.professor && !!this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.cursoGradeMateria;
    }

    isTabSelected(tab: InstituicaoCursoPeriodoOcorrenciaChildTab, index: number) {
        if (!!this.ui.instituicaoCursoOcorrenciaPeriodoTab[index]) {
            return this.ui.instituicaoCursoOcorrenciaPeriodoTab[index] === tab;
        }
        else {
            return InstituicaoCursoPeriodoOcorrenciaChildTab.aluno === tab;
        }
    }

    setTabSelected(tab: InstituicaoCursoPeriodoOcorrenciaChildTab, index: number) {
        this.ui.instituicaoCursoOcorrenciaPeriodoTab[index] = tab;
        this.$forceUpdate();
    }

    onChangeProfessorAndCursoGradeMateria() {
        this.ui.dayOfWeek = undefined;
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas = new Array<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel>();
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = undefined;

        this.$forceUpdate();
    }

    saveInstituicaoCursoOcorrenciaPeriodo(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        this.addInstituicaoCursoOcorrenciaPeriodo(instituicaoCursoOcorrenciaPeriodo);
    }

    saveInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel) {
        if (this.ui.dialogInstituicaoCursoPeriodoAlunoOperation === ModalOperation.add) {
            this.addInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno);
        }
        else {
            let index = this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoAlunos.indexOf(this.ui.instituicaoCUrsoOcorrenciaPeriodoAlunoUpdating);
            this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoAlunos[index] = instituicaoCursoOcorrenciaPeriodoAluno;
        }
        this.closeDialogInstituicaoCursoOcorrenciaPeriodoAluno();
    }

    saveInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel) {
        if (this.ui.dialogInstituicaoCursoPeriodoProfessorOperation === ModalOperation.add) {
            this.addInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor);
        }
        else {
            let index = this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoProfessores.indexOf(this.ui.instituicaoCursoOcorrenciaPeriodoProfessorUpdating);
            this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoProfessores[index] = instituicaoCursoOcorrenciaPeriodoProfessor;
        }
        this.closeDialogInstituicaoCursoOcorrenciaPeriodoProfessor();
    }


    toggleCollapseInstituicaoCursoOcorrenciaPeriodo(index: number) {
        this.ui.instituicaoCursoOcorrenciaPeriodoCollapse[index] = !this.ui.instituicaoCursoOcorrenciaPeriodoCollapse[index];
        this.$forceUpdate();
    }

    addInstituicaoCursoOcorrenciaPeriodo(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        this.model.instituicaoCursoOcorrenciaPeriodos.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodoModel(), instituicaoCursoOcorrenciaPeriodo));
    }

    removeInstituicaoCursoOcorrenciaPeriodo(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        this.model.instituicaoCursoOcorrenciaPeriodos.splice(this.model.instituicaoCursoOcorrenciaPeriodos.indexOf(instituicaoCursoOcorrenciaPeriodo), 1);
    }


    addInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel) {
        this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoAlunos.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodoAlunoModel(), instituicaoCursoOcorrenciaPeriodoAluno));
        this.closeDialogInstituicaoCursoOcorrenciaPeriodoAluno();
    }

    removeInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel, instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoAlunos.splice(instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoAlunos.indexOf(instituicaoCursoOcorrenciaPeriodoAluno), 1);
    }
    

    addInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel) {
        this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoProfessores.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodoProfessorModel(), instituicaoCursoOcorrenciaPeriodoProfessor));
        this.closeDialogInstituicaoCursoOcorrenciaPeriodoProfessor();
    }

    removeInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel, instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoProfessores.splice(instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoProfessores.indexOf(instituicaoCursoOcorrenciaPeriodoProfessor), 1);
    }


    addInstituicaoCursoOcorenciaPeriodoProfessorPeriodoAula(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula: InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel) {
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel(), instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula));
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = undefined;
    }

    removerInstituicaoCursoOcorenciaPeriodoProfessorPeriodoAula(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula: InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel) {
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.splice(this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.indexOf(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula), 1);
    }


    getPeriodosDisponiveis(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel, instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel, dayOfWeek: EnumLabel) {
        if (instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoPeriodo) {
            let periodosDisponiveis = new Array<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel>();
            let fromCurrentProfessor = this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas;

            instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoPeriodo.diaSemana.forEach(x => {
                if (dayOfWeek && x === dayOfWeek.value) {
                    let dataInicio = moment(instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoPeriodo.inicio, 'hh:mm');
                    let dataPausaInicio = moment(instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoPeriodo.pausaInicio, 'hh:mm');
                    let dataPausaFim = moment(instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoPeriodo.pausaFim, 'hh:mm');
                    let dataFim = moment(instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoPeriodo.fim, 'hh:mm');

                    let minutos = moment.duration((dataFim.diff(dataInicio))).asMinutes();
                    minutos -= moment.duration((dataPausaFim.diff(dataPausaInicio))).asMinutes();

                    while (dataInicio.isBefore(dataPausaInicio)) {
                        let instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel();
                        instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.dia = x;
                        instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.inicio = dataInicio.format('hh:mm');
                        dataInicio.add(minutos / (instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoPeriodo.quebras || 4), 'minute');
                        instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.fim = dataInicio.format('hh:mm');
                        instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.label = instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.inicio + ' - ' + instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.fim;

                        if (!fromCurrentProfessor.find(x => x.inicio === instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.inicio && x.fim === instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.fim && x.dia === dayOfWeek.value)) {
                            periodosDisponiveis.push(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula);
                        }
                    }

                    while (dataPausaFim.isBefore(dataFim)) {
                        let instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel();
                        instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.dia = x;
                        instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.inicio = dataPausaFim.format('hh:mm');
                        dataPausaFim.add(minutos / (instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoPeriodo.quebras || 4), 'minute');
                        instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.fim = dataPausaFim.format('hh:mm');
                        instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.label = instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.inicio + ' - ' + instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.fim;

                        if (!fromCurrentProfessor.find(x => x.inicio === instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.inicio && x.fim === instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.fim && x.dia === dayOfWeek.value)) {
                            periodosDisponiveis.push(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula);
                        }
                    }
                }
            });
            return periodosDisponiveis;
        }
        else {
            return [];
        }
    }

    getDayWeekLabel(dayOfWeek: DayOfWeekModel) {
        return this.ui.dayOfWeeks.find(x => x.value === dayOfWeek);
    }

    getDateTimeLabel(date: string) {
        return moment(date).format('DD/MM/YYYY');
    }


    openDialogInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        this.ui.instituicaoCursoOcorrenciaPeriodo = instituicaoCursoOcorrenciaPeriodo;
        (this.$refs['modal-ocorrencia-periodo-aluno'] as any).show();
    }

    closeDialogInstituicaoCursoOcorrenciaPeriodoAluno() {
        this.ui.instituicaoCursoOcorrenciaPeriodo = new InstituicaoCursoOcorrenciaPeriodoModel();
        this.ui.instituicaoCUrsoOcorrenciaPeriodoAluno = new InstituicaoCursoOcorrenciaPeriodoAlunoModel();
        this.ui.instituicaoCUrsoOcorrenciaPeriodoAlunoUpdating = undefined;
        (this.$refs['modal-ocorrencia-periodo-aluno'] as any).hide();
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoAlunoToAdd(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        this.ui.dialogInstituicaoCursoPeriodoAlunoOperation = ModalOperation.add;
        this.ui.instituicaoCUrsoOcorrenciaPeriodoAluno = new InstituicaoCursoOcorrenciaPeriodoAlunoModel();
        this.openDialogInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodo);
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoAlunoToUpdate(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel, instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        this.ui.dialogInstituicaoCursoPeriodoAlunoOperation = ModalOperation.update;
        this.ui.instituicaoCUrsoOcorrenciaPeriodoAlunoUpdating = instituicaoCursoOcorrenciaPeriodoAluno;
        this.ui.instituicaoCUrsoOcorrenciaPeriodoAluno = Object.assign({}, instituicaoCursoOcorrenciaPeriodoAluno);
        this.openDialogInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodo);
    }


    openDialogInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        this.ui.instituicaoCursoOcorrenciaPeriodo = instituicaoCursoOcorrenciaPeriodo;
        (this.$refs['modal-ocorrencia-periodo-professor'] as any).show();
    }

    closeDialogInstituicaoCursoOcorrenciaPeriodoProfessor() {
        this.ui.instituicaoCursoOcorrenciaPeriodo = new InstituicaoCursoOcorrenciaPeriodoModel();
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor = new InstituicaoCursoOcorrenciaPeriodoProfessorModel();
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessorUpdating = undefined;
        (this.$refs['modal-ocorrencia-periodo-professor'] as any).hide();
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoProfessorToAdd(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        this.ui.dialogInstituicaoCursoPeriodoProfessorOperation = ModalOperation.add;
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor = new InstituicaoCursoOcorrenciaPeriodoProfessorModel();
        this.openDialogInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodo);
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoProfessorToUpdate(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel, instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        this.ui.dialogInstituicaoCursoPeriodoProfessorOperation = ModalOperation.update;
        let instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas = Object.assign([], instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas);
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessorUpdating = instituicaoCursoOcorrenciaPeriodoProfessor;
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor = Object.assign({}, instituicaoCursoOcorrenciaPeriodoProfessor);
        this.openDialogInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodo);
        setImmediate(() => {
            this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas = instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas;
        });
    }

    getTableAlunos(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {

        let columns = [
            new CardTableColumn({
                value: (item: InstituicaoCursoOcorrenciaPeriodoAlunoModel) => item.aluno.label,
                label: () => 'Nome'
            }),
            new CardTableColumn({
                value: (item: InstituicaoCursoOcorrenciaPeriodoAlunoModel) => item.instituicaoCursoPeriodo.label,
                label: () => 'Periodo'
            }),
            new CardTableColumn({
                value: (item: InstituicaoCursoOcorrenciaPeriodoAlunoModel) => item.instituicaoCursoTurma.label,
                label: () => 'Turma'
            }),
        ];

        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry({
                label: (item) => 'Atualizar',
                method: (item) => this.openDialogInstituicaoCursoOcorrenciaPeriodoAlunoToUpdate(item, instituicaoCursoOcorrenciaPeriodo),
                btnClass: (item) => ['btn-primary'],
                iconClass: (item) => ['fa', 'fa-edit']
            }),
            new CardTableMenuEntry({
                label: (item) => 'Remover',
                method: (item) => this.removeInstituicaoCursoOcorrenciaPeriodoAluno(item, instituicaoCursoOcorrenciaPeriodo),
                btnClass: (item) => ['btn-danger'],
                iconClass: (item) => ['fa', 'fa-remove']
            })
        ];
        menu.main = [
            new CardTableMenuEntry({
                label: (item) => 'Adicionar',
                method: (item) => this.openDialogInstituicaoCursoOcorrenciaPeriodoAlunoToAdd(instituicaoCursoOcorrenciaPeriodo),
                btnClass: (item) => ['btn-primary'],
                iconClass: (item) => ['fa', 'fa-plus']
            }),
        ];

        return { columns: columns, menu: menu };
    }

    getTableProfessores(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {

        let columns = [
            new CardTableColumn({
                value: (item: InstituicaoCursoOcorrenciaPeriodoProfessorModel) => item.professor.label,
                label: () => 'Nome'
            }),
            new CardTableColumn({
                value: (item: InstituicaoCursoOcorrenciaPeriodoProfessorModel) => item.cursoGradeMateria.label,
                label: () => 'Matéria'
            }),
            new CardTableColumn({
                value: (item: InstituicaoCursoOcorrenciaPeriodoProfessorModel) => item.instituicaoCursoPeriodo.label,
                label: () => 'Periodo'
            }),
            new CardTableColumn({
                value: (item: InstituicaoCursoOcorrenciaPeriodoProfessorModel) => item.instituicaoCursoTurma.label,
                label: () => 'Turma'
            }),
        ];

        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry({
                label: (item) => 'Atualizar',
                method: (item) => this.openDialogInstituicaoCursoOcorrenciaPeriodoProfessorToUpdate(item, instituicaoCursoOcorrenciaPeriodo),
                btnClass: (item) => ['btn-primary'],
                iconClass: (item) => ['fa', 'fa-edit']
            }),
            new CardTableMenuEntry({
                label: (item) => 'Remover',
                method: (item) => this.removeInstituicaoCursoOcorrenciaPeriodoProfessor(item, instituicaoCursoOcorrenciaPeriodo),
                btnClass: (item) => ['btn-danger'],
                iconClass: (item) => ['fa', 'fa-remove']
            })
        ];
        menu.main = [
            new CardTableMenuEntry({
                label: (item) => 'Adicionar',
                method: (item) => this.openDialogInstituicaoCursoOcorrenciaPeriodoProfessorToAdd(instituicaoCursoOcorrenciaPeriodo),
                btnClass: (item) => ['btn-primary'],
                iconClass: (item) => ['fa', 'fa-plus']
            })
        ];

        return { columns: columns, menu: menu };
    }

    getTableProfessorPeriodoAulas(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel) {

        let columns = [
            new CardTableColumn({
                value: (item: InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel) => item.dia.toString(),
                label: () => 'Dia'
            }),
            new CardTableColumn({
                value: (item: InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel) => item.inicio,
                label: () => 'Início'
            }),
            new CardTableColumn({
                value: (item: InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel) => item.fim,
                label: () => 'Fim'
            })
        ];

        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry({
                label: (item) => 'Remover',
                method: (item) => this.removerInstituicaoCursoOcorenciaPeriodoProfessorPeriodoAula(item),
                btnClass: (item) => ['btn-danger'],
                iconClass: (item) => ['fa', 'fa-remove']
            })
        ];

        return { columns: columns, menu: menu };
    }

}