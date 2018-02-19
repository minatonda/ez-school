import { Vue } from 'vue-property-decorator';
import { Prop, Component } from 'vue-property-decorator/lib/vue-property-decorator';

@Component({
    template: require('./loader-compact.html')
})
export class LoaderCompactComponent extends Vue {

    @Prop([Boolean])
    show;

    constructor() {
        super();
    }

}