import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { UsuarioFactory } from '../../../common/factory/usuario/usuario.factory';
import { UsuarioViewModel } from '../../../common/factory/usuario/usuario.view-model';
import { UsuarioInfoViewModel } from '../../../common/factory/usuario/usuario-info.view-model';

@Component({
    template: require('./usuario-add-external.html')
})
export class UsuarioAddExternalComponent extends Vue {

    constructor() {
        super();
        this.model = new UsuarioViewModel();
        this.model.usuarioInfo = new UsuarioInfoViewModel();
    }

    public model: UsuarioViewModel;

    public mounted() {

    }

    public async save() {
        let usuarioViewModel = await UsuarioFactory.externalAdd(this.model);
        alert(usuarioViewModel.nomeusuario);
    }

}