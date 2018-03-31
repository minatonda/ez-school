import { BaseExceptionFieldInfoModel } from "./base-exception-field-info.model";
import { I18N_ERROR_SERVER } from "../../constant/i18n-template-messages.contant";

export class BaseExceptionModel {
    code: I18N_ERROR_SERVER = null;
    infos: Array<BaseExceptionFieldInfoModel> = null;
}