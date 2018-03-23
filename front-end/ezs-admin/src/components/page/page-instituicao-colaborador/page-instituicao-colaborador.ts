import { Vue, Component, Prop } from 'vue-property-decorator';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { RouterPathType } from '../../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { ApplicationService } from '../../../module/service/application.service';
import { InstituicaoColaboradorModel } from '../../../../../ezs-common/src/model/server/instituicao-colaborador.model';
import { UsuarioInfoModel } from '../../../../../ezs-common/src/model/server/usuario-info.model';
import { InstituicaoColaboradorPerfilModel } from '../../../../../ezs-common/src/model/server/instituicao-colaborador-perfil.model';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';

enum ModalOperation {
    add = 0,
    update = 1
}

interface UI {
    queryUsuario: any;
    usuarioInfoLabel: any;
    instituicaoColaboradorPerfis: Array<InstituicaoColaboradorPerfilModel>;
}

@Component({
    template: require('./page-instituicao-colaborador.html')
})
export class PageInstituicaoColaboradorComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    ui: UI = {
        queryUsuario: async (term) => {
            let itens = await FACTORY_CONSTANT.UsuarioFactory.allByTermo(term, false, false);
            return itens;
        },

        usuarioInfoLabel: (item: UsuarioInfoModel) => {
            let labelObj = {} as any;
            labelObj.key = item.label;
            labelObj.label = `<div><span>${item.label}</span><div><div><span style="float:left;">${item.rg}</span><span style="float:right;">${item.cpf}</span></div>`;
            return labelObj;
        },

        instituicaoColaboradorPerfis: undefined
    };

    model: InstituicaoColaboradorModel = new InstituicaoColaboradorModel();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.ui.instituicaoColaboradorPerfis = await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoColaboradorPerfil(AppRouter.app.$route.params.id);
            if (this.operation === RouterPathType.upd) {
                this.model = await FACTORY_CONSTANT.InstituicaoFactory.detailInstituicaoColaborador(this.$route.params.id, this.$route.params.idInstituicaoColaborador);
            }
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
            AppRouter.back();
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    async save() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RouterPathType.add):
                    {
                        await FACTORY_CONSTANT.InstituicaoFactory.addInstituicaoColaborador(this.$route.params.id, this.model);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await FACTORY_CONSTANT.InstituicaoFactory.updateInstituicaoColaborador(this.$route.params.id, this.model);
                        break;
                    }
            }
            NotifyUtil.successG(I18N_ERROR_GENERIC.MODELO_SALVAR, ApplicationService.getLanguage());
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    addInstituicaoColaboradorPerfil(idPerfil: number) {
        this.model.perfis.push(idPerfil.toString());
        this.$forceUpdate();
    }

    removeInstituicaoColaboradorPerfil(idPerfil: number) {
        this.model.perfis.splice(this.model.perfis.indexOf(idPerfil.toString()), 1);
        this.$forceUpdate();
    }

    getTableInstituicaoColaboradorPerfil() {
        let menu = new CardTableMenu();
        menu.row = [
            new CardTableMenuEntry({
                label: (item) => 'Remover',
                method: (item) => this.removeInstituicaoColaboradorPerfil(item),
                btnClass: (item) => ['btn-danger'],
                iconClass: (item) => ['fa', 'fa-times']
            })
        ];
        let columns = [
            new CardTableColumn({
                value: (item: string) => {
                    return this.ui.instituicaoColaboradorPerfis.find(x => x.id === parseInt(item)).nome;
                },
                label: () => 'Perfil'
            })
        ];
        return { columns: columns, menu: menu };
    }

}