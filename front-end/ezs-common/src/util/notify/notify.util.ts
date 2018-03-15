import toastr from 'toastr';
import { I18NUtil } from '../i18n/i18n.util';
import { I18N_ERROR_GENERIC, I18N_LANG, I18N_TEMPLATE_MESSAGE_GENERIC_CONSTANT, } from '../../constant/i18n-template-messages.contant';
import { BaseError } from '../../error/base.error';

export enum NOTIFY_TYPE {
    SUCCESS = 'SUCCESS',
        ERROR = 'ERROR',
        INFO = 'INFO',
        WARNING = 'WARNING'
}

class Util {

    public error = (message: string, title: string) => {
        toastr.error(message, title, { closeButton: true, timeOut: 0, extendedTimeOut: 0 });
    }

    public success = (message: string, title: string) => {
        toastr.success(message, title);
    }

    public info = (message: string, title: string) => {
        toastr.info(message, title);
    }

    public warning = (message: string, title: string) => {
        toastr.warning(message, title);
    }

    public errorG = (code: I18N_ERROR_GENERIC, lang: I18N_LANG) => {
        let template = I18NUtil.getTemplateMessageGeneric(code, lang);
        toastr.error(template.message, template.title, { closeButton: true, timeOut: 0, extendedTimeOut: 0 });
    }

    public successG = (code: I18N_ERROR_GENERIC, lang: I18N_LANG) => {
        let template = I18NUtil.getTemplateMessageGeneric(code, lang);
        toastr.success(template.message, template.title);
    }

    public infoG = (code: I18N_ERROR_GENERIC, lang: I18N_LANG) => {
        let template = I18NUtil.getTemplateMessageGeneric(code, lang);
        toastr.info(template.message, template.title);
    }

    public warningG = (code: I18N_ERROR_GENERIC, lang: I18N_LANG) => {
        let template = I18NUtil.getTemplateMessageGeneric(code, lang);
        toastr.warning(template.message, template.title);
    }

    public exception = (exception, lang: I18N_LANG) => {
        let template = I18NUtil.resolveException(exception, lang);
        toastr.error(template.message, template.title, { closeButton: true, timeOut: 0, extendedTimeOut: 0 });
    }

}

export const NotifyUtil = new Util();