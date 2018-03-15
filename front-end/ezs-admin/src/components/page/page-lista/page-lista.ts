import { Vue, Component, Prop, Watch } from 'vue-property-decorator';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { CardTableMenu, CardTableMenuEntry, CardTableColumn } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { NOTIFY_TYPE, NotifyUtil } from '../../../../../ezs-common/src/util/notify/notify.util';
import { ApplicationService } from '../../../module/service/application.service';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { FormBuilderInput } from '../../../../../ezs-common/src/component/form-builder/form-builder.types';
import { Pagination } from '../../../../../ezs-common/src/component/pagination/pagination';

interface UI {
    itens: Array < any > ;
    resumes: Array < Resume > ;
    pagination: Pagination;
    filterData: any;
}

interface Resume {
    label: string;
    value: string;
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
    columns: Array < CardTableColumn > ;

    @Prop([Object])
    menu: CardTableMenu;

    @Prop({ type: Array, default: () => [] })
    filters: Array < FormBuilderInput > ;

    @Prop([Array, Function])
    resumes: any;

    ui: UI = {
        itens: new Array < any > (),
        resumes: undefined,
        pagination: { page: 1, limit: 10, pageTotal: 1 },
        filterData: {}
    };

    @Watch('resumes')
    async onResumesChange(val) {
        this.setResumes(val);
    }

    @Watch('query')
    async onQueryChange(val) {
        this.doQuery(this.ui.pagination, this.ui.filterData);
    }

    onPaginationSettingsChanged(Pagination: Pagination) {
        this.ui.pagination = Pagination;
        this.doQuery(this.ui.pagination, this.ui.filterData);
    }

    onFilterSubmit(data: any) {
        this.ui.filterData = data;
        this.ui.pagination.page = 1;
        this.onPaginationSettingsChanged(Object.assign({}, this.ui.pagination));
        this.doQuery(this.ui.pagination, this.ui.filterData);
    }

    async mounted() {
        this.doQuery(this.ui.pagination, this.ui.filterData);
        this.setResumes(this.resumes);
    }

    async doQuery(Pagination: Pagination, filterData) {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER, true);
            this.ui.itens = [];
            this.ui.itens = await this.query(Pagination.page - 1, Pagination.limit, filterData) as Array < any > ;
            // let result = await this.query(Pagination.page - 1, Pagination.limit, filterData) as Array < any > ;
            // this.ui.itens = result.content;

            // this.ui.pagination = {
            //     page: result.number + 1,
            //     pageTotal: result.totalPages,
            //     limit: Pagination.limit,
            //     totalItens: result.totalElements
            // };
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER, true);
        }
    }

    async doAdd(item) {
        if (this.routePathAdd && this.queryAdd) {
            this.queryAdd(this.routePathAdd);
        }
        else if (this.routePathAdd) {
            AppRouter.push(this.routePathAdd);
        }
        else {
            NotifyUtil.error('Funcionalidade não implementado', 'Erro');
        }
    }

    async doUpdate(item) {
        if (this.routePathUpdate && this.queryUpdate) {
            this.queryUpdate(item, this.routePathUpdate);
        }
        else if (this.routePathUpdate) {
            AppRouter.push({ name: this.routePathUpdate, params: item });
        }
        else {
            NotifyUtil.error('Funcionalidade não implementado', 'Erro');
        }
    }

    async doRemove(item) {
        if (this.queryRemove) {
            try {
                AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER, true);
                await this.queryRemove(item.id);

                NotifyUtil.success('Registro removido com êxito', this.alias);
                AppRouter.push(AppRouterPath.ROOT);
                setImmediate(() => {
                    AppRouter.push(this.$route.path);
                });
            }
            catch (error) {

            }
            finally {
                AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER, true);
            }
        }
        else {
            NotifyUtil.error('Funcionalidade não implementado', 'Erro');
        }
    }

    getColumns() {
        return this.columns || [
            new CardTableColumn({
                value: (item: any) => item.id,
                label: () => 'Id'
            }),
            new CardTableColumn({
                value: (item: any) => item.label,
                label: () => 'Informação'
            })
        ];
    }

    getItens() {
        return this.ui.itens || [];
    }

    getMenu() {
        let menu = new CardTableMenu();
        menu.row = [];
        menu.main = [];
        if (this.routePathUpdate) {
            menu.row.push(new CardTableMenuEntry({
                label: (item) => 'Atualizar',
                method: (item) => this.doUpdate(item),
                btnClass: (item) => ['btn-primary'],
                iconClass: (item) => ['fa', 'fa-edit']
            }));
        }
        if (this.queryRemove) {
            menu.row.push(new CardTableMenuEntry({
                label: (item) => 'Remover',
                method: (item) => this.doRemove(item),
                btnClass: (item) => ['btn-danger'],
                iconClass: (item) => ['fa', 'fa-times']
            }));
        }
        if (this.routePathAdd) {
            menu.main.push(
                new CardTableMenuEntry({
                    label: (item) => 'Adicionar',
                    method: (item) => this.doAdd(item),
                    btnClass: (item) => ['btn-primary'],
                    iconClass: (item) => ['fa', 'fa-plus']
                }));
        }
        if (this.menu && this.menu.row) {
            menu.row = Array.prototype.concat(menu.row, this.menu.row);
        }
        if (this.menu && this.menu.main) {
            menu.main = Array.prototype.concat(menu.main, this.menu.main);
        }
        return menu;
    }

    async setResumes(resumes ? ) {
        if (resumes instanceof Array) {
            this.ui.resumes = resumes;
        }
        else if (resumes instanceof Function) {
            this.ui.resumes = await resumes();
        }
        else {
            this.ui.resumes = undefined;
        }
    }

    getFilters() {
        return this.filters || [];
    }

}