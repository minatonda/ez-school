import { Prop, Component, Model, Watch, Vue } from 'vue-property-decorator';

interface Data {
    value: Array<any>;
}

@Component({
    template: require('./flagger.html')
})
export class FlaggerComponent extends Vue {

    data: Data = {
        value: []
    };

    @Prop({ type: Array, default: () => [] })
    entries: Array<FlaggerEntry>;

    @Prop([Array])
    value: any;

    mounted() {
        this.doUpdateValue(this.value);
    }

    @Watch('value')
    onValueChange(val) {
        this.doUpdateValue(val);
    }

    doToggleCheck(item: FlaggerEntry) {
        if (this.isChecked(item)) {
            this.doUncheck(item);
        }
        else {
            this.doCheck(item);
        }
    }

    doCheck(item: FlaggerEntry) {
        this.data.value.push(item.value);
        this.doUpdateValue(this.data.value);
    }

    doUncheck(item: FlaggerEntry) {
        this.data.value = this.data.value.filter(elem => elem !== item.value);
        this.doUpdateValue(this.data.value);
    }

    doUpdateValue(value: Array<any>) {
        this.data.value = value;
        this.$emit('input', value);
    }

    getLabel(item: FlaggerEntry) {
        return item.label;
    }

    isChecked(item: FlaggerEntry) {
        return this.data.value.indexOf(item.value) > -1;
    }

}

interface FlaggerEntryInterface {
    disabled?: boolean;
}

export class FlaggerEntry {
    constructor(value: string, label: string, params?: FlaggerEntryInterface) {
        this.value = value;
        this.label = label;
        if (params) {
            this.disabled = params.disabled;
        }
    }
    value: string;
    label: string;
    disabled: boolean;
}