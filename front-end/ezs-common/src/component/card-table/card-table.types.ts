import { FlaggerEntry } from '../flagger/flagger';
import { FormBuilderInput } from '../form-builder/form-builder.types';

export interface CardTableColumnConfig {
    moeda?: boolean;
    input?: FormBuilderInput;
    title?: string;
}

interface CardTableColumnInterface {
    value: (item: any) => string | number | Date;
    label: () => string | number | Date;
    config?: CardTableColumnConfig;
}

export class CardTableColumn {
    constructor(config: CardTableColumnInterface) {
        for (let key in config) {
            this[key] = config[key];
        }
        if (!config.config) {
            this.config = {};
        }
    }
    value: (item: any) => string | number | Date;
    label: () => string | number | Date;
    config?: CardTableColumnConfig;
}

interface CardTableMenuEntryInterface {
    method: (item: any) => void;
    label: (item: any) => string | number | Date;
    iconClass: (item: any) => Array < string > ;
    btnClass: (item: any) => Array < string > ;
    disabled?: (item: any) => boolean;
}

export class CardTableMenuEntry implements CardTableMenuEntryInterface {
    constructor(config: CardTableMenuEntryInterface) {
        this.method = config.method;
        this.label = config.label;
        this.iconClass = config.iconClass;
        this.btnClass = config.btnClass;
        this.disabled = config.disabled;
    }
    method: (item: any) => void;
    label: (item: any) => string | number | Date;
    iconClass: (item: any) => string[];
    btnClass: (item: any) => string[];
    disabled: (item: any) => boolean;
}

export class CardTableMenu {
    main: Array < CardTableMenuEntry > ;
    row: Array < CardTableMenuEntry > ;
}