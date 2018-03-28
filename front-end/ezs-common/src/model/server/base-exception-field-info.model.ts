import { I18N_ERROR_SERVER_FIELD, I18N_ERROR_SERVER } from "../../constant/i18n-template-messages.contant";

export class BaseExceptionFieldInfoModel {
    value: string;
    code: I18N_ERROR_SERVER;
    field: I18N_ERROR_SERVER_FIELD;
    references :Array<BaseExceptionFieldInfoModel>;
}