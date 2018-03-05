import { CursoGradeModel } from './curso-grade.model';
import { BaseModel } from './base.model';

export class CursoModel extends BaseModel < number > {

    constructor() {
        super();
        this.grades = new Array<CursoGradeModel>();
    }

    nome: string;
    descricao: string;
    grades: Array<CursoGradeModel>;

}