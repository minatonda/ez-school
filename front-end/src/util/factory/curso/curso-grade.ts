import { Materia } from '../materia/materia';

export class CursoGrade {
    constructor() {
        this.materias = new Array<Materia>();
    }
    id: number;
    materias: Array<Materia>;
}