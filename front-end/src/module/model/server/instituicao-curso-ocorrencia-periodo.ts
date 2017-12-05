import { Instituicao } from './instituicao';
import { InstituicaoCursoOcorrenciaPeriodoAluno } from './instituicao-curso-ocorrencia-periodo-aluno';
import { InstituicaoCursoOcorrenciaPeriodoProfessor } from './instituicao-curso-ocorrencia-periodo-professor';

export class InstituicaoCursoOcorrenciaPeriodo {

    constructor() {
        this.instituicaoCursoOcorrenciaPeriodoAlunos = new Array < InstituicaoCursoOcorrenciaPeriodoAluno > ();
        this.instituicaoCursoOcorrenciaPeriodoProfessores = new Array < InstituicaoCursoOcorrenciaPeriodoProfessor > ();
    }

    id?: string;
    instituicaoCursoOcorrenciaPeriodoProfessores: Array < InstituicaoCursoOcorrenciaPeriodoProfessor > ;
    instituicaoCursoOcorrenciaPeriodoAlunos: Array < InstituicaoCursoOcorrenciaPeriodoAluno > ;
    dataInicio: Date;
    dataExpiracao: Date;
}