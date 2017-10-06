import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../../../common/card-table/card-table.types';
import { MateriaFactory } from '../../../../util/factory/materia/materia.factory';
import { CursoFactory } from '../../../../util/factory/curso/curso.factory';
import { Curso } from '../../../../util/factory/curso/curso';
import { CursoGrade } from '../../../../util/factory/curso/curso-grade';
import { Materia } from '../../../../util/factory/materia/materia';
import { CursoGradeMateria } from '../../../../util/factory/curso/curso-grade-materia';

@Component({
    template: require('./curso-grade-management.html')
})
export class CursoGradeManagementComponent extends Vue {

    @Prop()
    curso: Curso = new Curso();

    model: CursoGrade = undefined;

    grades:Array<CursoGrade> = new Array<CursoGrade>();
    materias: Array<Materia> = new Array<Materia>();
    cursoGradeMateria: CursoGradeMateria = new CursoGradeMateria();

    action: string = 'list';
    tab: string = 'list';

    constructor() {
        super();
    }

    created() {

    }

    public setAction(action) {
        this.action = action;
    }
    public isAction(action) {
        return this.action === action;
    }

    public setTab(tab) {
        this.tab = tab;
    }
    public isTab(tab) {
        return this.tab === tab;
    }

    public enableAdd() {
        this.model = new CursoGrade();
        this.setAction('add');
        this.setTab('add');
    }
    public enableUpd(model) {
        this.model = model;
        this.setAction('upd');
        this.setTab('upd');
    }

    public disableActions() {
        this.model = undefined;
        this.setAction(undefined);
        this.setTab('list');
    }

    async mounted() {
        try {
            this.materias = await MateriaFactory.all();
            this.grades = await CursoFactory.getGrades(this.curso.id);
        }
        catch (e) {

        }
        finally {

        }
    }

    public getMaterias() {
        return this.materias;
    }

    public getColumnsGrade() {
        return [
            new CardTableColumn((item: CursoGrade) => item.id, () => 'ID'),
            new CardTableColumn((item: CursoGrade) => item.descricao, () => 'Descrição'),
        ];
    }

    public getItensGrade() {
        return this.grades;
    }

    public getMenuGrade() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry(
                (item) => this.enableUpd(item),
                (item) => 'Atualizar',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            )
        ];
        return menu;
    }


    public getColumnsMateria() {
        return [
            new CardTableColumn((item: CursoGradeMateria) => item.materia.nome, () => 'Materia'),
            new CardTableColumn((item: CursoGradeMateria) => item.descricao, () => 'Descrição'),
        ];
    }

    public getItensMateria() {
        return this.model && this.model.materias;
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

    public async addMateria(materia: CursoGradeMateria) {
        this.model.materias.push(materia);
        this.$forceUpdate();
    }

    public async removeMateria(materia: CursoGradeMateria) {
        this.model.materias.splice(this.model.materias.indexOf(materia), 1);
        this.$forceUpdate();
    }

    public async save() {
        switch(this.action){
            case('add'):{
                CursoFactory.addGrade(1, this.model);
                break;
            }
            case('upd'):{
                CursoFactory.updGrade(1, this.model);
                break;
            }
        }

    }

}