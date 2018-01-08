import { MateriaRelacionamento } from './materia-relacionamento';

export class Materia {

    constructor() {
        this.materiasRelacionadas = new Array<MateriaRelacionamento>();
    }

    id?: string;
    nome: string;
    descricao: string;
    materiasRelacionadas: Array<MateriaRelacionamento>;
}