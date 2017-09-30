import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../../../common/card-table/card-table.types';
import { MateriaFactory } from '../../../../util/factory/materia/materia.factory';
import { CursoFactory } from '../../../../util/factory/curso/curso.factory';
import { Curso } from '../../../../util/factory/curso/curso';
import { CursoGrade } from '../../../../util/factory/curso/curso-grade';
import { Materia } from '../../../../util/factory/materia/materia';

@Component({
    template: require('./curso-grade-mng.html')
})
export class CursoGradeMngComponent extends Vue {

    @Prop()
    curso: Curso = new Curso();
    model: CursoGrade = null;
    materias: Array<Materia> = new Array<Materia>();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            this.materias = await MateriaFactory.all();
            this.curso.grades = await CursoFactory.getGrades(this.curso.id);
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
        ];
    }

    public getItensGrade() {
        return this.curso.grades;
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

    public showModalGrade(grade) {
        this.model = grade;
        (this.$refs.modalGrade as any).show();
    }

    public hideModalGrade() {
        (this.$refs.modalGrade as any).hide();
    }

    public async selectMateria(materia: Materia) {
        try {
            if (this.model) {
                await CursoFactory.addGradeMaterias(this.curso.id, this.model.id, materia, true);
                this.model.materias.push(materia);
            }
            else {
                let grade = new CursoGrade();
                grade.materias.push(materia);
                this.model = await CursoFactory.addGrade(this.curso.id, grade, true);
                this.curso.grades.push(this.model);
            }
            this.$forceUpdate();
        } catch (error) {

        }
    }

    public async removeMateria(materia: Materia) {
        try {
            await CursoFactory.removeGradeMaterias(this.curso.id, this.model.id, materia.id, true);
            this.model.materias.splice(this.model.materias.indexOf(materia), 1);
            this.$forceUpdate();
        } catch (error) {

        }
    }


}