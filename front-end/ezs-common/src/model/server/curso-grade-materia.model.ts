import { MateriaModel } from './materia.model';
import { BaseModel } from './base.model';

export class CursoGradeMateriaModel extends BaseModel < number > {

    descricao: string;
    materia: MateriaModel;

}