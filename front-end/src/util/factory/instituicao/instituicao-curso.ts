import { Instituicao } from './instituicao';
import { Curso } from '../curso/curso';
import { CursoGrade } from '../curso/curso-grade';

export class InstituicaoCurso {
    id: number;
    curso: Curso;
    cursoGrade: CursoGrade;
    dataInicio: Date;
    dataFim: Date;
}