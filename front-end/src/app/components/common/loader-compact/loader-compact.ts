import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { Prop } from 'vue-property-decorator/lib/vue-property-decorator';

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