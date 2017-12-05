import { Instituicao } from './instituicao';
import { InstituicaoCursoPeriodo } from './instituicao-curso-periodo';
import { InstituicaoCursoTurma } from './instituicao-curso-turma';
import { Curso } from './curso';
import { CursoGrade } from './curso-grade';

export class InstituicaoCurso {

    constructor() {
        this.periodos = new Array < InstituicaoCursoPeriodo > ();
        this.turmas = new Array < InstituicaoCursoTurma > ();
    }

    id?: string;
    curso: Curso;
    cursoGrade: CursoGrade;
    periodos: Array < InstituicaoCursoPeriodo > ;
    turmas: Array < InstituicaoCursoTurma > ;
    dataInicio: Date;
    dataFim: Date;
}