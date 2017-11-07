import { Instituicao } from './instituicao';
import { Curso } from '../curso/curso';
import { CursoGrade } from '../curso/curso-grade';
import { Professor } from '../usuario/professor';
import { Aluno } from '../usuario/aluno';
import { InstituicaoCursoOcorrenciaAluno } from './instituicao-curso-ocorrencia-aluno';
import { InstituicaoCursoOcorrenciaProfessor } from './instituicao-curso-ocorrencia-professor';

export class InstituicaoCursoOcorrencia {
    
    constructor() {
        this.alunos = new Array<InstituicaoCursoOcorrenciaAluno>();
        this.professores = new Array<InstituicaoCursoOcorrenciaProfessor>();
    }

    id?: string;
    coordenador: Professor;
    alunos: Array<InstituicaoCursoOcorrenciaAluno>;
    professores: Array<InstituicaoCursoOcorrenciaProfessor>;
    dataInicio: Date;
    dataFim: Date;
}