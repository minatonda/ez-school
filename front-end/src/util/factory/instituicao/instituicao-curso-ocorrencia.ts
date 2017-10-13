import { Instituicao } from './instituicao';
import { Curso } from '../curso/curso';
import { CursoGrade } from '../curso/curso-grade';
import { Professor } from '../professor/professor';
import { Aluno } from '../aluno/aluno';

export class InstituicaoCursoOcorrencia {
    id?: string;
    coordenador: Professor;
    alunos: Array<Aluno>;
    dataInicio: Date;
    dataFim: Date;
}