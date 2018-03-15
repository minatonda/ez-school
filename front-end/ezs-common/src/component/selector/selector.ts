import { Vue } from 'vue-property-decorator';
import { Prop, Component, Model, Watch } from 'vue-property-decorator';

interface Data {
    value: any;
    itens: Array < any > ;
    text: string;
    enabled: boolean;
    canDisable: boolean;
    pointer: number;
    loading: boolean;
}

@Component({
    template: require('./selector.html')
})
export class SelectorComponent extends Vue {

    data: Data = {
        value: undefined,
        itens: new Array < any > (),
        text: undefined,
        enabled: false,
        canDisable: true,
        pointer: 0,
        loading: false
    };

    @Prop([String, Object, Array, Number, Boolean])
    value: any;

    @Prop({ type: Array })
    itens: Array < any > ;

    @Prop({ type: Function })
    query: Function;

    @Prop([String, Function])
    label: any;

    @Prop({ type: Boolean, default: false })
    clear: boolean;
    @Prop({ type: Boolean, default: false })
    clearAfter: boolean;
    @Prop({ type: Boolean, default: false })
    nullable: boolean;
    @Prop({ type: Boolean, default: false })
    onlyAutoComplete: boolean;

    @Prop(String)
    outputAs: string;

    @Prop(String)
    name: string;

    @Prop(String)
    inputLabel: string;

    @Prop(String)
    placeholder: string;


    @Prop(Number)
    limit: number;

    async mounted() {
        this.selectItem(this.value);
        if (this.itens) {
            this.data.itens = this.itens;
        }
        else if (this.query) {
            this.doQuery(this.data.text);
        }
    }

    @Watch('value')
    onValueChange(val) {
        if (this.outputAs) {
            this.selectItem(this.data.itens.find(x => x[this.outputAs] === val));
        }
        else {
            this.selectItem(val);
        }
    }

    @Watch('query')
    async onQueryChange(val) {
        this.selectItem(undefined);
        this.doQuery(this.data.text);
    }

    @Watch('itens')
    onItensChange(val) {
        if (val) {
            this.data.itens = val;
        }
        else {
            this.data.itens = new Array < any > ();
        }
    }

    @Watch('clear')
    onValueClear(val) {
        if (this.clear) {
            this.selectItem(undefined);
        }
    }

    async doQuery(val) {
        this.data.loading = true;
        this.data.itens = await this.query(val);
        this.data.loading = false;
    }

    onTextChanged(val) {
        // Caso tenha apagado o texto, irá resetar o campo
        if (!val) {
            this.selectItem(undefined);
        }
        else {
            if (this.query) {
                this.doQuery(val);
            }
            if (this.isOnlyAutoComplete()) {
                this.selectItem(this.data.text);
            }
        }
    }

    onTextFieldBlur() {
        if (this.canDisable()) {
            this.disable();
        }
    }

    isEnabled() {
        return this.data.enabled;
    }

    canDisable() {
        return this.data.canDisable;
    }

    canDeselect() {
        return !this.isOnlyAutoComplete() && this.isNullable();
    }

    isNullable() {
        return this.nullable;
    }

    isOnlyAutoComplete() {
        return this.onlyAutoComplete;
    }

    isPointerAt(index) {
        return this.data.pointer === index;
    }

    isItemSelected(item) {
        return this.data.value === item;
    }

    isAnyItemSelected() {
        return !!this.data.value;
    }

    enable() {
        this.data.enabled = true;
    }

    disable() {
        this.data.enabled = false;
        this.enableDisable();
    }

    enableDisable() {
        this.data.canDisable = true;
    }

    disableDisable() {
        this.data.canDisable = false;
    }

    setInternalValue(item) {
        if (item && this.outputAs && this.isTypeOf(item, ['string'])) {
            this.data.value = this.itens.find(x => x[this.outputAs] === item);
            this.data.text = this.getItemLabel(item);
            this.$emit('input', this.data.value);
        }
        else if (item && this.outputAs) {
            this.data.value = item;
            this.data.text = this.getItemLabel(item);
            this.$emit('input', this.data.value[this.outputAs]);
        }
        else {
            this.data.value = item;
            this.$emit('input', this.data.value);
        }
    }

    selectItem(item ? ) {
        if (this.isOnlyAutoComplete() && item && !this.isTypeOf(item, ['string', 'number', 'boolean'])) {
            let autoCompleteItem = this.getItemLabel(item, false);
            this.data.text = autoCompleteItem;
            this.setInternalValue(autoCompleteItem);
            this.$emit('change', this.data.text, item);
        }
        else if (this.isOnlyAutoComplete()) {
            this.setInternalValue(this.data.text);
            this.$emit('change', this.data.text);
        }
        else if (item && !this.isTypeOf(item, ['string', 'number', 'boolean', String, Number, Boolean])) {
            this.data.text = this.getItemLabel(item, false);
            this.setInternalValue(item);
            this.$emit('change', item);
        }
        else {
            this.data.text = '';
            this.setInternalValue(item);
            this.getPlaceholder();
            this.$emit('change', item);
        }
    }

    selectItemByIndex(index) {
        this.selectItem(this.getItensByText(this.data.text)[index]);
    }

    pointerForward() {
        if (this.getItensByText(this.data.text).length - 1 > this.data.pointer) {
            this.data.pointer++;
        }
        this.$forceUpdate();
    }

    pointerBackward() {
        if (-1 < this.data.pointer) {
            this.data.pointer--;
        }
        this.$forceUpdate();
    }

    getItensByText(text: string) {
        if (this.limit) {
            return this.filter(text, this.data.itens).slice(0, this.limit);
        }
        return this.filter(text, this.data.itens);
    }

    getPlaceholder() {
        if (this.data.value) {
            return this.getItemLabel(this.data.value);
        }
        else {
            return this.placeholder;
        }
    }

    getItemLabel(item, highlight?: boolean) {
        let labelResult: any;
        if (!item) {
            return '';
        }
        else if (this.label === undefined) {
            return item;
        }
        else if (this.isTypeOf(this.label, [String, 'string'])) {
            labelResult = item[this.label as string];
            return highlight ? this.highlight(labelResult, this.data.text) : labelResult;
        }
        else {
            labelResult = this.label(item);
            if (this.isTypeOf(labelResult, [String, 'string'])) {
                return highlight ? this.highlight(labelResult, this.data.text) : labelResult;
            }
            else {
                return highlight ? this.highlight(labelResult.label, this.data.text) : labelResult.key;
            }
        }
    }

    // Retorna TRUE se o tipo do item for qualquer um dos tipos passado no array
    isTypeOf(item: any, types: Array < any > ) {
        let isTypeOf = false;

        types.forEach(type => {
            // verifica se o tipo é primitivo ou objeto
            if (typeof type === 'function') {

                // Se for "objeto", valida com instanceof
                if (item instanceof type) {
                    isTypeOf = true;
                }

                // Se for "primitivo", valida com typeof
            }
            else if (typeof item === type) {
                isTypeOf = true;
            }
        });
        return isTypeOf;
    }

    filter(text: string, itens: Array < any > ) {
        if (itens && text) {
            let filtered = itens
                .filter(x => {
                    return this.prepareCompare(this.getItemLabel(x, true)).indexOf(this.prepareCompare(text)) > -1;
                })
                .sort((x, y) => {
                    return this.prepareCompare(this.getItemLabel(x, false)).indexOf(this.prepareCompare(text)) - this.prepareCompare(this.getItemLabel(y, false)).indexOf(this.prepareCompare(text));
                });
            return this.sortInputFirst(text, filtered, (x) => this.getItemLabel(x, true));

        }
        else {
            return itens;
        }
    }

    sortInputFirst(input: string, data: Array < any > , label) {
        let first = [];
        let others = [];
        for (let i = 0; i < data.length; i++) {
            if (this.prepareCompare(label(data[i])).indexOf(this.prepareCompare(input)) === 0) {
                first.push(data[i]);
            }
            else {
                others.push(data[i]);
            }
        }
        return (first.concat(others));
    }

    highlight(label, query, subStringStart ? ) {
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

    isInsideTag(text, start) {
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

    prepareCompare(text) {
        if (!text) {
            return '';
        }
        return this.removeAccents(text).toLowerCase().trim();
    }

    removeAccents(text) {
        if (!text) {
            return '';
        }
        else if (text) {
            let helperRemoveAccents_map = { 'Ã': 'A', 'Â': 'A', 'Á': 'A', 'ã': 'a', 'â': 'a', 'á': 'a', 'à': 'a', 'É': 'E', 'Ê': 'E', 'È': 'E', 'é': 'e', 'ê': 'e', 'è': 'e', 'Í': 'I', 'Î': 'I', 'Ì': 'I', 'î': 'I', 'í': 'i', 'ì': 'i', 'Ô': 'O', 'Õ': 'O', 'Ó': 'O', 'Ò': 'O', 'ô': 'o', 'õ': 'o', 'ó': 'o', 'ò': 'o', 'Ú': 'U', 'Ù': 'U', 'ú': 'u', 'ù': 'u', 'ç': 'c' };
            return text.replace(/[^A-Za-z0-9\[\] ]/g, function(a) {
                return helperRemoveAccents_map[a] || a;
            });
        }
    }

}