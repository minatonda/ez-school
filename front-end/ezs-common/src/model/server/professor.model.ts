import { CategoriaProfissionalModel } from './categoria-profissional.model';
import { UsuarioInfoModel } from './usuario-info.model';



export class ProfessorModel {

    constructor() {
        this.categoriaProfissionais = new Array<CategoriaProfissionalModel>();
    }
    
    id?: string;
    usuarioInfo: UsuarioInfoModel;
    categoriaProfissionais: Array<CategoriaProfissionalModel>;
}