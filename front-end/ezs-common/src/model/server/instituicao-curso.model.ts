import { InstituicaoModel } from './instituicao.model';
import { InstituicaoCursoPeriodoModel } from './instituicao-curso-periodo.model';
import { InstituicaoCursoTurmaModel } from './instituicao-curso-turma.model';
import { CursoModel } from './curso.model';
import { CursoGradeModel } from './curso-grade.model';
import { BaseModel } from './base.model';

export class InstituicaoCursoModel extends BaseModel < number > {

    constructor() {
        super();
        this.periodos = new Array < InstituicaoCursoPeriodoModel > ();
        this.turmas = new Array < InstituicaoCursoTurmaModel > ();
    }

    curso: CursoModel = null;
    cursoGrade: CursoGradeModel = null;
    periodos: Array < InstituicaoCursoPeriodoModel >  = null;
    turmas: Array < InstituicaoCursoTurmaModel >  = null;
    dataInicio: Date = null;
    dataFim: Date = null;

}