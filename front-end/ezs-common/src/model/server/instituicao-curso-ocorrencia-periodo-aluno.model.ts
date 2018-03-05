import { InstituicaoCursoTurmaModel } from './instituicao-curso-turma.model';
import { InstituicaoCursoPeriodoModel } from './instituicao-curso-periodo.model';
import { InstituicaoCursoOcorrenciaAlunoModel } from './instituicao-curso-ocorrencia-aluno.model';
import { AlunoModel } from './aluno.model';
import { BaseModel } from './base.model';

export class InstituicaoCursoOcorrenciaPeriodoAlunoModel extends BaseModel < number > {

    aluno: AlunoModel;
    instituicaoCursoTurma: InstituicaoCursoTurmaModel;
    instituicaoCursoPeriodo: InstituicaoCursoPeriodoModel;

}