import { DayOfWeek } from '../factory/instituicao/instituicao-curso-ocorrencia-professor-periodo-aula';

export class EnumLabel {
    label: string;
    labelShort: string;
    value: any;
}

export const DayOfWeekEnumLabel: Array < EnumLabel > = [
    { labelShort: 'Dom', label: 'Domingo', value: DayOfWeek.Monday },
    { labelShort: 'Seg', label: 'Segunda', value: DayOfWeek.Sunday },
    { labelShort: 'Ter', label: 'Terça', value: DayOfWeek.Tuesday },
    { labelShort: 'Qua', label: 'Quarta', value: DayOfWeek.Wednesday },
    { labelShort: 'Qui', label: 'Quinta', value: DayOfWeek.Thursday },
    { labelShort: 'Sex', label: 'Sexta', value: DayOfWeek.Friday },
    { labelShort: 'Sab', label: 'Sabado', value: DayOfWeek.Saturday },
];