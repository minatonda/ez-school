import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { AutenticacaoService } from '../../../common/service/autenticacao/autenticacao.service';
import { UsuarioAutenticacaoViewModel } from '../../../common/view-model/usuario/usuario-autenticacao.viewmodel';

@Component({
    template: require('./usuario-autenticacao.html')
})
export class UsuarioAutenticacaoComponent extends Vue {

    public model: UsuarioAutenticacaoViewModel = { senha: undefined, usuario: undefined };
    constructor() {
        super();
    }

    mounted() {

    }

    public autenticar() {
        AutenticacaoService.autenticar(this.model);
    }
}