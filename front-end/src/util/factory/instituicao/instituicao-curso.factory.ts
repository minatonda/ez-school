import { Instituicao } from "./instituicao";
import { Curso } from "../curso/curso";
import { CursoGrade } from "../curso/curso-grade";

export class InstituicaoCurso {
    id: number;
    instituicao: Instituicao;
    curso: Curso;
    cursoGrade: CursoGrade;
    dataInicio: Date;
    dataFim: Date;
}