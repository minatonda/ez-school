import { Vue, Component, Prop } from 'vue-property-decorator';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { CursoModel } from '../../../../../ezs-common/src/model/server/curso.model';
import { CursoGradeModel } from '../../../../../ezs-common/src/model/server/curso-grade.model';
import { CursoGradeMateriaModel } from '../../../../../ezs-common/src/model/server/curso-grade-materia.model';
import { MateriaModel } from '../../../../../ezs-common/src/model/server/materia.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_MESSAGE } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';

enum ModalOperation {
    add = 0,
    update = 1
}

interface UI {
    materias: Array<MateriaModel>;
    cursoGrade: CursoGradeModel;
    cursoGradeMateria: CursoGradeMateriaModel;
    dialogCursoGradeOperation: ModalOperation;
}

@Component({
    template: require('./page-curso.html')
})
export class PageCursoComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: CursoModel = new CursoModel();

    ui: UI = {
        materias: undefined,
        cursoGrade: new CursoGradeModel(),
        cursoGradeMateria: new CursoGradeMateriaModel(),
        dialogCursoGradeOperation: ModalOperation.add
    };

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.ui.materias = await Factory.MateriaFactory.all();
            if (this.operation === RouterPathType.upd) {
                this.model = await Factory.CursoFactory.detail(this.$route.params.id);
            }
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.CONSULTAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
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
                        let result = await Factory.CursoFactory.add(this.model);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await Factory.CursoFactory.update(this.model);
                        break;
                    }
            }
            NotifyUtil.notifyI18N(I18N_MESSAGE.MODELO_SALVAR, ApplicationService.getLanguage(), NOTIFY_TYPE.SUCCESS);
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.MODELO_SALVAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    saveCursoGrade(cursoGrade: CursoGradeModel) {
        if (this.ui.dialogCursoGradeOperation === ModalOperation.add) {
            this.addCursoGrade(cursoGrade);
        }
        else {

        }
        this.closeDialogCursoGrade();
    }

    addCursoGrade(cursoGrade: CursoGradeModel) {
        this.model.grades.push(Object.assign(new CursoGradeModel(), cursoGrade));
    }

    removeCursoGrade(cursoGrade: CursoGradeModel) {
        this.model.grades.splice(this.model.grades.indexOf(cursoGrade), 1);
    }

    addCursoGradeMateria(cursoGradeMateria: CursoGradeMateriaModel) {
        this.ui.cursoGrade.materias.push(Object.assign(new CursoGradeMateriaModel(), cursoGradeMateria));
    }

    removeCursoGradeMateria(cursoGradeMateria: CursoGradeMateriaModel) {
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
        this.ui.cursoGrade = new CursoGradeModel();
        this.openDialogCursoGrade();
    }

    openDialogCursoGradeUpdate(cursoGrade: CursoGradeModel) {
        this.ui.dialogCursoGradeOperation = ModalOperation.update;
        this.ui.cursoGrade = cursoGrade;
        this.openDialogCursoGrade();
    }

}