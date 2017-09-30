import { Vue } from 'vue-property-decorator';
import { Prop, Component } from 'vue-property-decorator';
import { CardTableMenu, CardTableColumn, CardTableMenuEntry } from './card-table.types';

@Component({
    template: require('./card-table.html')
})
export class CardTableComponent extends Vue {

    @Prop()
    private menu: CardTableMenu;
    @Prop()
    private columns: Array<CardTableColumn>;
    @Prop()
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
                return column.value(item);
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