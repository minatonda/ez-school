import { CursoGrade } from './curso-grade';

export class Curso {
    constructor() {
        this.grades = new Array<CursoGrade>();
    }
    id?: string;
    nome: string;
    descricao: string;
    grades: Array<CursoGrade>;
}