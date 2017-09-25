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