import { MateriaModel } from './materia.model';
import { BaseModel } from './base.model';

export class CursoGradeMateriaModel extends BaseModel<number> {

    nomeExibicao: string;
    descricao: string;
    tags: string;
    numeroAulas: string;
    grupo: string;
    materia: MateriaModel;

}