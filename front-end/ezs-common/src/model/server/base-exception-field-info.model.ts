import { I18N_ERROR_SERVER_FIELD, I18N_ERROR_SERVER } from '../../constant/i18n-template-messages.contant';

export class BaseExceptionFieldInfoModel {
    value: string = null;
    valueType: BaseExceptionFieldInfoValueTypeModel = null;
    code: I18N_ERROR_SERVER = null;
    field: I18N_ERROR_SERVER_FIELD = null;
    references: Array<BaseExceptionFieldInfoModel> = null;
}

export enum BaseExceptionFieldInfoValueTypeModel {
    TEXT = 'TEXT',
    CURRENCY = 'CURRENCY',
    DATE = 'DATE',
    TIME = 'TIME',
    DATETIME = 'DATETIME'
}