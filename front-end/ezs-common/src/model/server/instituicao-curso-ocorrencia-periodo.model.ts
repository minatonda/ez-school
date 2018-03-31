import { InstituicaoModel } from './instituicao.model';
import { InstituicaoCursoOcorrenciaPeriodoAlunoModel } from './instituicao-curso-ocorrencia-periodo-aluno.model';
import { InstituicaoCursoOcorrenciaPeriodoProfessorModel } from './instituicao-curso-ocorrencia-periodo-professor.model';
import { BaseModel } from './base.model';

export class InstituicaoCursoOcorrenciaPeriodoModel extends BaseModel < number > {

    constructor() {
        super();
        this.instituicaoCursoOcorrenciaPeriodoAlunos = new Array < InstituicaoCursoOcorrenciaPeriodoAlunoModel > ();
        this.instituicaoCursoOcorrenciaPeriodoProfessores = new Array < InstituicaoCursoOcorrenciaPeriodoProfessorModel > ();
    }

    instituicaoCursoOcorrenciaPeriodoProfessores: Array < InstituicaoCursoOcorrenciaPeriodoProfessorModel >  = null;
    instituicaoCursoOcorrenciaPeriodoAlunos: Array < InstituicaoCursoOcorrenciaPeriodoAlunoModel >  = null;
    dataInicio: Date = null;
    dataExpiracao: Date = null;

}