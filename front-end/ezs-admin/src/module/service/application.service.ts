import { I18N_LANG } from '../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { UsuarioInfoModel } from '../../../../ezs-common/src/model/server/usuario-info.model';

class Service {

    private language: I18N_LANG;
    private usuarioInfo: UsuarioInfoModel;

    setLanguage(language: I18N_LANG) {
        this.language = language;
    }

    getLanguage() {
        return this.language;
    }

    setUsuarioInfo(usuarioInfo: UsuarioInfoModel) {
        this.usuarioInfo = usuarioInfo;
    }

    getUsuarioInfo() {
        return this.usuarioInfo;
    }

}

export const ApplicationService = new Service();