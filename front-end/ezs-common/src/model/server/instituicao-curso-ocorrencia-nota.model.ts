import { BaseModel } from './base.model';
import { InstituicaoCursoOcorrenciaPeriodoAlunoModel } from './instituicao-curso-ocorrencia-periodo-aluno.model';

export class InstituicaoCursoOcorrenciaNotaModel extends BaseModel < number > {
    idTag: string = null;
    valor: number = null;
    dataLancamento: string = null;
    instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel = null;
}