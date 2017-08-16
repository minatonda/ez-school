import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { UsuarioAddExternalViewModelAdapter } from "../../../common/view-model/usuario/usuario-external-add.viewmodel";

@Component({
    template: require('./usuario-add-external.html')
})
export class UsuarioAddExternalComponent extends Vue {

    constructor() {
        super();
    }

    model = new UsuarioAddExternalViewModelAdapter();

    mounted() {

    }
}