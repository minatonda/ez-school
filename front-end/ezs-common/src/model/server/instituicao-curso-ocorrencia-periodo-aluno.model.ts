import { InstituicaoCursoTurmaModel } from './instituicao-curso-turma.model';
import { InstituicaoCursoPeriodoModel } from './instituicao-curso-periodo.model';
import { InstituicaoCursoOcorrenciaAlunoModel } from './instituicao-curso-ocorrencia-aluno.model';
import { BaseModel } from './base.model';
import { UsuarioInfoModel } from './usuario-info.model';

export class InstituicaoCursoOcorrenciaPeriodoAlunoModel extends BaseModel<number> {

    aluno: UsuarioInfoModel = null;
    instituicaoCursoTurma: InstituicaoCursoTurmaModel = null;
    instituicaoCursoPeriodo: InstituicaoCursoPeriodoModel = null;

}