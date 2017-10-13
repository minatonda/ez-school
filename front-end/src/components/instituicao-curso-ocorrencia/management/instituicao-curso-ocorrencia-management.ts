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

@Component({
    template: require('./instituicao-curso-ocorrencia-management.html')
})
export class InstituicaoCursoOcorrenciaManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: InstituicaoCursoOcorrencia = new InstituicaoCursoOcorrencia();

    clearCursoGrade = false;

    cursos: Array<Curso> = new Array<Curso>();
    cursoGrades: Array<CursoGrade> = new Array<CursoGrade>();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.cursos = await CursoFactory.all();
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


    public getCursos() {
        return this.cursos;
    }

    public getCursoGrades() {
        return this.cursoGrades;
    }

    public async onCursoChanged(curso) {
        this.clearCursoGrade = true;
        setImmediate(() => { this.clearCursoGrade = false; });
        if (curso) {
            this.cursoGrades = await CursoFactory.allGrade(curso.id);
        }
        else {
            this.cursoGrades = [];
        }
    }

    async save() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RouterPathType.add): {
                    await InstituicaoFactory.addCursoOcorrencia(this.$route.params.idInstituicao, this.$route.params.idCurso, this.model, true);
                    break;
                }
                case (RouterPathType.upd): {
                    await InstituicaoFactory.updateCursoOcorrencia(this.$route.params.idInstituicao, this.$route.params.idCurso, this.model, true);
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