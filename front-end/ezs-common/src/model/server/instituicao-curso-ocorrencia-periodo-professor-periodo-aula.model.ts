import { BaseModel } from './base.model';

export class InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel extends BaseModel < number > {

    inicio: string = null;
    fim: string = null;
    dia: DayOfWeekModel = null;
    label: string = null;

}

export enum DayOfWeekModel {
    Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
}