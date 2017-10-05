import { Materia } from '../materia/materia';
import { CursoGradeMateria } from './curso-grade-materia';

export class CursoGrade {
    constructor() {
        this.materias = new Array<CursoGradeMateria>();
    }
    id: number;
    descricao: string;
    materias: Array<CursoGradeMateria>;
}