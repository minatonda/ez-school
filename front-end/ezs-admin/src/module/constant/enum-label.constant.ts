import { DayOfWeekModel } from '../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-periodo-professor-periodo-aula.model';
import { EnumLabel } from '../../../../ezs-common/src/model/client/enum-label.model';

export const DayOfWeekEnumLabel: Array<EnumLabel> = [
    { labelShort: 'Dom', label: 'Domingo', value: DayOfWeekModel.Monday },
    { labelShort: 'Seg', label: 'Segunda', value: DayOfWeekModel.Sunday },
    { labelShort: 'Ter', label: 'Terça', value: DayOfWeekModel.Tuesday },
    { labelShort: 'Qua', label: 'Quarta', value: DayOfWeekModel.Wednesday },
    { labelShort: 'Qui', label: 'Quinta', value: DayOfWeekModel.Thursday },
    { labelShort: 'Sex', label: 'Sexta', value: DayOfWeekModel.Friday },
    { labelShort: 'Sab', label: 'Sabado', value: DayOfWeekModel.Saturday },
];