import { Instituicao } from './instituicao';
import { Curso } from '../curso/curso';
import { CursoGrade } from '../curso/curso-grade';
import { Professor } from '../usuario/professor';
import { Aluno } from '../usuario/aluno';
import { InstituicaoCursoOcorrenciaAluno } from './instituicao-curso-ocorrencia-aluno';
import { InstituicaoCursoOcorrenciaProfessor } from './instituicao-curso-ocorrencia-professor';

export class InstituicaoCursoOcorrencia {
    
    constructor() {
        this.instituicaoCursoOcorrenciaAlunos = new Array<InstituicaoCursoOcorrenciaAluno>();
    }

    id?: string;
    coordenador: Professor;
    instituicaoCursoOcorrenciaAlunos: Array<InstituicaoCursoOcorrenciaAluno>;
    dataInicio: Date;
    dataFim: Date;
}