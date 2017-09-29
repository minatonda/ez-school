import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { Curso } from '../../../common/factory/curso/curso';
import { CursoFactory } from '../../../common/factory/curso/curso.factory';
import { BroadcastEvent } from '../../../common/vue/broadcast/broadcast.events';
import { BroadcastEventBus } from '../../../common/vue/broadcast/broadcast.event-bus';
import { Notify, MESSAGES } from '../../../common/modules/notify/notify';
import { RouterManager } from '../../../common/vue/router/router.manager';
import { RoutePathType } from '../../../common/vue/router/route-path-type';
import { prop } from 'vue-property-decorator/lib/vue-property-decorator';
import { CardTableColumn } from '../../common/card-table/types/card-table-column';
import { CursoGrade } from '../../../common/factory/curso/curso-grade';
import { CardTableMenu } from '../../common/card-table/types/card-table-menu';
import { CardTableMenuEntry } from '../../common/card-table/types/card-table-menu-entry';
import { Materia } from '../../../common/factory/materia/materia';
import { MateriaFactory } from '../../../common/factory/materia/materia.factory';
import { RoutePath } from '../../../common/vue/router/route-path';

@Component({
    template: require('./curso-add-upd.html')
})
export class CursoAddUpdComponent extends Vue {

    @prop()
    alias: string;
    @prop()
    operation: RoutePathType;

    model: Curso = new Curso();
    grade: CursoGrade = null;
    materias: Array<Materia> = new Array<Materia>();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RoutePathType.upd) {
                this.materias = await MateriaFactory.all();
                this.model = await CursoFactory.dtl(parseInt(this.$route.params.id), true);
                this.model.grades = await CursoFactory.getGrades(this.model.id);
            }
        }
        catch (e) {
            RouterManager.redirectRoutePrevious();
        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    public getMaterias() {
        return this.materias;
    }


    public getColumnsGrade() {
        return [
            new CardTableColumn((item: CursoGrade) => item.id, () => 'ID'),
        ];
    }

    public getItensGrade() {
        return this.model.grades;
    }

    public getMenuGrade() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => this.showModalGrade(item),
                (item) => 'Atualizar',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            )
        ];
        return menu;
    }


    public getColumnsMateria() {
        return [
            new CardTableColumn((item: Materia) => item.nome, () => 'Nome'),
        ];
    }

    public getItensMateria() {
        return this.grade && this.grade.materias;
    }

    public getMenuMateria() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => this.removeMateria(item),
                (item) => 'Remover',
                (item) => ['fa', 'fa-remove'],
                (item) => ['btn-primary']
            )
        ];
        return menu;
    }



    public showModalGrade(grade) {
        this.grade = grade;
        (this.$refs.modalGrade as any).show();
    }

    public hideModalGrade() {
        (this.$refs.modalGrade as any).hide();
    }

    public async selectMateria(materia: Materia) {
        try {
            if (this.grade) {
                await CursoFactory.addGradeMaterias(this.model.id, this.grade.id, materia, true);
                this.grade.materias.push(materia);
            }
            else {
                let grade = new CursoGrade();
                grade.materias.push(materia);
                this.grade = await CursoFactory.addGrade(this.model.id, grade, true);
                this.model.grades.push(this.grade);
            }
            this.$forceUpdate();
        } catch (error) {

        }
    }

    public async removeMateria(materia: Materia) {
        try {
            await CursoFactory.removeGradeMaterias(this.model.id, this.grade.id, materia.id, true);
            this.grade.materias.splice(this.grade.materias.indexOf(materia), 1);
            this.$forceUpdate();
        } catch (error) {

        }
    }

    async save() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RoutePathType.add): {
                    let result = await CursoFactory.add(this.model, true);
                    RouterManager.redirectRoute(RoutePath.CURSO_UPD, result);
                    break;
                }
                case (RoutePathType.upd): {
                    await CursoFactory.upd(this.model, true);
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