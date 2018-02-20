import { I18N_LANG } from '../../../../ezs-common/src/constant/i18n-template-messages.contant';

class Service {
    private language: I18N_LANG;

    setLanguage(language: I18N_LANG) {
        this.language = language;
    }

    getLanguage() {
        return this.language;
    }

}

export const ApplicationService = new Service();