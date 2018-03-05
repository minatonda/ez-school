import { CursoGradeMateriaModel } from './curso-grade-materia.model';
import { BaseModel } from './base.model';

export class CursoGradeModel extends BaseModel < number > {

    constructor() {
        super();
        this.materias = new Array < CursoGradeMateriaModel > ();
    }
    
    descricao: string;
    materias: Array < CursoGradeMateriaModel > ;

}