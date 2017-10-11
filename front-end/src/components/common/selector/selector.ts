import { Vue } from 'vue-property-decorator';
import { Prop, Component, Model, Watch } from 'vue-property-decorator';

@Component({
    template: require('./selector.html')
})
export class SelectorComponent extends Vue {

    open: boolean = false;
    close: boolean = false;
    text: string = '';
    pointer = 0;
    internalValue: any = false;

    @Prop()
    itens: Array<any>;
    @Prop({ default: null })
    value;
    @Prop(String)
    placeholder: string;
    @Prop([String, Function])
    label: any;
    @Prop({ type: Function })
    change: Function;
    @Prop({ type: Boolean, default: false })
    clear: boolean;
    @Prop({ type: Boolean, default: false })
    clearAfter: boolean;

    onCreate() {
        this.internalValue = this.value;
    }

    public isOpen() {
        return this.open;
    }

    public isSelected(item) {
        if (item) {
            return this.internalValue === item;
        }
        else {
            return !!this.internalValue;
        }
    }

    public isPointer(index) {
        return this.pointer === index;
    }

    public isCloseEnabled() {
        return this.close;
    }

    public pointerForward() {

        if (this.getItens(this.text).length - 1 > this.pointer) {
            this.pointer++;
        }
        this.$forceUpdate();
    }

    public pointerBackward() {
        if (-1 < this.pointer) {
            this.pointer--;
        }
        this.$forceUpdate();
    }

    public activate() {
        this.open = true;
    }

    public deactivate() {
        this.open = false;
        this.text = '';
        this.pointer = -1;
    }

    public enableClose() {
        this.close = true;
    }

    public disableClose() {
        this.close = false;
    }

    public select(item) {
        if (!this.clearAfter) {
            this.internalValue = item;
            this.$emit('input', item);
        }
        this.$emit('change', item);
        this.deactivate();
    }

    public selectByIndex(index) {
        this.select(this.getItens(this.text)[index]);
    }

    public getPlaceholder() {
        if (this.internalValue && this.internalValue) {
            return this.getItemLabel(this.internalValue);
        }
        else {
            return this.placeholder;
        }
    }

    public getItens(text) {
        if (text) {
            let itens = this.itens.filter(x => this.prepareCompare(this.getItemLabel(x)).indexOf(this.prepareCompare(text)) > -1);
            itens = itens.sort(x => this.prepareCompare(this.getItemLabel(x)).indexOf(this.prepareCompare(text)));
            return itens;
        }
        else {
            return this.itens;
        }
    }

    public getItemLabel(item, highlight?: boolean) {
        let label: string;
        if (this.label instanceof String || ((typeof this.label) === 'string')) {
            label = item[this.label as string];
        }
        else {
            label = this.label(item);
        }
        return highlight ? this.highlight(label, this.text) : label;
    }

    public highlight(label, query) {
        if (!label || !query) {
            return label;
        }
        let labelPrepared = this.prepareCompare(label);
        let queryPrepared = this.prepareCompare(query);

        if (labelPrepared.indexOf(queryPrepared) < 0) return label;

        let start = labelPrepared.indexOf(queryPrepared);
        let end = start + queryPrepared.length;

        let highlighted = '<b>' + label.substring(start, end) + '</b>';
        return label.substring(0, start) + highlighted + label.substring(end);
    }

    public prepareCompare(str) {
        if (!str) {
            return '';
        }
        return this.removeAccents(str).toLowerCase().trim();
    }

    public removeAccents(str) {
        if (!str) {
            return '';
        }
        if (str) {
            let helperRemoveAccents_map = { 'Ã': 'A', 'Â': 'A', 'Á': 'A', 'ã': 'a', 'â': 'a', 'á': 'a', 'à': 'a', 'É': 'E', 'Ê': 'E', 'È': 'E', 'é': 'e', 'ê': 'e', 'è': 'e', 'Í': 'I', 'Î': 'I', 'Ì': 'I', 'î': 'I', 'í': 'i', 'ì': 'i', 'Ô': 'O', 'Õ': 'O', 'Ó': 'O', 'Ò': 'O', 'ô': 'o', 'õ': 'o', 'ó': 'o', 'ò': 'o', 'Ú': 'U', 'Ù': 'U', 'ú': 'u', 'ù': 'u', 'ç': 'c' };
            return str.replace(/[^A-Za-z0-9\[\] ]/g, function (a) {
                return helperRemoveAccents_map[a] || a;
            });
        }
        else {
            return str;
        }
    }

    @Watch('value')
    public onValueChange(val) {
        this.internalValue = val;
    }

    @Watch('clear')
    public onValueClear(val) {
        if (this.clear) {
            this.select(undefined);
        }
    }

}