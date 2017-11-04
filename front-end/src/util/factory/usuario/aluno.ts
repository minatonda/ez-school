import { UsuarioInfo } from '../usuario/usuario-info';
import { CategoriaProfissional } from '../categoria-profissional/categoria-profissional';

export class Aluno {

    constructor() {
        this.categoriaProfissionais = new Array<CategoriaProfissional>();
    }

    id?: string;
    usuarioInfo: UsuarioInfo;
    categoriaProfissionais: Array<CategoriaProfissional>;
}