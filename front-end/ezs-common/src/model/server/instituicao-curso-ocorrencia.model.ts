import { InstituicaoModel } from './instituicao.model';
import { InstituicaoCursoOcorrenciaPeriodoModel } from './instituicao-curso-ocorrencia-periodo.model';
import { ProfessorModel } from './professor.model';
import { BaseModel } from './base.model';

export class InstituicaoCursoOcorrenciaModel  extends BaseModel < number > {

    constructor() {
        super();
        this.instituicaoCursoOcorrenciaPeriodos = new Array < InstituicaoCursoOcorrenciaPeriodoModel > ();
    }

    coordenador: ProfessorModel;
    dataInicio: string;
    dataExpiracao: string;
    instituicaoCursoOcorrenciaPeriodos: Array < InstituicaoCursoOcorrenciaPeriodoModel > ;

}