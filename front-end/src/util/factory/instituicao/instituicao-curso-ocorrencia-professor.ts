import { Professor } from '../usuario/professor';
import { InstituicaoCursoTurma } from './instituicao-curso-turma';
import { InstituicaoCursoPeriodo } from './instituicao-curso-periodo';
import { CursoGradeMateria } from '../curso/curso-grade-materia';

export class InstituicaoCursoOcorrenciaProfessor {
    id ?: string;
    professor: Professor;
    cursoGradeMateria: CursoGradeMateria;
    turma: InstituicaoCursoTurma;
    periodo: InstituicaoCursoPeriodo;
}