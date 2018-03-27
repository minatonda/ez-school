import { Vue, Component, Prop } from 'vue-property-decorator';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { CursoModel } from '../../../../../ezs-common/src/model/server/curso.model';
import { CursoGradeModel } from '../../../../../ezs-common/src/model/server/curso-grade.model';
import { CursoGradeMateriaModel } from '../../../../../ezs-common/src/model/server/curso-grade-materia.model';
import { MateriaModel } from '../../../../../ezs-common/src/model/server/materia.model';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';
import { AppRouterPath } from '../../../app.router.path';

enum ModalOperation {
    add = 0,
    update = 1
}

interface UI {
    materias: Array<MateriaModel>;
    cursoGrade: CursoGradeModel;
    instituicoes: Array<InstituicaoModel>;
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
        instituicoes: undefined,
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
            this.ui.materias = await FACTORY_CONSTANT.MateriaFactory.all();
            this.ui.instituicoes = await FACTORY_CONSTANT.InstituicaoFactory.all();
            if (this.operation === RouterPathType.upd) {
                this.model = await FACTORY_CONSTANT.CursoFactory.detail(this.$route.params.id);
            }
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage(), I18N_ERROR_GENERIC.CONSULTAR_FALHA);
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
                        let result = await FACTORY_CONSTANT.CursoFactory.add(this.model);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await FACTORY_CONSTANT.CursoFactory.update(this.model);
                        break;
                    }
            }
            NotifyUtil.successG(I18N_ERROR_GENERIC.MODELO_SALVAR, ApplicationService.getLanguage());
            AppRouter.push(AppRouterPath.CURSO);
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage(), I18N_ERROR_GENERIC.MODELO_SALVAR_FALHA);
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

    getTableCursoGradeMateria() {

        let columns = [
            new CardTableColumn({
                value: (item: CursoGradeMateriaModel) => item.nomeExibicao,
                label: () => 'Nome de Exibição'
            }),
            new CardTableColumn({
                value: (item: CursoGradeMateriaModel) => item.descricao,
                label: () => 'Descrição'
            }),
            new CardTableColumn({
                value: (item: CursoGradeMateriaModel) => item.materia.label,
                label: () => 'Matéria'
            }),
            new CardTableColumn({
                value: (item: CursoGradeMateriaModel) => item.grupo,
                label: () => 'Grupo'
            })
        ];

        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry({
                label: (item) => 'Remover',
                method: (item) => this.removeCursoGradeMateria(item),
                btnClass: (item) => ['btn-danger'],
                iconClass: (item) => ['fa', 'fa-remove']
            })
        ];

        return { columns: columns, menu: menu };
    }

}