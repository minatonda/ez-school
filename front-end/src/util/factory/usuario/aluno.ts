import { UsuarioInfo } from '../usuario/usuario-info';
import { CategoriaProfissional } from '../categoria-profissional/categoria-profissional';

export class Aluno {
    id?: string;
    usuarioInfo: UsuarioInfo;
    categoriaprossional: Array<CategoriaProfissional>;
}