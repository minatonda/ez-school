import { CursoGradeMateriaModel } from './curso-grade-materia.model';
import { BaseModel } from './base.model';
import { InstituicaoModel } from './instituicao.model';

export class CursoGradeModel extends BaseModel<number> {

    constructor() {
        super();
        this.materias = new Array<CursoGradeMateriaModel>();
    }

    descricao: string = null;
    instituicao: InstituicaoModel = null;
    materias: Array<CursoGradeMateriaModel> = null;

}