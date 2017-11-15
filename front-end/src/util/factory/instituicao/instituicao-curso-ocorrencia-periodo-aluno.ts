import { Aluno } from '../usuario/aluno';
import { InstituicaoCursoTurma } from './instituicao-curso-turma';
import { InstituicaoCursoPeriodo } from './instituicao-curso-periodo';
import { InstituicaoCursoOcorrenciaAluno } from './instituicao-curso-ocorrencia-aluno';

export class InstituicaoCursoOcorrenciaPeriodoAluno {
    id ? : string;
    instituicaoCursoOcorrenciaAlunoVM: InstituicaoCursoOcorrenciaAluno;
    instituicaoCursoTurmaVM: InstituicaoCursoTurma;
    instituicaoCursoPeriodoVM: InstituicaoCursoPeriodo;
}