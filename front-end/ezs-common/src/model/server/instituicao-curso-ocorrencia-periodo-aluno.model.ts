import { InstituicaoCursoTurmaModel } from './instituicao-curso-turma.model';
import { InstituicaoCursoPeriodoModel } from './instituicao-curso-periodo.model';
import { InstituicaoCursoOcorrenciaAlunoModel } from './instituicao-curso-ocorrencia-aluno.model';
import { AlunoModel } from './aluno.model';

export class InstituicaoCursoOcorrenciaPeriodoAlunoModel {
    id?: string;
    aluno: AlunoModel;
    instituicaoCursoTurma: InstituicaoCursoTurmaModel;
    instituicaoCursoPeriodo: InstituicaoCursoPeriodoModel;
}