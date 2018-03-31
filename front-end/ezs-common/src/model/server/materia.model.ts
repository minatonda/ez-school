import { MateriaRelacionamentoModel } from './materia-relacionamento.model';
import { BaseModel } from './base.model';

export class MateriaModel  extends BaseModel < number > {

    constructor() {
        super();
        this.materiasRelacionadas = new Array<MateriaRelacionamentoModel>();
    }

    nome: string = null;
    descricao: string = null;
    materiasRelacionadas: Array<MateriaRelacionamentoModel> = null;

}