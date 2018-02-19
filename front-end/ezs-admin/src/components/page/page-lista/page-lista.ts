import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { NOTIFY_TYPE, NotifyUtil } from '../../../../../ezs-common/src/util/notify/notify.util';
import { ApplicationService } from '../../../module/service/application.service';
import { I18N_MESSAGE } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';

interface UI {
    itens: Array<any>;
}

@Component({
    template: require('./page-lista.html')
})
export class PageListaComponent extends Vue {

    @Prop()
    alias: string;

    @Prop([Function])
    query: any;

    @Prop()
    routePathAdd: AppRouterPath;

    @Prop()
    routePathUpdate: AppRouterPath;

    @Prop([Function])
    queryAdd: any;

    @Prop([Function])
    queryUpdate: any;

    @Prop([Function])
    queryRemove: any;

    @Prop({ type: Array })
    columns: Array<CardTableColumn>;

    @Prop([Object])
    menu: CardTableMenu;

    ui: UI = {
        itens: undefined
    };

    async mounted() {
        await this.doQuery();
    }

    @Watch('query')
    async onQueryChange() {
        await this.doQuery();
    }

    doAdd() {
        if (this.queryAdd) {
            this.queryAdd(this.routePathAdd);
        }
        else {
            AppRouter.push(this.routePathAdd);
        }
    }

    doUpdate(item) {
        if (this.queryUpdate) {
            this.queryUpdate(item, this.routePathUpdate);
        }
        else {
            AppRouter.push({ name: this.routePathUpdate, params: item });
        }
    }

    async doRemove(item) {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER, true);
            await this.queryRemove(item.id);
            await this.doQuery();
            NotifyUtil.notifyI18N(I18N_MESSAGE.MODELO_DESATIVAR, ApplicationService.getLanguage(), NOTIFY_TYPE.SUCCESS);
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.MODELO_DESATIVAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER, true);
        }
    }

    async doQuery() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER, true);
            this.ui.itens = await this.query();
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.CONSULTAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
            AppRouter.back();
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER, true);
        }
    }

    getColumns() {
        return this.columns || [
            new CardTableColumn((item) => item.id, () => 'ID'),
            new CardTableColumn((item) => item.label, () => 'Informação')
        ];
    }

    getItens() {
        return this.ui.itens || [];
    }

    getMenu() {
        let menu = new CardTableMenu();
        menu.row = Array.prototype.concat(
            [
                new CardTableMenuEntry(
                    (item) => this.doUpdate(item),
                    (item) => 'Atualizar',
                    (item) => ['fa', 'fa-edit'],
                    (item) => ['btn-primary']
                ),
                new CardTableMenuEntry(
                    (item) => this.doRemove(item),
                    (item) => 'Remover',
                    (item) => ['fa', 'fa-times'],
                    (item) => ['btn-danger']
                )
            ],
            this.menu.row
        );
        menu.main = Array.prototype.concat(
            [
                new CardTableMenuEntry(
                    (item) => this.doAdd(),
                    (item) => 'Adicionar',
                    (item) => ['fa', 'fa-plus'],
                    (item) => ['btn-primary']
                )
            ],
            this.menu.main
        );
        return menu;
    }

}