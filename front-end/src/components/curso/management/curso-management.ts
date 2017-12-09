import { Vue } from 'vue-property-decorator';
import axios, { AxiosResponse } from 'axios';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType, RouterPath } from '../../../module/model/client/route-path';
import { BroadcastEventBus, BroadcastEvent } from '../../../module/broadcast.event-bus';
import { Router } from '../../../router';
import { CursoFactory } from '../../../module/factory/curso.factory';
import { Curso } from '../../../module/model/server/curso';
import { Materia } from '../../../module/model/server/materia';
import { MateriaFactory } from '../../../module/factory/materia.factory';
import { CursoGrade } from '../../../module/model/server/curso-grade';
import { CursoGradeMateria } from '../../../module/model/server/curso-grade-materia';

enum ModalOperation {
    add = 0,
        update = 1
}

interface UI {
    materias: Array < Materia > ;
    cursoGrade: CursoGrade;
    cursoGradeMateria: CursoGradeMateria;
    dialogCursoGradeOperation: ModalOperation;
}

@Component({
    template: require('./curso-management.html')
})
export class CursoManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: Curso = new Curso();

    ui: UI = {
        materias: undefined,
        cursoGrade: new CursoGrade(),
        cursoGradeMateria: new CursoGradeMateria(),
        dialogCursoGradeOperation: ModalOperation.add
    };

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.ui.materias = await MateriaFactory.all();
            if (this.operation === RouterPathType.upd) {
                this.model = await CursoFactory.detail(this.$route.params.id, true);
            }
        }
        catch (e) {
            Router.redirectRoutePrevious();
        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    saveCursoGrade(cursoGrade: CursoGrade) {
        if (this.ui.dialogCursoGradeOperation === ModalOperation.add) {
            this.addCursoGrade(cursoGrade);
        }
        else {

        }
        this.closeDialogCursoGrade();
    }

    async save() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RouterPathType.add):
                    {
                        let result = await CursoFactory.add(this.model, true);
                        Router.redirectRoute(RouterPath.CURSO_UPD, result);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await CursoFactory.update(this.model, true);
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


    addCursoGrade(cursoGrade: CursoGrade) {
        this.model.grades.push(Object.assign(new CursoGrade(), cursoGrade));
    }

    removeCursoGrade(cursoGrade: CursoGrade) {
        this.model.grades.splice(this.model.grades.indexOf(cursoGrade), 1);
    }

    addCursoGradeMateria(cursoGradeMateria: CursoGradeMateria) {
        this.ui.cursoGrade.materias.push(Object.assign(new CursoGradeMateria(), cursoGradeMateria));
    }

    removeCursoGradeMateria(cursoGradeMateria: CursoGradeMateria) {
        this.ui.cursoGrade.materias.splice(this.ui.cursoGrade.materias.indexOf(cursoGradeMateria), 1);
    }


    openDialogCursoGrade() {
        (this.$refs['modal-curso-grade'] as any).show();
    }

    closeDialogCursoGrade() {
        (this.$refs['modal-curso-grade'] as any).hide();
    }

    openDialogCursoGradeToAdd() {
        this.ui.dialogCursoGradeOperation = ModalOperation.add;
        this.ui.cursoGrade = new CursoGrade();
        this.openDialogCursoGrade();
    }

    openDialogCursoGradeUpdate(cursoGrade: CursoGrade) {
        this.ui.dialogCursoGradeOperation = ModalOperation.update;
        this.ui.cursoGrade = cursoGrade;
        this.openDialogCursoGrade();
    }

}