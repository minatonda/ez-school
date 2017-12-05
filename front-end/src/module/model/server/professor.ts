import { CategoriaProfissional } from './categoria-profissional';
import { UsuarioInfo } from './usuario-info';



export class Professor {

    constructor() {
        this.categoriaProfissionais = new Array<CategoriaProfissional>();
    }
    
    id?: string;
    usuarioInfo: UsuarioInfo;
    categoriaProfissionais: Array<CategoriaProfissional>;
}