import { I18N_LANG, I18N_ENUM_LABELS_CONSTANTS } from '../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { UsuarioInfoModel } from '../../../../ezs-common/src/model/server/usuario-info.model';
import { ENUM_CONTANT } from '../../../../ezs-common/src/constant/enum.contant';
import { InstituicaoModel } from '../../../../ezs-common/src/model/server/instituicao.model';
import { FACTORY_CONSTANT } from '../constant/factory.constant';
import { InstituicaoBusinessAulaModel } from '../../../../ezs-common/src/model/server/instituicao-business-aula.model';
import { InstituicaoBusinessAulaDetalheAlunoModel } from '../../../../ezs-common/src/model/server/instituicao-business-aula-detalhe-aluno.model';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../app.broadcast-event-bus';
import { Vue, Component } from 'vue-property-decorator';

export enum ApplicationMode {
    PROFESSOR,
    ALUNO
}

class Service {

    private language: I18N_LANG = I18N_LANG.ptBR;
    private applicationMode = ApplicationMode.ALUNO;
    private usuarioInfo: UsuarioInfoModel;
    private instituicaoBusinessAulasByProfessor: Array<InstituicaoBusinessAulaModel>;
    private instituicaoBusinessAulasByAluno: Array<InstituicaoBusinessAulaDetalheAlunoModel>;
    private views: Array<string>;
    private admin: boolean;
    private loading: boolean = false;

    setLanguage(language: I18N_LANG) {
        this.language = language;
    }

    getLanguage() {
        return this.language;
    }

    setApplicationMode(mode: ApplicationMode) {
        switch (mode) {
            case (ApplicationMode.ALUNO): {
                AppBroadcastEventBus.$emit(AppBroadcastEvent.ATIVAR_MODO_ALUNO);
            }
            case (ApplicationMode.PROFESSOR): {
                AppBroadcastEventBus.$emit(AppBroadcastEvent.ATIVAR_MODO_PROFESSOR);
            }
            default: {
                this.applicationMode = mode;
            }
        }

    }

    isApplicationMode(mode: ApplicationMode) {
        return this.applicationMode === mode;
    }

    setUsuarioInfo(usuarioInfo: UsuarioInfoModel) {
        this.usuarioInfo = usuarioInfo;
    }

    getUsuarioInfo() {
        return this.usuarioInfo;
    }

    setViews(views: Array<string>) {
        this.views = views;
    }

    getViews() {
        return this.views;
    }

    getEnumLabels(enumerable: ENUM_CONTANT) {
        return I18N_ENUM_LABELS_CONSTANTS.filter(x => x.enumerable === enumerable && x.lang === this.language);
    }

    setLoading(loading: boolean) {
        this.loading = !!loading;
    }

    isLoading() {
        return this.loading;
    }

    setInstituicaoBusinessAulasByProfessor(instituicaoBusinessAulasByProfessor: Array<InstituicaoBusinessAulaModel>) {
        this.instituicaoBusinessAulasByProfessor = instituicaoBusinessAulasByProfessor;
    }

    getInstituicaoBusinessAulasByProfessor() {
        return this.instituicaoBusinessAulasByProfessor;
    }

    setInstituicaoBusinessAulasByAluno(instituicaoBusinessAulasByAluno: Array<InstituicaoBusinessAulaDetalheAlunoModel>) {
        this.instituicaoBusinessAulasByAluno = instituicaoBusinessAulasByAluno;
    }

    getInstituicaoBusinessAulasByAluno() {
        return this.instituicaoBusinessAulasByAluno;
    }

    isReady() {
        return !!this.instituicaoBusinessAulasByAluno && !!this.instituicaoBusinessAulasByProfessor && !!this.usuarioInfo && !!this.views && this.admin !== undefined;
    }

    isAdmin() {
        return this.admin;
    }

    isAluno() {
        return this.instituicaoBusinessAulasByAluno && this.instituicaoBusinessAulasByAluno.length > 0;
    }

    isProfessor() {
        return this.instituicaoBusinessAulasByProfessor && this.instituicaoBusinessAulasByProfessor.length > 0;
    }

    configureDefaults = async () => {
        this.setLoading(true);
        this.usuarioInfo = await FACTORY_CONSTANT.UsuarioFactory.me();
        this.views = await FACTORY_CONSTANT.UsuarioFactory.meAuthorizedView();
        this.instituicaoBusinessAulasByProfessor = await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoBusinessAulaByProfessor(this.usuarioInfo.id);
        this.instituicaoBusinessAulasByAluno = await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoBusinessAulaByAluno(this.usuarioInfo.id, false);
        this.admin = await FACTORY_CONSTANT.UsuarioFactory.meAdmin();
        this.applicationMode = this.instituicaoBusinessAulasByProfessor.length ? ApplicationMode.PROFESSOR : ApplicationMode.ALUNO;
        this.setLoading(false);
    }

    resetDefaults() {
        this.usuarioInfo = undefined;
        this.views = undefined;
        this.instituicaoBusinessAulasByProfessor = undefined;
        this.instituicaoBusinessAulasByAluno = undefined;
        this.admin = undefined;
    }

}

export const ApplicationService = new Service();