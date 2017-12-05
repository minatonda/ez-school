import { InstituicaoCursoTurma } from './instituicao-curso-turma';
import { InstituicaoCursoPeriodo } from './instituicao-curso-periodo';
import { InstituicaoCursoOcorrenciaAluno } from './instituicao-curso-ocorrencia-aluno';
import { Aluno } from './aluno';

export class InstituicaoCursoOcorrenciaPeriodoAluno {
    id?: string;
    aluno: Aluno;
    instituicaoCursoTurma: InstituicaoCursoTurma;
    instituicaoCursoPeriodo: InstituicaoCursoPeriodo;
}