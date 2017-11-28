export class Materia {

    constructor() {
        this.materiasRelacionadas = new Array<Materia>();
    }

    id?: string;
    nome: string;
    descricao: string;
    materiasRelacionadas: Array<Materia>;
}