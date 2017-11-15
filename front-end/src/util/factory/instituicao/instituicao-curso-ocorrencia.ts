import { Instituicao } from './instituicao';
import { Professor } from '../usuario/professor';

export class InstituicaoCursoOcorrencia {
    
    constructor() {
    }

    id?: string;
    coordenador: Professor;
    dataInicio: Date;
    dataFim: Date;
}