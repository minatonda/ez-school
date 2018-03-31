import { I18N_LANG, I18N_TEMPLATE_MESSAGE_SERVER_CONSTANT, I18N_ERROR_GENERIC, I18N_ERROR_SERVER, I18N_TEMPLATE_MESSAGE_GENERIC_CONSTANT, I18N_ERROR_SERVER_FIELD, I18N_FIELD_LABELS_CONSTANT, I18N_LABEL_REFERENCES_PREPEND, I18N_REFERENCES_PREPEND } from '../../constant/i18n-template-messages.contant';
import { BaseExceptionModel } from '../../model/server/base-exception.model';
import { BaseError } from '../../error/base.error';
import { BaseExceptionFieldInfoModel, BaseExceptionFieldInfoValueTypeModel } from '../../model/server/base-exception-field-info.model';
import * as moment from 'moment';

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

                        subMessage = subMessage.replace('{{value}}', `<b>${this.generateValue(x, lang)}</b>`);


                        if (x.references && x.references.length > 0) {
                            let referencesWithLabel = x.references.filter(x => !!x.field);
                            let references = x.references.filter(x => !x.field);

                            let referenceReplace = '';

                            if (references.length) {
                                let text = x.references.map(y => {
                                    return y.value;
                                }).join(', ');
                                referenceReplace += this.getReferencePrepend(lang).message + text;
                            }

                            if (references.length && referencesWithLabel.length) {
                                referenceReplace += ' e ';
                            }

                            if (referencesWithLabel.length) {
                                let text = x.references.map(y => {
                                    let fieldDef = this.getFieldLabel(y.field, lang);
                                    if (fieldDef) {
                                        return `<li>${fieldDef.label} (<b>${this.generateValue(y, lang)}</b>)</li>`;
                                    }
                                    else {
                                        console.error(`I18N_FIELD_LABELS_CONSTANT not found : ${y.field}`);
                                        return '';
                                    }
                                }).join('');

                                referenceReplace += `${this.getReferenceLabelPrepend(lang).message}<ul>${text}</ul>`;
                            }

                            subMessage = subMessage.replace('{{references}}', `${referenceReplace}`);
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

    getReferenceLabelPrepend(lang: I18N_LANG) {
        return I18N_LABEL_REFERENCES_PREPEND.find(x => x.lang === lang);
    }

    getReferencePrepend(lang: I18N_LANG) {
        return I18N_REFERENCES_PREPEND.find(x => x.lang === lang);
    }

    generateValue(baseExceptionFieldInfoModel: BaseExceptionFieldInfoModel, lang: I18N_LANG) {
        switch (baseExceptionFieldInfoModel.valueType) {
            case (BaseExceptionFieldInfoValueTypeModel.CURRENCY): {
                return baseExceptionFieldInfoModel.value;
            }
            case (BaseExceptionFieldInfoValueTypeModel.DATE): {
                return moment.utc(baseExceptionFieldInfoModel.value).format('DD/MM/YYYY');
            }
            case (BaseExceptionFieldInfoValueTypeModel.TIME): {
                return moment.utc(baseExceptionFieldInfoModel.value).format('HH:MM:SS');
            }
            case (BaseExceptionFieldInfoValueTypeModel.DATETIME): {
                return moment.utc(baseExceptionFieldInfoModel.value).format('DD/MM/YYYY HH:MM:SS');
            }
            default: {
                return baseExceptionFieldInfoModel.value;
            }
        }
    }

}

export const I18NUtil = new Util();