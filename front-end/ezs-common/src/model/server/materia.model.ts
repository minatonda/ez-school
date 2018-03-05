import { MateriaRelacionamentoModel } from './materia-relacionamento.model';
import { BaseModel } from './base.model';

export class MateriaModel  extends BaseModel < number > {

    constructor() {
        super();
        this.materiasRelacionadas = new Array<MateriaRelacionamentoModel>();
    }

    nome: string;
    descricao: string;
    materiasRelacionadas: Array<MateriaRelacionamentoModel>;

}