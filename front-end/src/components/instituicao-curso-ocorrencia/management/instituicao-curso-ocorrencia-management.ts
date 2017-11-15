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
import { InstituicaoCursoOcorrenciaProfessorPeriodoAula } from '../../../util/factory/instituicao/instituicao-curso-ocorrencia-professor-periodo-aula';
import { INSPECT_MAX_BYTES } from 'buffer';

enum ModalOperation {
    aluno = 0,
        professor = 1
}

interface UI {
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
        }
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

    addAluno(aluno: Aluno) {
        let instituicaoCursoOcorrenciaAluno = new InstituicaoCursoOcorrenciaAluno();
        instituicaoCursoOcorrenciaAluno.aluno = aluno;
        this.model.instituicaoCursoOcorrenciaAlunos.push(instituicaoCursoOcorrenciaAluno);
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