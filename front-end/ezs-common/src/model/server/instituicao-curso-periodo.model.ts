import { DayOfWeekModel } from './instituicao-curso-ocorrencia-periodo-professor-periodo-aula.model';
import { BaseModel } from './base.model';

export class InstituicaoCursoPeriodoModel extends BaseModel < number > {

    constructor() {
        super();
        this.diaSemana = new Array < DayOfWeekModel > ();
    }

    inicio: string;
    fim: string;
    pausaInicio: string;
    pausaFim: string;
    quebras: number;
    diaSemana: Array < DayOfWeekModel > ;

}