import { DayOfWeekModel } from './instituicao-curso-ocorrencia-periodo-professor-periodo-aula.model';

export class InstituicaoCursoPeriodoModel {

    constructor() {
        this.diaSemana = new Array < DayOfWeekModel > ();
    }

    id?: string;
    inicio: string;
    fim: string;
    pausaInicio: string;
    pausaFim: string;
    quebras: number;
    diaSemana: Array < DayOfWeekModel > ;
}