import { Vue } from 'vue-property-decorator';
import { Component, Prop, Watch } from 'vue-property-decorator';
import { RouterPathType } from '../../../module/model/client/route-path';
import { Router } from '../../../router';
import { BroadcastEventBus, BroadcastEvent } from '../../../module/broadcast.event-bus';
import { UsuarioFactory } from '../../../module/factory/usuario.factory';
import { InstituicaoFactory } from '../../../module/factory/instituicao.factory';
import { Aluno } from '../../../module/model/server/aluno';
import { Professor } from '../../../module/model/server/professor';
import { CursoGradeMateria } from '../../../module/model/server/curso-grade-materia';
import { InstituicaoCursoTurma } from '../../../module/model/server/instituicao-curso-turma';
import { InstituicaoCursoPeriodo } from '../../../module/model/server/instituicao-curso-periodo';
import { InstituicaoCursoOcorrenciaPeriodo } from '../../../module/model/server/instituicao-curso-ocorrencia-periodo';
import { InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula, DayOfWeek } from '../../../module/model/server/instituicao-curso-ocorrencia-periodo-professor-periodo-aula';
import { EnumLabel, DayOfWeekEnumLabel } from '../../../module/constant/enum-label.constant';
import { InstituicaoCursoOcorrencia } from '../../../module/model/server/instituicao-curso-ocorrencia';
import { InstituicaoCursoOcorrenciaPeriodoAluno } from '../../../module/model/server/instituicao-curso-ocorrencia-periodo-aluno';
import { InstituicaoCursoOcorrenciaPeriodoProfessor } from '../../../module/model/server/instituicao-curso-ocorrencia-periodo-professor';
import moment from 'moment';
import { InstituicaoCursoOcorrenciaAluno } from '../../../module/model/server/instituicao-curso-ocorrencia-aluno';

enum ModalOperation {
    add = 0,
        update = 1
}


interface UI {

    cursoGradeMaterias: Array < CursoGradeMateria > ;
    instituicaoCursoTurmas: Array < InstituicaoCursoTurma > ;
    instituicaoCursoPeriodos: Array < InstituicaoCursoPeriodo > ;

    instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodo;
    instituicaoCUrsoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAluno;
    instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessor;
    instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula: InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula;

    dayOfWeek: DayOfWeek;
    dayOfWeeks: Array < EnumLabel > ;

    queryAluno: any;
    queryProfessor: any;

    usuarioInfoLabel: any;

    dialogInstituicaoCursoPeriodoOperation: ModalOperation;
    dialogInstituicaoCursoPeriodoAlunoOperation: ModalOperation;
    dialogInstituicaoCursoPeriodoProfessorOperation: ModalOperation;
}

@Component({
    template: require('./instituicao-curso-ocorrencia-management.html')
})
export class InstituicaoCursoOcorrenciaManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    ui: UI = {
        cursoGradeMaterias: undefined,
        instituicaoCursoTurmas: undefined,
        instituicaoCursoPeriodos: undefined,

        instituicaoCursoOcorrenciaPeriodo: new InstituicaoCursoOcorrenciaPeriodo(),
        instituicaoCUrsoOcorrenciaPeriodoAluno: new InstituicaoCursoOcorrenciaPeriodoAluno(),
        instituicaoCursoOcorrenciaPeriodoProfessor: new InstituicaoCursoOcorrenciaPeriodoProfessor(),
        instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula: undefined,

        dayOfWeek: undefined,
        dayOfWeeks: DayOfWeekEnumLabel,

        queryAluno: async(term) => {
            let itens = await UsuarioFactory.allAluno(term);
            return itens;
        },
        queryProfessor: async(term) => {
            let itens = await UsuarioFactory.allProfessor(term);
            return itens;
        },

        usuarioInfoLabel: (item) => {
            let obj: any = {};
            obj.key = item.label;
            obj.label = `<div><span>${item.label}</span><div><div><span style="float:left;">${item.usuarioInfo.rg}</span><span style="float:right;">${item.usuarioInfo.cpf}</span></div>`;
            return obj;
        },

        dialogInstituicaoCursoPeriodoOperation: ModalOperation.add,
        dialogInstituicaoCursoPeriodoAlunoOperation: ModalOperation.add,
        dialogInstituicaoCursoPeriodoProfessorOperation: ModalOperation.add,
    };

    model: InstituicaoCursoOcorrencia = new InstituicaoCursoOcorrencia();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RouterPathType.upd) {
                this.model = await InstituicaoFactory.detailInstituicaoCursoOcorrencia(this.$route.params.id, this.$route.params.idInstituicaoCurso, this.$route.params.idInstituicaoCursoOcorrencia, true);
            }
            this.ui.cursoGradeMaterias = await InstituicaoFactory.allCursoGradeMaterias(this.$route.params.id, this.$route.params.idInstituicaoCurso);
            this.ui.instituicaoCursoTurmas = await InstituicaoFactory.allInstituicaoCursoTurma(this.$route.params.id, this.$route.params.idInstituicaoCurso);
            this.ui.instituicaoCursoPeriodos = await InstituicaoFactory.allInstituicaoCursoPeriodo(this.$route.params.id, this.$route.params.idInstituicaoCurso);
        }
        catch (e) {
            Router.redirectRoutePrevious();
        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    async save() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RouterPathType.add):
                    {
                        await InstituicaoFactory.addInstituicaoCursoOcorrencia(this.$route.params.id, this.$route.params.idInstituicaoCurso, this.model, true);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await InstituicaoFactory.updateInstituicaoCursoOcorrencia(this.$route.params.id, this.$route.params.idInstituicaoCurso, this.model, true);
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

    isSelectPeriodoAula() {
        return !!this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.professor && !!this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.cursoGradeMateria;
    }

    onChangeProfessorAndCursoGradeMateria() {
        this.resetDialogInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas();
        this.$forceUpdate();
    }


    saveInstituicaoCursoOcorrenciaPeriodo(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodo) {
        if (this.ui.dialogInstituicaoCursoPeriodoOperation === ModalOperation.add) {
            this.addInstituicaoCursoOcorrenciaPeriodo(instituicaoCursoOcorrenciaPeriodo);
        }
        else {

        }
    }

    saveInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAluno) {
        if (this.ui.dialogInstituicaoCursoPeriodoAlunoOperation === ModalOperation.add) {
            this.addInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno);
        }
        else {

        }
    }

    saveInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessor) {
        if (this.ui.dialogInstituicaoCursoPeriodoProfessorOperation === ModalOperation.add) {
            this.addInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor);
        }
        else {

        }
    }


    addInstituicaoCursoOcorrenciaPeriodo(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodo) {
        this.model.instituicaoCursoOcorrenciaPeriodos.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodo(), instituicaoCursoOcorrenciaPeriodo));
        this.closeDialogInstituicaoCursoOcorrenciaPeriodo();
    }

    addInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAluno) {
        this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoAlunos.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodoAluno(), instituicaoCursoOcorrenciaPeriodoAluno));
        this.backFromInstituicaoCursoOcorrenciaPeriodoAlunoToInstituicaoCursoOcorrenciaPeriodo();
    }

    removeInstituicaoCursoOcorrenciaPeriodoAluno(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAluno) {
        this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoAlunos.splice(this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoAlunos.indexOf(instituicaoCursoOcorrenciaPeriodoAluno), 1);
    }

    addInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessor) {
        this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoProfessores.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodoProfessor(), instituicaoCursoOcorrenciaPeriodoProfessor));
        this.backFromInstituicaoCursoOcorrenciaProfessorToInstituicaoCursoOcorrenciaPeriodo();
    }

    removeInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessor) {
        this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoProfessores.splice(this.ui.instituicaoCursoOcorrenciaPeriodo.instituicaoCursoOcorrenciaPeriodoProfessores.indexOf(instituicaoCursoOcorrenciaPeriodoProfessor), 1);
    }

    addInstituicaoCursoOcorenciaPeriodoProfessorPeriodoAula(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula: InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula) {
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.push(Object.assign(new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(), instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula));
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = undefined;
    }


    getPeriodosDisponiveis(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodo, instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessor, dayOfWeek: EnumLabel) {
        if (instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoPeriodo) {
            let periodosDisponiveis = new Array < InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula > ();
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
                        let instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula();
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
                        let instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula();
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

    getDayWeekLabel(dayOfWeek: DayOfWeek) {
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
        this.ui.instituicaoCursoOcorrenciaPeriodo = new InstituicaoCursoOcorrenciaPeriodo();
        this.openDialogInstituicaoCursoOcorrenciaPeriodo();
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoToUpdate(instituicaoCursoOcorrenciaPeriodo: InstituicaoCursoOcorrenciaPeriodo) {
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
        this.ui.instituicaoCUrsoOcorrenciaPeriodoAluno = new InstituicaoCursoOcorrenciaPeriodoAluno();
        this.openDialogInstituicaoCursoOcorrenciaPeriodoAluno();
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoAlunoToUpdate(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAluno) {
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
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor = new InstituicaoCursoOcorrenciaPeriodoProfessor();
        this.openDialogInstituicaoCursoOcorrenciaPeriodoProfessor();
    }

    openDialogInstituicaoCursoOcorrenciaPeriodoProfessorToUpdate(instituicaoCursoOcorrenciaPeriodoProfessor: InstituicaoCursoOcorrenciaPeriodoProfessor) {
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
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessor.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas = new Array < InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula > ();
        this.ui.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = undefined;
    }

}