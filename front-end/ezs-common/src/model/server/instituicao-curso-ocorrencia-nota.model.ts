import { BaseModel } from './base.model';
import { InstituicaoCursoOcorrenciaPeriodoAlunoModel } from './instituicao-curso-ocorrencia-periodo-aluno.model';

export class InstituicaoCursoOcorrenciaNotaModel extends BaseModel < number > {
    idTag: string;
    valor: number;
    dataLancamento: string;
    instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel;
}