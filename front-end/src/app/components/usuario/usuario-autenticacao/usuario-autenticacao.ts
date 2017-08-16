import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { AutenticacaoService } from '../../../common/service/autenticacao/autenticacao.service';
import { UsuarioAutenticacaoViewModel } from '../../../common/factory/usuario/usuario-autenticacao.view-model';

@Component({
    template: require('./usuario-autenticacao.html')
})
export class UsuarioAutenticacaoComponent extends Vue {

    public model = new UsuarioAutenticacaoViewModel();
    constructor() {
        super();
    }

    mounted() {

    }

    public autenticar() {
        AutenticacaoService.autenticar(this.model);
    }
}