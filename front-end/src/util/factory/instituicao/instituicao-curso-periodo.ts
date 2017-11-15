import { DayOfWeek } from './instituicao-curso-ocorrencia-professor-periodo-aula';

export class InstituicaoCursoPeriodo {

    constructor() {
        this.diaSemana = new Array < DayOfWeek > ();
    }

    id ?: string;
    inicio: string;
    fim: string;
    diaSemana: Array < DayOfWeek > ;
}