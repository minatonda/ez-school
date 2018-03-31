import { DayOfWeekModel } from './instituicao-curso-ocorrencia-periodo-professor-periodo-aula.model';
import { BaseModel } from './base.model';

export class InstituicaoCursoPeriodoModel extends BaseModel < number > {

    constructor() {
        super();
        this.diaSemana = new Array < DayOfWeekModel > ();
    }

    inicio: string = null;
    fim: string = null;
    pausaInicio: string = null;
    pausaFim: string = null;
    quebras: number = null;
    diaSemana: Array < DayOfWeekModel >  = null;

}