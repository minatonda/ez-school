import { MateriaRelacionamentoModel } from './materia-relacionamento.model';

export class MateriaModel {

    constructor() {
        this.materiasRelacionadas = new Array<MateriaRelacionamentoModel>();
    }

    id?: string;
    nome: string;
    descricao: string;
    materiasRelacionadas: Array<MateriaRelacionamentoModel>;
}