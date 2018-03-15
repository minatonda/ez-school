import { Vue, Watch } from 'vue-property-decorator';
import { Prop, Component } from 'vue-property-decorator';
import { CardTableMenu, CardTableColumn, CardTableMenuEntry } from './card-table.types';
import { DropDownItem } from '../dropdown/dropdown';
import { FormBuilderUtil } from '../form-builder/form-builder.util';

interface UI {

}

@Component({
    template: require('./card-table.html')
})
export class CardTableComponent extends Vue {

    @Prop()
    menu: CardTableMenu;

    @Prop()
    columns: Array < CardTableColumn > ;

    @Prop()
    itens: Array < any > ;

    ui: UI = {};

    util = FormBuilderUtil;

    created() {}

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
        return this.itens;
    }

    /* MENU MANAGEMENT */
    getMenuRow() {
        return this.menu && this.menu.row;
    }

    getMenuMain() {
        return this.menu && this.menu.main;
    }

    getMenuItensAsDropDown(menu: Array < CardTableMenuEntry > ) {
        if (menu) {
            return menu.map(x => {
                let dropdownItem: DropDownItem = {
                    text: `<i class='${x.iconClass(this.getItens()).join(' ')} mr-2'></i>${x.label(this.getItens())}`,
                    item: x.method
                };
                return dropdownItem;
            });
        }
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

    isMenuItemDisabled(menu: CardTableMenuEntry, item: any) {
        return menu.disabled ? menu.disabled(item) : false;
    }

    isColumnInput(column: CardTableColumn) {
        return column.config && column.config.input;
    }

    doCallMenuMethod(menuItem: CardTableMenuEntry, item: any) {
        menuItem.method(item);
    }


}