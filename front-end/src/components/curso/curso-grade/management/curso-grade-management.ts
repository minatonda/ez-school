import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../../../common/card-table/card-table.types';
import { MateriaFactory } from '../../../../util/factory/materia/materia.factory';
import { CursoFactory } from '../../../../util/factory/curso/curso.factory';
import { Curso } from '../../../../util/factory/curso/curso';
import { CursoGrade } from '../../../../util/factory/curso/curso-grade';
import { Materia } from '../../../../util/factory/materia/materia';
import { CursoGradeMateria } from '../../../../util/factory/curso/curso-grade-materia';

interface UI {
    grades: Array < CursoGrade > ;
    materias: Array < Materia > ;
    cursoGradeMateria: CursoGradeMateria;
    action: string;
    tab: string;
}

@Component({
    template: require('./curso-grade-management.html')
})
export class CursoGradeManagementComponent extends Vue {

    @Prop({ type: Curso, default: null })
    curso: Curso = new Curso();

    model: CursoGrade = undefined;

    ui: UI = {
        grades: undefined,
        materias: undefined,
        cursoGradeMateria: new CursoGradeMateria(),
        action: 'list',
        tab: 'list'
    };



    constructor() {
        super();
    }

    created() {

    }

    public setAction(action) {
        this.ui.action = action;
    }
    public isAction(action) {
        return this.ui.action === action;
    }

    public setTab(tab) {
        this.ui.tab = tab;
    }
    public isTab(tab) {
        return this.ui.tab === tab;
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
            this.ui.materias = await MateriaFactory.all();
            this.ui.grades = await CursoFactory.allGrade(this.curso.id);
        }
        catch (e) {

        }
        finally {

        }
    }

    public getColumnsGrade() {
        return [
            new CardTableColumn((item: CursoGrade) => item.id, () => 'ID'),
            new CardTableColumn((item: CursoGrade) => item.descricao, () => 'Descrição'),
        ];
    }

    public getItensGrade() {
        return this.ui.grades;
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
        if (materia.materia) {
            this.model.materias.push(materia);
            this.$forceUpdate();
        }
    }

    public async removeMateria(materia: CursoGradeMateria) {
        this.model.materias.splice(this.model.materias.indexOf(materia), 1);
        this.$forceUpdate();
    }

    public async save() {
        switch (this.ui.action) {
            case ('add'):
                {
                    CursoFactory.addGrade(this.curso.id, this.model);
                    break;
                }
            case ('upd'):
                {
                    CursoFactory.updateGrade(this.curso.id, this.model);
                    break;
                }
        }

    }

}