import { I18N_LANG, I18N_TEMPLATE_MESSAGE_SERVER_CONSTANT, I18N_ERROR_GENERIC, I18N_ERROR_SERVER, I18N_TEMPLATE_MESSAGE_GENERIC_CONSTANT } from '../../constant/i18n-template-messages.contant';

class Util {

    resolveException(exception, lang: I18N_LANG) {
        exception.response.data.Code;
        let i18Nmessage = this.getTemplateMessageServer(exception.response.data.Code, lang);
        let message = i18Nmessage.message;
        for (let key in exception.response.data) {
            while (message.indexOf(`{{${key}}}`) > -1) {
                message = message.replace(`{{${key}}}`, exception.response.data[key]);
            }
        }
        return { title: i18Nmessage.title, message: message };
    }

    getTemplateMessageServer(code: I18N_ERROR_SERVER, lang: I18N_LANG) {
        return I18N_TEMPLATE_MESSAGE_SERVER_CONSTANT.find(x => x.i18nMessage === code && x.lang === lang);
    }

    getTemplateMessageGeneric(code: I18N_ERROR_GENERIC, lang: I18N_LANG) {
        return I18N_TEMPLATE_MESSAGE_GENERIC_CONSTANT.find(x => x.i18nMessage === code && x.lang === lang);
    }

}

export const I18NUtil = new Util();