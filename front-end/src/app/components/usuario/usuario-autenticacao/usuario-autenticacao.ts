import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { AutenticacaoService } from '../../../common/service/autenticacao/autenticacao.service';

@Component({
    template: require('./usuario-autenticacao.html')
})
export class UsuarioAutenticacaoComponent extends Vue {

    public model = new Object();
    constructor() {
        super();
    }

    mounted() {

    }

    public autenticar() {
        AutenticacaoService.autenticar(this.model);
    }
}