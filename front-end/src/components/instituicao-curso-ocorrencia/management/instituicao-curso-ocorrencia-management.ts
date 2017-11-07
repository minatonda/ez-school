import { Vue } from 'vue-property-decorator';
import { Component, Prop, Watch } from 'vue-property-decorator';
import { RouterPathType } from '../../../util/router/router.path';
import { BroadcastEventBus, BroadcastEvent } from '../../../util/broadcast/broadcast.event-bus';
import { RouterManager } from '../../../util/router/router.manager';
import { InstituicaoCursoOcorrencia } from '../../../util/factory/instituicao/instituicao-curso-ocorrencia';
import { InstituicaoFactory } from '../../../util/factory/instituicao/instituicao.factory';
import { Curso } from '../../../util/factory/curso/curso';
import { CursoGrade } from '../../../util/factory/curso/curso-grade';
import { CursoFactory } from '../../../util/factory/curso/curso.factory';
import { InstituicaoCursoPeriodo } from '../../../util/factory/instituicao/instituicao-curso-periodo';
import { Professor } from '../../../util/factory/usuario/professor';
import { CursoGradeMateria } from '../../../util/factory/curso/curso-grade-materia';
import { Aluno } from '../../../util/factory/usuario/aluno';
import { Usuario } from '../../../util/factory/usuario/usuario';
import { UsuarioFactory } from '../../../util/factory/usuario/usuario.factory';
import { InstituicaoCursoTurma } from '../../../util/factory/instituicao/instituicao-curso-turma';
import { InstituicaoCursoOcorrenciaAluno } from '../../../util/factory/instituicao/instituicao-curso-ocorrencia-aluno';
import { InstituicaoCursoOcorrenciaProfessor } from '../../../util/factory/instituicao/instituicao-curso-ocorrencia-professor';

enum ModalOperation {
    aluno = 0,
        professor = 1
}

interface UI {
    periodos: Array < InstituicaoCursoPeriodo > ;
    turmas: Array < InstituicaoCursoTurma > ;
    cursoGradeMaterias: Array < CursoGradeMateria > ;
    modalOperation: ModalOperation;
    queryAluno: any;
    queryProfessor: any;
    usuarioInfoLabel: any;
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
        periodos: undefined,
        cursoGradeMaterias: undefined,
        turmas: undefined,
        modalOperation: undefined,
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
    };

    model: InstituicaoCursoOcorrencia = new InstituicaoCursoOcorrencia();

    constructor() {
        super();
    }

    created() {

    }

    openModalRegistro() {
        (this.$refs['modal-aluno-professor'] as any).show();
    }

    closeModalRegistro() {
        (this.$refs['modal-aluno-professor'] as any).hide();
    }

    setModalRegistroAluno() {
        this.ui.modalOperation = ModalOperation.aluno;
    }

    setModalRegistroProfessor() {
        this.ui.modalOperation = ModalOperation.professor;
    }

    isModalRegistroAluno() {
        return this.ui.modalOperation === ModalOperation.aluno;
    }

    isModalRegistroProfessor() {
        return this.ui.modalOperation === ModalOperation.professor;
    }

    alunosPorPeriodo(periodo: InstituicaoCursoPeriodo) {
        return this.model.alunos.filter(x => x.periodo.id === periodo.id);
    }

    professoresPorPeriodo(periodo: InstituicaoCursoPeriodo) {
        return this.model.professores.filter(x => x.periodo.id === periodo.id);
    }

    addRegistro(aluno: Aluno, professor: Professor, turma: InstituicaoCursoTurma, periodo: InstituicaoCursoPeriodo, cursoGradeMateria: CursoGradeMateria) {
        switch (this.ui.modalOperation) {
            case (ModalOperation.aluno):
                {
                    this.addAluno(aluno, turma, periodo);
                    break;
                }
            case (ModalOperation.professor):
                {
                    this.addProfessor(professor, turma, periodo, cursoGradeMateria);
                    break;
                }
        }
    }

    addAluno(aluno: Aluno, turma: InstituicaoCursoTurma, periodo: InstituicaoCursoPeriodo) {
        let instituicaoCursoOcorrencia = new InstituicaoCursoOcorrenciaAluno();
        instituicaoCursoOcorrencia.aluno = aluno;
        instituicaoCursoOcorrencia.periodo = periodo;
        instituicaoCursoOcorrencia.turma = turma;
        this.model.alunos.push(instituicaoCursoOcorrencia);
    }

    addProfessor(professor: Professor, turma: InstituicaoCursoTurma, periodo: InstituicaoCursoPeriodo, cursoGradeMateria: CursoGradeMateria) {
        let instituicaoCursoOcorrencia = new InstituicaoCursoOcorrenciaProfessor();
        instituicaoCursoOcorrencia.professor = professor;
        instituicaoCursoOcorrencia.periodo = periodo;
        instituicaoCursoOcorrencia.turma = turma;
        instituicaoCursoOcorrencia.cursoGradeMateria = cursoGradeMateria;
        this.model.professores.push(instituicaoCursoOcorrencia);
    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.ui.periodos = await InstituicaoFactory.allPeriodo(this.$route.params.id, this.$route.params.idCurso);
            this.ui.cursoGradeMaterias = await InstituicaoFactory.allCursoGradeMaterias(this.$route.params.id, this.$route.params.idCurso);
            this.ui.turmas = await InstituicaoFactory.allTurma(this.$route.params.id, this.$route.params.idCurso);
            let horas = await InstituicaoFactory.allPeriodoHoraLivre(this.$route.params.id, this.$route.params.idCurso, this.ui.periodos[0].id);
            if (this.operation === RouterPathType.upd) {
                // this.model = await InstituicaoFactory.detailCurso(this.$route.params.id, this.$route.params.idInstituicao, true);
            }
        }
        catch (e) {
            RouterManager.redirectRoutePrevious();
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
                        await InstituicaoFactory.addCursoOcorrencia(this.$route.params.id, this.$route.params.idCurso, this.model, true);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        // await InstituicaoFactory.updateCursoOcorrencia(this.$route.params.idInstituicao, this.$route.params.idCurso, this.$route.params.dataInicio, this.model, true);
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