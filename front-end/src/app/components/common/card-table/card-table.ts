import Vue from 'vue';
import Component from 'vue-class-component';
import { prop } from 'vue-property-decorator/lib/vue-property-decorator';
import { CardTableMenu } from './types/card-table-menu';
import { CardTableColumn } from './types/card-table-column';
import { FilterUtil } from '../../../common/util/filter.util';
import { CardTableMenuEntry } from './types/card-table-menu-entry';

@Component({
    template: require('./card-table.html')
})
export class CardTableComponent extends Vue {

    @prop
    private menu: CardTableMenu;
    @prop
    private columns: Array<CardTableColumn>;
    @prop
    private itens: Array<any>;

    constructor() {
        super();
    }

    /* COLUMN MANAGEMENT */
    public getColumns() {
        return this.columns;
    }

    public getColumnLabel(column: CardTableColumn) {
        return column.label();
    }

    public getColumnValue(column: CardTableColumn, item: any) {
        if (column.config) {
            if (column.config.moeda) {
                return FilterUtil.paraMoeda(column.value(item) as number);
            }
        }
        return column.value(item);
    }
    /* COLUMN MANAGEMENT - END*/

    public getItens() {
        return this.itens;
    }

    /* MENU MANAGEMENT */
    public getMenuRow() {
        return this.menu && this.menu.row;
    }

    public getMenuMain() {
        return this.menu && this.menu.main;
    }

    public getMenuItemLabel(menuItem: CardTableMenuEntry, item: any) {
        return menuItem.label(item);
    }

    public getMenuIconClass(menuItem: CardTableMenuEntry, item: any) {
        return menuItem.iconClass(item);
    }

    public getMenuButtonClass(menuItem: CardTableMenuEntry, item: any) {
        return menuItem.btnClass(item);
    }

    public triggerMenuMethod(menuItem: CardTableMenuEntry, item: any) {
        menuItem.method(item);
    }
    /* MENU MANAGEMENT - END*/

}