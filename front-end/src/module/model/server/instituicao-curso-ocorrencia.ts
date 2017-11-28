import { Instituicao } from './instituicao';
import { InstituicaoCursoOcorrenciaPeriodo } from './instituicao-curso-ocorrencia-periodo';
import { Professor } from './professor';

export class InstituicaoCursoOcorrencia {

    constructor() {
        this.instituicaoCursoOcorrenciaPeriodos = new Array < InstituicaoCursoOcorrenciaPeriodo > ();
    }

    id?: string;
    coordenador: Professor;
    dataInicio: Date;
    dataFim: Date;
    instituicaoCursoOcorrenciaPeriodos: Array < InstituicaoCursoOcorrenciaPeriodo > ;
}