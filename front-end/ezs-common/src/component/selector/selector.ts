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
    queryItens: Array < any > = [];

    @Prop()
    itens: Array < any > ;
    @Prop({ default: null })
    value;
    @Prop(String)
    placeholder: string;
    @Prop([String, Function])
    label: any;
    @Prop({ type: Boolean })
    disabled: false ;
    @Prop({ type: Function })
    change: Function;
    @Prop({ type: Boolean, default: false })
    clear: boolean;
    @Prop({ type: Boolean, default: false })
    clearAfter: boolean;
    @Prop({ type: Function, default: undefined })
    query: Function;

    created() {
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

    public isDisabled() {
        return !!this.disabled;
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
        let itens = this.query ? this.queryItens : this.itens;
        return this.filter(text, itens);
    }

    public filter(text, itens) {
        if (itens && text) {
            let filtered = itens.filter(x => this.prepareCompare(this.getItemLabel(x, true)).indexOf(this.prepareCompare(text)) > -1);
            filtered = filtered.sort(x => this.prepareCompare(this.getItemLabel(x, true)).indexOf(this.prepareCompare(text)));
            return filtered;
        }
        else {
            return itens;
        }
    }

    public getItemLabel(item, highlight?: boolean) {
        let labelResult: any;
        if (this.label === undefined) {
            return item;
        }
        else if (this.label instanceof String || ((typeof this.label) === 'string')) {
            labelResult = item[this.label as string];
            return highlight ? this.highlight(labelResult, this.text) : labelResult;
        }
        else {
            labelResult = this.label(item);
            if (labelResult instanceof String || ((typeof labelResult) === 'string')) {
                return highlight ? this.highlight(labelResult, this.text) : labelResult;
            }
            else {
                return highlight ? this.highlight(labelResult.label, this.text) : labelResult.key;
            }
        }
    }

    public highlight(label, query, subStringStart ? ) {
        if (!label || !query) {
            return label;
        }

        let labelPrepared = this.prepareCompare(label);
        let queryPrepared = this.prepareCompare(query);

        if (labelPrepared.substring(subStringStart).indexOf(queryPrepared) < 0) {
            return label;
        }
        else {
            let start = labelPrepared.substring(subStringStart).indexOf(queryPrepared) + (subStringStart || 0);
            let end = start + queryPrepared.length;

            if (this.isInsideTag(labelPrepared, end) !== undefined) {
                return this.highlight(label, query, this.isInsideTag(labelPrepared, end));
            }
            else {
                let highlighted = '<b>' + label.substring(start, end) + '</b>';
                return label.substring(0, start) + highlighted + label.substring(end);
            }
        }
    }

    public isInsideTag(text, start) {
        let endTag = undefined;
        let startCount = start;
        while (startCount <= text.length) {
            let char = text[startCount];
            if (char === '<') {
                break;
            }
            else if (char === '>') {
                endTag = startCount;
                break;
            }
            startCount++;
        }
        return endTag;
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
            return str.replace(/[^A-Za-z0-9\[\] ]/g, function(a) {
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

    @Watch('text')
    public async onTextChanged(val) {
        if (this.query) {
            this.queryItens = await this.query(val);
        }
    }

}