import { Instituicao } from './instituicao';
import { Curso } from '../curso/curso';
import { CursoGrade } from '../curso/curso-grade';

export class InstituicaoCursoOcorrencia {
    id?: string;
    //coordenador: Professor;
    //alunos: Array<Aluno>;
    dataInicio: Date;
    dataFim: Date;
}