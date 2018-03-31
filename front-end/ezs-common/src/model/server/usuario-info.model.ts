import { BaseModel } from './base.model';
import { CategoriaProfissionalModel } from './categoria-profissional.model';
import { AreaInteresseModel } from './area-interesse.model';
import { EnderecoModel } from './endereco.model';

export class UsuarioInfoModel extends BaseModel<string> {

    constructor() {
        super();
        this.areaInteresses = new Array<AreaInteresseModel>();
        this.roles = new Array<string>();
        this.endereco = new EnderecoModel();
    }

    nome: string = null;
    email: string = null;
    telefone: string = null;
    dataNascimento: string = null;
    rg: string = null;
    cpf: string = null;
    endereco: EnderecoModel = null;
    genero: string = null;
    estadoCivil: string = null;
    areaInteresses: Array<AreaInteresseModel> = null;
    roles: Array<string> = null;

}