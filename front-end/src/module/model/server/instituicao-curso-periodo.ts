import { DayOfWeek } from './instituicao-curso-ocorrencia-periodo-professor-periodo-aula';

export class InstituicaoCursoPeriodo {

    constructor() {
        this.diaSemana = new Array < DayOfWeek > ();
    }

    id?: string;
    inicio: string;
    fim: string;
    pausaInicio: string;
    pausaFim: string;
    quebras: number;
    diaSemana: Array < DayOfWeek > ;
}