import { I18N_LANG, I18N_TEMPLATE_MESSAGE_SERVER_CONSTANT, I18N_ERROR_GENERIC, I18N_ERROR_SERVER, I18N_TEMPLATE_MESSAGE_GENERIC_CONSTANT, I18N_ERROR_SERVER_FIELD, I18N_FIELD_LABELS_CONSTANT } from '../../constant/i18n-template-messages.contant';
import { BaseExceptionModel } from '../../model/server/base-exception.model';
import { BaseError } from '../../error/base.error';
class Util {

    resolveException(exception, lang: I18N_LANG) {
        let baseExceptionModel = exception.response.data as BaseExceptionModel;
        if (this.getTemplateMessageServer(baseExceptionModel.code, lang)) {

            let i18Nmessage = this.getTemplateMessageServer(baseExceptionModel.code, lang);
            let message = i18Nmessage.message;

            if (baseExceptionModel.infos && baseExceptionModel.infos.length) {
                message += '<br><ul>';
                baseExceptionModel.infos.forEach(x => {
                    if (this.getTemplateMessageServer(x.code, lang)) {
                        let subMessage = `<li>${this.getTemplateMessageServer(x.code, lang).message}</li>`;

                        if (x.field && this.getFieldLabel(x.field, lang)) {
                            while (subMessage.indexOf('{{field}}') > -1) {
                                subMessage = subMessage.replace('{{field}}', `<b>${this.getFieldLabel(x.field, lang).label}</b>`);
                            }
                        }
                        else {
                            console.error(`I18N_FIELD_LABELS_CONSTANT not found : ${x.field}`);
                            return false;
                        }

                        while (subMessage.indexOf('{{value}}') > -1) {
                            subMessage = subMessage.replace('{{value}}', `<b>${x.value}</b>`);
                        }

                        if (x.references && x.references.length > 0) {
                            let references = x.references.map(y => {
                                let fieldDef = this.getFieldLabel(y.field, lang);
                                if (fieldDef) {
                                    return `<li>${fieldDef.label} (<b>${y.value}</b>)</li>`;
                                }
                                else {
                                    console.error(`I18N_FIELD_LABELS_CONSTANT not found : ${y.field}`);
                                    return '';
                                }
                            }).join('');
                            subMessage = subMessage.replace('{{references}}', `<ul>${references}</ul>`);
                        }

                        message += subMessage;
                    }
                    else {
                        console.error(`I18N_TEMPLATE_MESSAGE_SERVER_CONSTANT not found : ${x.code}`);
                        return false;
                    }
                });
                message += '</ul>';
            }
            return { title: i18Nmessage.title, message: message };
        }
        else {
            console.error(`I18N_TEMPLATE_MESSAGE_SERVER_CONSTANT not found : ${baseExceptionModel.code}`);
        }
    }

    getTemplateMessageServer(code: I18N_ERROR_SERVER, lang: I18N_LANG) {
        return I18N_TEMPLATE_MESSAGE_SERVER_CONSTANT.find(x => x.i18nMessage === code && x.lang === lang);
    }

    getTemplateMessageGeneric(code: I18N_ERROR_GENERIC, lang: I18N_LANG) {
        return I18N_TEMPLATE_MESSAGE_GENERIC_CONSTANT.find(x => x.i18nMessage === code && x.lang === lang);
    }

    getFieldLabel(field: I18N_ERROR_SERVER_FIELD, lang: I18N_LANG) {
        return I18N_FIELD_LABELS_CONSTANT.find(x => x.i18nMessage === field && x.lang === lang);
    }

}

export const I18NUtil = new Util();