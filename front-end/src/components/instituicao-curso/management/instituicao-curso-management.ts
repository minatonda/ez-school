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

@Component({
    template: require('./instituicao-curso-management.html')
})
export class InstituicaoCursoManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: InstituicaoCurso = new InstituicaoCurso();

    curso: Curso = new Curso();

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
                // this.model = await InstituicaoFactory.dtl(parseInt(this.$route.params.id), true);
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

    @Watch('curso', {
        deep: true,
        immediate: true
    })
    public async onCursoChanged() {
        if (this.curso) {
            this.model.curso = this.curso;
            this.model.cursoGrade = null;
            this.cursoGrades = await CursoFactory.getGrades(this.curso.id);
        }
    }

    async save() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RouterPathType.add): {
                    await InstituicaoFactory.addCurso(1, this.model, true);
                    break;
                }
                case (RouterPathType.upd): {
                    // await InstituicaoFactory.upd(this.model, true);
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