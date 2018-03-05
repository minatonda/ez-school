import { MateriaModel } from './materia.model';
import { BaseModel } from './base.model';

export class MateriaRelacionamentoModel extends BaseModel < number > {

    materiaPai: MateriaModel;

}