import toastr from 'toastr';
import { I18N_MESSAGE, I18N_LANG, I18N_TEMPLATE_MESSAGE_CONSTANT, } from '../../constant/i18n-template-messages.contant';
import { BaseError } from '../../error/base.error';

export enum NOTIFY_TYPE {
    SUCCESS = 'SUCCESS',
    ERROR = 'ERROR',
    INFO = 'INFO',
    WARNING = 'WARNING'
}

class Util {

    public notifyI18N = (i18nMessage: I18N_MESSAGE, lang: I18N_LANG, type: NOTIFY_TYPE) => {
        let i18nMessageTemplate = I18N_TEMPLATE_MESSAGE_CONSTANT.find(x => x.lang === lang && x.i18nMessage === i18nMessage);
        this.notify(i18nMessageTemplate.message, i18nMessageTemplate.title, type);
    }

    public notifyI18NError = (i18nMessage: I18N_MESSAGE, lang: I18N_LANG, type: NOTIFY_TYPE, error: BaseError) => {
        let i18nMessageTemplate = I18N_TEMPLATE_MESSAGE_CONSTANT.find(x => x.lang === lang && x.i18nMessage === i18nMessage);
        this.notify(i18nMessageTemplate.message, i18nMessageTemplate.title, type);
    }

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

    public notify = (message: string, title: string, type: NOTIFY_TYPE) => {
        switch (type) {
            case (NOTIFY_TYPE.SUCCESS): {
                this.success(message, title);
                break;
            }
            case (NOTIFY_TYPE.ERROR): {
                this.error(message, title);
                break;
            }
            case (NOTIFY_TYPE.INFO): {
                this.info(message, title);
                break;
            }
            case (NOTIFY_TYPE.WARNING): {
                this.warning(message, title);
                break;
            }
        }
    }

}

export const NotifyUtil = new Util();