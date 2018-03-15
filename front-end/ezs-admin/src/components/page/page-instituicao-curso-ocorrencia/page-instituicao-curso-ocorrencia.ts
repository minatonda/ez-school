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

import * as moment from 'moment';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';
import { UsuarioInfoModel } from '../../../../../ezs-common/src/model/server/usuario-info.model';

enum ModalOperation {
    add = 0,
        update = 1
}

interface UI {

    cursoGradeMaterias: Array < CursoGradeMateriaModel > ;
    instituicaoCursoTurmas: Array < InstituicaoCursoTurmaModel > ;
    instituicaoCursoPeriodos: Array < InstituicaoCursoPeriodoModel > ;

    instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel;
    instituicaoCUrsoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel;
    instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel;
    instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula: InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel;

    dayOfWeek: DayOfWeekModel;
    dayOfWeeks: Array < EnumLabel > ;

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
        instituicaoCursoOcorrenciaPeriodoProfessor: new InstituicaoCursoOcorrenciaPeriodoProfessorModel(),
        instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula: undefined,

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
        return !!this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.professor && !!this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.cursoGradeMateria;
    }

    onChangeProfessorAndCursoGradeMateria() {
        this.resetDialogInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas();
        this.$forceUpdate();
    }


    saveInstituicaoCursoOcorrenciaPeriodo(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        if (this.ui.dialogInstituicaoCursoPeriodoOperation === ModalOperation.add) {
            this.addInstituicaoCursoOcorrenciaPeriodo(instituicaoCursoOcorrenciaPeriodo);
        }
        else {

        }
    }

    saveInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel) {
        if (this.ui.dialogInstituicaoCursoPeriodoAlunoOperation === ModalOperation.add) {
            this.addInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno);
        }
        else {

        }
    }

    saveInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel) {
        if (this.ui.dialogInstituicaoCursoPeriodoProfessorOperation === ModalOperation.add) {
            this.addInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor);
        }
        else {

        }
    }

    addInstituicaoCursoOcorrenciaPeriodo(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        this.model.instituicaoCursoOcorrenciaPeriodos.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodoModel(), instituicaoCursoOcorrenciaPeriodo));
        this.closeDialogInstituicaoCursoOcorrenciaPeriodo();
    }

    addInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel) {
        this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoAlunos.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodoAlunoModel(), instituicaoCursoOcorrenciaPeriodoAluno));
        this.backFromInstituicaoCursoOcorrenciaPeriodoAlunoToInstituicaoCursoOcorrenciaPeriodo();
    }

    removeInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel) {
        this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoAlunos.splice(this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoAlunos.indexOf(instituicaoCursoOcorrenciaPeriodoAluno), 1);
    }

    addInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel) {
        this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoProfessores.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodoProfessorModel(), instituicaoCursoOcorrenciaPeriodoProfessor));
        this.backFromInstituicaoCursoOcorrenciaProfessorToInstituicaoCursoOcorrenciaPeriodo();
    }

    removeInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel) {
        this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoProfessores.splice(this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoProfessores.indexOf(instituicaoCursoOcorrenciaPeriodoProfessor), 1);
    }

    addInstituicaoCursoOcorenciaPeriodoProfessorPeriodoAula(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula: InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel) {
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel(), instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula));
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = undefined;
    }


    getPeriodosDisponiveis(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel, instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel, dayOfWeek: EnumLabel) {
        if (instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoPeriodo) {
            let periodosDisponiveis = new Array < InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel > ();
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


    openDialogInstituicaoCursoOcorrenciaPeriodo() {
        (this.$refs['modal-ocorrencia-periodo'] as any).show();
    }

    closeDialogInstituicaoCursoOcorrenciaPeriodo() {
        (this.$refs['modal-ocorrencia-periodo'] as any).hide();
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoToAdd() {
        this.ui.dialogInstituicaoCursoPeriodoOperation = ModalOperation.add;
        this.ui.instituicaoCursoOcorrenciaPeriodo = new InstituicaoCursoOcorrenciaPeriodoModel();
        this.openDialogInstituicaoCursoOcorrenciaPeriodo();
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoToUpdate(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodoModel) {
        this.ui.dialogInstituicaoCursoPeriodoOperation = ModalOperation.update;
        this.ui.instituicaoCursoOcorrenciaPeriodo = instituicaoCursoOcorrenciaPeriodo;
        this.openDialogInstituicaoCursoOcorrenciaPeriodo();
    }


    openDialogInstituicaoCursoOcorrenciaPeriodoAluno() {
        this.closeDialogInstituicaoCursoOcorrenciaPeriodo();
        setImmediate(() => {
            (this.$refs['modal-ocorrencia-periodo-aluno'] as any).show();
        });
    }

    closeDialogInstituicaoCursoOcorrenciaPeriodoAluno() {
        (this.$refs['modal-ocorrencia-periodo-aluno'] as any).hide();
    }


    openDialogInstituicaoCursoOcorrenciaPeriodoAlunoToAdd() {
        this.ui.dialogInstituicaoCursoPeriodoAlunoOperation = ModalOperation.add;
        this.ui.instituicaoCUrsoOcorrenciaPeriodoAluno = new InstituicaoCursoOcorrenciaPeriodoAlunoModel();
        this.openDialogInstituicaoCursoOcorrenciaPeriodoAluno();
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoAlunoToUpdate(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel) {
        this.ui.dialogInstituicaoCursoPeriodoAlunoOperation = ModalOperation.update;
        this.ui.instituicaoCUrsoOcorrenciaPeriodoAluno = instituicaoCursoOcorrenciaPeriodoAluno;
        this.openDialogInstituicaoCursoOcorrenciaPeriodoAluno();
    }

    backFromInstituicaoCursoOcorrenciaPeriodoAlunoToInstituicaoCursoOcorrenciaPeriodo() {
        this.closeDialogInstituicaoCursoOcorrenciaPeriodoAluno();
        this.openDialogInstituicaoCursoOcorrenciaPeriodo();
    }

    resetDialogInstituicaoCursoOcorrenciaPeriodoAluno() {
        this.ui.instituicaoCUrsoOcorrenciaPeriodoAluno.aluno = undefined;
        this.ui.instituicaoCUrsoOcorrenciaPeriodoAluno.instituicaoCursoPeriodo = undefined;
        this.ui.instituicaoCUrsoOcorrenciaPeriodoAluno.instituicaoCursoTurma = undefined;
    }


    openDialogInstituicaoCursoOcorrenciaPeriodoProfessor() {
        this.closeDialogInstituicaoCursoOcorrenciaPeriodo();
        setImmediate(() => {
            (this.$refs['modal-ocorrencia-periodo-professor'] as any).show();
        });
    }

    closeDialogInstituicaoCursoOcorrenciaPeriodoProfessor() {
        (this.$refs['modal-ocorrencia-periodo-professor'] as any).hide();
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoProfessorToAdd() {
        this.ui.dialogInstituicaoCursoPeriodoAlunoOperation = ModalOperation.add;
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor = new InstituicaoCursoOcorrenciaPeriodoProfessorModel();
        this.openDialogInstituicaoCursoOcorrenciaPeriodoProfessor();
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoProfessorToUpdate(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessorModel) {
        this.ui.dialogInstituicaoCursoPeriodoAlunoOperation = ModalOperation.update;
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor = instituicaoCursoOcorrenciaPeriodoProfessor;
        this.openDialogInstituicaoCursoOcorrenciaPeriodoProfessor();
    }

    backFromInstituicaoCursoOcorrenciaProfessorToInstituicaoCursoOcorrenciaPeriodo() {
        this.closeDialogInstituicaoCursoOcorrenciaPeriodoProfessor();
        this.openDialogInstituicaoCursoOcorrenciaPeriodo();
    }

    resetDialogInstituicaoCursoOcorrenciaPeriodoProfessor() {
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.professor = undefined;
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.cursoGradeMateria = undefined;
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoTurma = undefined;
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoPeriodo = undefined;
    }

    resetDialogInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas() {
        this.ui.dayOfWeek = undefined;
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas = new Array < InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel > ();
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = undefined;
    }

}