import { InstituicaoCursoTurmaModel } from './instituicao-curso-turma.model';
import { InstituicaoCursoPeriodoModel } from './instituicao-curso-periodo.model';
import { AlunoModel } from './aluno.model';

export class InstituicaoCursoOcorrenciaAlunoModel {
    id ?: string;
    aluno: AlunoModel;
}