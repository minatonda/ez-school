import { InstituicaoCursoTurmaModel } from './instituicao-curso-turma.model';
import { InstituicaoCursoPeriodoModel } from './instituicao-curso-periodo.model';
import { AlunoModel } from './aluno.model';
import { BaseModel } from './base.model';
import { UsuarioModel } from './usuario.model';

export class InstituicaoCursoOcorrenciaAlunoModel extends BaseModel < number > {

    aluno: UsuarioModel = null;

}