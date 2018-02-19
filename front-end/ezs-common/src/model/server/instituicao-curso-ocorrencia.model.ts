import { InstituicaoModel } from './instituicao.model';
import { InstituicaoCursoOcorrenciaPeriodoModel } from './instituicao-curso-ocorrencia-periodo.model';
import { ProfessorModel } from './professor.model';

export class InstituicaoCursoOcorrenciaModel {

    constructor() {
        this.instituicaoCursoOcorrenciaPeriodos = new Array < InstituicaoCursoOcorrenciaPeriodoModel > ();
    }

    id?: string;
    coordenador: ProfessorModel;
    dataInicio: string;
    dataExpiracao: string;
    instituicaoCursoOcorrenciaPeriodos: Array < InstituicaoCursoOcorrenciaPeriodoModel > ;
}