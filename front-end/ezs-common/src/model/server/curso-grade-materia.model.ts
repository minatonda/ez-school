import { MateriaModel } from './materia.model';
import { BaseModel } from './base.model';

export class CursoGradeMateriaModel extends BaseModel<number> {

    nomeExibicao: string = null;
    descricao: string = null;
    tags: string = null;
    numeroAulas: string = null;
    grupo: string = null;
    materia: MateriaModel = null;

}