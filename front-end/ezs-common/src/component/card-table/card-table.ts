import { Vue, Watch } from 'vue-property-decorator';
import { Prop, Component } from 'vue-property-decorator';
import { CardTableMenu, CardTableColumn, CardTableFilter, CardTableMenuEntry, CardTablePage } from './card-table.types';

interface UI {
    pageData: CardTablePage<any>;
    itens: Array<any>;
    page: number;
    limit: number;
    filter: any;
}

@Component({
    template: require('./card-table.html')
})
export class CardTableComponent extends Vue {

    @Prop()
    menu: CardTableMenu;

    @Prop()
    columns: Array<CardTableColumn>;

    @Prop()
    itens: Array<any>;

    @Prop({ type: Function })
    query: Function;

    @Prop({ type: Array, default: () => [] })
    filters: Array<CardTableFilter>;

    ui: UI = {
        pageData: undefined,
        itens: undefined,
        page: 0,
        limit: 10,
        filter: {}
    };

    @Watch('itens')
    onItensChanged(val) {
        this.ui.itens = this.itens;
    }

    @Watch('query')
    onQueryChanged(val) {
        this.doQuery(this.ui.page, this.ui.limit, this.ui.filter);
    }

    created() {
        this.doQuery(this.ui.page, this.ui.limit, this.ui.filter);
    }

    async doQuery(page, limit, filter) {
        if (this.query && !this.itens) {
            this.ui.pageData = await this.query(page, limit, filter) as CardTablePage<any>;
            this.ui.itens = this.ui.pageData.content;
        }
    }

    async doIncreasePage() {
        this.ui.page++;
        await this.doQuery(this.ui.page, this.ui.limit, this.ui.filter);
    }

    async doDecreasePage() {
        this.ui.page--;
        await this.doQuery(this.ui.page, this.ui.limit, this.ui.filter);
    }

    async doIncreaseLimit() {
        this.ui.limit += 10;
        await this.doQuery(this.ui.page, this.ui.limit, this.ui.filter);
    }

    async doDecreaseLimit() {
        this.ui.limit -= 10;
        await this.doQuery(this.ui.page, this.ui.limit, this.ui.filter);
    }


    /* COLUMN MANAGEMENT */
    getColumns() {
        return this.columns;
    }

    getColumnLabel(column: CardTableColumn) {
        return column.label();
    }

    getColumnValue(column: CardTableColumn, item: any) {
        if (column.config) {
            if (column.config.moeda) {
                return column.value(item);
            }
        }
        return column.value(item);
    }
    /* COLUMN MANAGEMENT - END*/

    getItens() {
        return this.ui.itens;
    }

    /* MENU MANAGEMENT */
    getMenuRow() {
        return this.menu && this.menu.row;
    }

    getMenuMain() {
        return this.menu && this.menu.main;
    }

    getMenuItemLabel(menuItem: CardTableMenuEntry, item: any) {
        return menuItem.label(item);
    }

    getMenuIconClass(menuItem: CardTableMenuEntry, item: any) {
        return menuItem.iconClass(item);
    }

    getMenuButtonClass(menuItem: CardTableMenuEntry, item: any) {
        return menuItem.btnClass(item);
    }

    triggerMenuMethod(menuItem: CardTableMenuEntry, item: any) {
        menuItem.method(item);
    }
    /* MENU MANAGEMENT - END*/

}