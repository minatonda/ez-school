export class InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaModel {
    id?: string;
    inicio: string;
    fim: string;
    dia: DayOfWeekModel;
    label: string;
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