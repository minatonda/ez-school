import { Vue } from 'vue-property-decorator';
import { Prop, Component, Model, Watch } from 'vue-property-decorator';

interface UI {
    visible: boolean;
}

export interface DropDownItem {
    text: any;
    item: any;
}

enum RenderType {
    TOP_TO_BOTTOM = 'top-to-bottom',
    BOTTOM_TO_TOP = 'bottom-to-top',
    FULL_SCREEN = 'full-screen'
}

@Component({
    template: require('./gp-dropdown.html')
})
export class DropdownComponent extends Vue {

    ui: UI = {
        visible: false
    };

    @Prop([Boolean])
    value: any;

    @Prop([Array])
    itens: Array<DropDownItem>;

    onSelect(dropDownItem: DropDownItem) {
        if ((typeof dropDownItem.item) === 'string') {

        }
        else {
            dropDownItem.item();
        }
        this.$emit('select', dropDownItem);
        this.hide();
    }

    getLabelByDropDownItem(dropDownItem: DropDownItem) {
        if ((typeof dropDownItem.text) === 'string') {
            return dropDownItem.text;
        }
        else {
            return dropDownItem.text();
        }
    }

    toggle() {
        if (this.ui.visible) {
            this.hide();
        }
        else {
            this.show();
        }
    }

    show() {
        this.ui.visible = true;
        this.$emit('input', this.ui.visible);
    }

    hide() {
        this.ui.visible = false;
        this.$emit('input', this.ui.visible);
    }

    @Watch('value')
    onValueChange(val) {
        this.ui.visible = val;
    }

}