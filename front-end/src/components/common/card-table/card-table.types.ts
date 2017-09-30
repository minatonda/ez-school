export interface CardTableColumnConfig {
    moeda?: boolean;
}

export class CardTableColumn {
    constructor(value: (item: any) => string | number | Date, label: () => string | number | Date, config?: CardTableColumnConfig) {
        this.value = value;
        this.label = label;
        this.config = config;
    }
    value: (item: any) => string | number | Date;
    label: () => string | number | Date;
    config: CardTableColumnConfig;
}

export class CardTableMenuEntry {
    constructor(method: (item: any) => any, label: (item: any) => string | number | Date, iconClass: (item: any) => Array<string>, btnClass?: (item: any) => Array<string>) {
        this.method = method;
        this.label = label;
        this.iconClass = iconClass;
        this.btnClass = btnClass;
    }
    method: (item: any) => string | number | Date;
    label: (item: any) => string | number | Date;
    iconClass: (item: any) => Array<string>;
    btnClass: (item: any) => Array<string>;
}

export class CardTableMenu {
    main: Array<CardTableMenuEntry>;
    row: Array<CardTableMenuEntry>;
}