import { VueRouter } from 'vue-router/types/router';
import { Vue } from 'vue/types/vue';
import { HomeComponent } from '../../../components/home/home';
import { UsuarioAutenticacaoComponent } from '../../../components/usuario/usuario-autenticacao/usuario-autenticacao';
import { UsuarioAddExternalComponent } from "../../../components/usuario/usuario-add-external/usuario-add-external";

export enum RoutePath {
    ROOT = '/',
        ABOUT = '/about',
        USUARIO_AUTENTICACAO = '/usuario/autenticacao',
        USUARIO_ADD_EXTERNAL = '/usuario/cadastro'
}

export const RouterConfig = {
    routes: [
        { path: RoutePath.ROOT, component: HomeComponent },
        { path: RoutePath.USUARIO_AUTENTICACAO, component: UsuarioAutenticacaoComponent },
        { path: RoutePath.USUARIO_ADD_EXTERNAL, component: UsuarioAddExternalComponent }
    ]
};