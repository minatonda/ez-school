import { Aluno } from '../usuario/aluno';
import { InstituicaoCursoTurma } from './instituicao-curso-turma';
import { InstituicaoCursoPeriodo } from './instituicao-curso-periodo';

export class InstituicaoCursoOcorrenciaAluno {
    id ?: string;
    aluno: Aluno;
    turma: InstituicaoCursoTurma;
    periodo: InstituicaoCursoPeriodo;
}