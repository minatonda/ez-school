import { BaseModel } from './base.model';
import { InstituicaoCursoOcorrenciaPeriodoAlunoModel } from './instituicao-curso-ocorrencia-periodo-aluno.model';

export class InstituicaoCursoOcorrenciaAusenciaModel extends BaseModel < number > {
    dataAusencia: string = null;
    instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel = null;
}