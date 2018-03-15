import { Vue } from 'vue-property-decorator';
import { Prop, Component, Model, Watch } from 'vue-property-decorator';
import { FormBuilderInput } from './form-builder.types';
import { FormBuilderUtil } from './form-builder.util';

interface Data {
    values: any;
}

@Component({
    template: require('./form-builder.html')
})
export class FormBuilderComponent extends Vue {

    data: Data = {
        values: {}
    };

    @Prop({ type: Array, default: () => [] })
    inputs: Array<FormBuilderInput>;

    @Prop({ type: String, default: 'Salvar' })
    submitLabel: string;

    util = FormBuilderUtil;

    created() {

    }

    submitData(data: any) {
        this.$emit('submit-data', data);
    }


    getInputClass(inputs: Array<FormBuilderInput>, index: number) {
        if (inputs.length % 4 === 0 && index === inputs.length - 1) {
            return 'col-md-2';
        }
        return 'col-md-3';
    }

}