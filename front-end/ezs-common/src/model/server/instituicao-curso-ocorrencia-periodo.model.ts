import { InstituicaoModel } from './instituicao.model';
import { InstituicaoCursoOcorrenciaPeriodoAlunoModel } from './instituicao-curso-ocorrencia-periodo-aluno.model';
import { InstituicaoCursoOcorrenciaPeriodoProfessorModel } from './instituicao-curso-ocorrencia-periodo-professor.model';

export class InstituicaoCursoOcorrenciaPeriodoModel {

    constructor() {
        this.instituicaoCursoOcorrenciaPeriodoAlunos = new Array < InstituicaoCursoOcorrenciaPeriodoAlunoModel > ();
        this.instituicaoCursoOcorrenciaPeriodoProfessores = new Array < InstituicaoCursoOcorrenciaPeriodoProfessorModel > ();
    }

    id?: string;
    instituicaoCursoOcorrenciaPeriodoProfessores: Array < InstituicaoCursoOcorrenciaPeriodoProfessorModel > ;
    instituicaoCursoOcorrenciaPeriodoAlunos: Array < InstituicaoCursoOcorrenciaPeriodoAlunoModel > ;
    dataInicio: Date;
    dataExpiracao: Date;
}