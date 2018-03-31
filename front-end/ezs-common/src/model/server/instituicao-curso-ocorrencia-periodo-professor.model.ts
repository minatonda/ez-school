import { InstituicaoCursoTurmaModel } from './instituicao-curso-turma.model';
import { InstituicaoCursoPeriodoModel } from './instituicao-curso-periodo.model';
import { InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel } from './instituicao-curso-ocorrencia-periodo-professor-periodo-aula.model';
import { CursoGradeMateriaModel } from './curso-grade-materia.model';
import { BaseModel } from './base.model';
import { UsuarioInfoModel } from './usuario-info.model';

export class InstituicaoCursoOcorrenciaPeriodoProfessorModel extends BaseModel < number > {

    constructor() {
        super();
        this.instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas = new Array < InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel > ();
    }

    professor: UsuarioInfoModel = null;
    cursoGradeMateria: CursoGradeMateriaModel = null;
    instituicaoCursoTurma: InstituicaoCursoTurmaModel = null;
    instituicaoCursoPeriodo: InstituicaoCursoPeriodoModel = null;
    instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas: Array < InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel >  = null;

}