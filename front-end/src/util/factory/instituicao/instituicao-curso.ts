import { Instituicao } from './instituicao';
import { Curso } from '../curso/curso';
import { CursoGrade } from '../curso/curso-grade';
import { InstituicaoCursoPeriodo } from './instituicao-curso-periodo';

export class InstituicaoCurso {

    constructor() {
        this.periodos = new Array<InstituicaoCursoPeriodo>();
    }

    id?: string;
    curso: Curso;
    cursoGrade: CursoGrade;
    periodos: Array<InstituicaoCursoPeriodo>;
    dataInicio: Date;
    dataFim: Date;
}