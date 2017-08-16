import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { CardTableColumn } from './types/card-table-column';
import { prop } from 'vue-property-decorator/lib/vue-property-decorator';
import { FilterUtil } from '../../../common/util/filter.util';

@Component({
    template: require('./card-table.html')
})
export class CardTableComponent extends Vue {

    @prop
    private columns: Array < CardTableColumn > ;
    @prop
    private itens: Array < any > ;

    constructor() {
        super();
    }

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

    public getItens() {
        return this.itens;
    }


}