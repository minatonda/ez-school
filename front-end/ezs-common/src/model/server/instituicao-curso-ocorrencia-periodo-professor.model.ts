import { InstituicaoCursoTurmaModel } from './instituicao-curso-turma.model';
import { InstituicaoCursoPeriodoModel } from './instituicao-curso-periodo.model';
import { InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel } from './instituicao-curso-ocorrencia-periodo-professor-periodo-aula.model';
import { ProfessorModel } from './professor.model';
import { CursoGradeMateriaModel } from './curso-grade-materia.model';

export class InstituicaoCursoOcorrenciaPeriodoProfessorModel {
    constructor() {
        this.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas = new Array < InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel > ();
    }

    id?: string;
    professor: ProfessorModel;
    cursoGradeMateria: CursoGradeMateriaModel;
    instituicaoCursoTurma: InstituicaoCursoTurmaModel;
    instituicaoCursoPeriodo: InstituicaoCursoPeriodoModel;
    instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas: Array < InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel > ;
}