import { CursoGradeMateriaModel } from './curso-grade-materia.model';

export class CursoGradeModel {
    constructor() {
        this.materias = new Array<CursoGradeMateriaModel>();
    }
    id?: string;
    descricao: string;
    materias: Array<CursoGradeMateriaModel>;
}