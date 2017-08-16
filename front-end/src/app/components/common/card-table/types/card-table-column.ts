import { CardTableColumnConfig } from './card-table-column.config';

export class CardTableColumn {
    constructor(value: (item: any) => string | number | Date, label: () => string | number | Date, config ?: CardTableColumnConfig) {
        this.value = value;
        this.label = label;
        this.config = config;
    }
    value: (item: any) => string | number | Date;
    label: () => string | number | Date;
    config: CardTableColumnConfig;
}