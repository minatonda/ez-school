import { Prop, Component, Model, Watch, Vue } from 'vue-property-decorator';
import { TreeViewModel } from '../../model/server/tree-view.model';


interface Data {
    value: Array<any>;
}

@Component({
    template: require('./tree-view.html')
})
export class TreeViewComponent extends Vue {

    @Prop({ type: Array, default: () => [] })
    itens: Array<TreeViewModel<any>>;

    @Prop({ type: [String, Function], default: () => 'label' })
    label: any;

    @Prop({ type: Array, default: () => [] })
    value: Array<any>;

    data: Data = {
        value: new Array()
    };

    @Watch('value', { deep: true })
    onValueChanged(value) {
        this.data.value = value;
    }

    onChildrenChanged(value: Array<any>) {
        this.data.value = this.data.value.filter(x => this.itens.some(y => y.id === x));
        this.data.value = Array.prototype.concat(this.data.value, value);
        this.itens.forEach(x => {
            if (x.children && x.children.length && x.children.some(y => !!value.indexOf(y.id)) && x.childrenRequisite) {
                this.data.value.push(x.id);
            }
        });
        this.$emit('input', this.data.value);
        this.$emit('changed', this.data.value);
    }

    getLabel(item: TreeViewModel<any>) {
        if (typeof (this.label) === 'string') {
            return item[this.label];
        }
        else if (this.label) {
            return this.label(item);
        }
    }

    doToggleSelect(item: TreeViewModel<any>) {
        if (this.isSelected(item)) {
            this.doUnselect(item);
        }
        else {
            this.doSelect(item);
        }
    }

    doSelect(item: TreeViewModel<any>) {
        this.data.value = Array.prototype.concat(this.data.value, [item.id]);
        this.$emit('input', this.data.value);
        this.$emit('changed', this.data.value);
    }

    doUnselect(item: TreeViewModel<any>) {
        if (item.childrenRequisite) {
            this.data.value = this.data.value.filter(x => !item.children.some(y => y.id === x));
        }
        this.data.value = this.data.value.filter(x => x !== item.id);
        this.$emit('input', this.data.value);
        this.$emit('changed', this.data.value);
    }

    isSelected(item: TreeViewModel<any>) {
        return !!this.data.value.find(x => x === item.id);
    }


}