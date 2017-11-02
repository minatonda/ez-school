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

interface UI {
    periodo: InstituicaoCursoPeriodo;
    professores: Array < Professor > ;
    periodos: Array < InstituicaoCursoPeriodo > ;
    cursoGradeMaterias: Array < CursoGradeMateria > ;
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
        periodo: undefined,
        professores: undefined,
        periodos: undefined,
        cursoGradeMaterias: undefined
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
            this.ui.periodos = await InstituicaoFactory.allPeriodo(this.$route.params.id, this.$route.params.idCurso, this.$route.params.dataInicio);
            this.ui.cursoGradeMaterias = await InstituicaoFactory.allCursoGradeMaterias(this.$route.params.id, this.$route.params.idCurso, this.$route.params.dataInicio);
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
                        await InstituicaoFactory.addCursoOcorrencia(this.$route.params.idInstituicao, this.$route.params.idCurso, this.$route.params.dataInicio, this.model, true);
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