import { CursoGradeModel } from './curso-grade.model';

export class CursoModel {
    constructor() {
        this.grades = new Array<CursoGradeModel>();
    }
    id?: string;
    nome: string;
    descricao: string;
    grades: Array<CursoGradeModel>;
}