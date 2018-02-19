import { CategoriaProfissionalModel } from './categoria-profissional.model';
import { UsuarioInfoModel } from './usuario-info.model';

export class AlunoModel {

    constructor() {
        this.categoriaProfissionais = new Array<CategoriaProfissionalModel>();
        this.usuarioInfo = new UsuarioInfoModel();
    }

    id?: string;
    usuarioInfo: UsuarioInfoModel;
    categoriaProfissionais: Array<CategoriaProfissionalModel>;
}