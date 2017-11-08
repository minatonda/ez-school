import { UsuarioInfo } from '../usuario/usuario-info';
import { CategoriaProfissional } from '../categoria-profissional/categoria-profissional';

export class Professor {

    constructor() {
        this.categoriaProfissionais = new Array<CategoriaProfissional>();
    }
    
    id?: string;
    usuarioInfo: UsuarioInfo;
    categoriaProfissionais: Array<CategoriaProfissional>;
}