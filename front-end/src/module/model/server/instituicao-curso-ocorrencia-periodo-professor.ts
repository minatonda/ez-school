import { InstituicaoCursoTurma } from './instituicao-curso-turma';
import { InstituicaoCursoPeriodo } from './instituicao-curso-periodo';
import { InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula } from './instituicao-curso-ocorrencia-periodo-professor-periodo-aula';
import { Professor } from './professor';
import { CursoGradeMateria } from './curso-grade-materia';

export class InstituicaoCursoOcorrenciaPeriodoProfessor {
    constructor() {
        this.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas = new Array < InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula > ();
    }

    id?: string;
    professor: Professor;
    cursoGradeMateria: CursoGradeMateria;
    instituicaoCursoTurma: InstituicaoCursoTurma;
    instituicaoCursoPeriodo: InstituicaoCursoPeriodo;
    instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas: Array < InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula > ;
}