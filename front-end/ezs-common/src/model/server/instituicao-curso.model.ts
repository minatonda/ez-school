import { InstituicaoModel } from './instituicao.model';
import { InstituicaoCursoPeriodoModel } from './instituicao-curso-periodo.model';
import { InstituicaoCursoTurmaModel } from './instituicao-curso-turma.model';
import { CursoModel } from './curso.model';
import { CursoGradeModel } from './curso-grade.model';

export class InstituicaoCursoModel {

    constructor() {
        this.periodos = new Array < InstituicaoCursoPeriodoModel > ();
        this.turmas = new Array < InstituicaoCursoTurmaModel > ();
    }

    id?: string;
    curso: CursoModel;
    cursoGrade: CursoGradeModel;
    periodos: Array < InstituicaoCursoPeriodoModel > ;
    turmas: Array < InstituicaoCursoTurmaModel > ;
    dataInicio: Date;
    dataFim: Date;
}