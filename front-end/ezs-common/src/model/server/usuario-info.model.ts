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

    nome: string;
    email: string;
    telefone: string;
    dataNascimento: string;
    rg: string;
    cpf: string;
    endereco: EnderecoModel;
    genero: string;
    estadoCivil: string;
    areaInteresses: Array<AreaInteresseModel>;
    roles: Array<string>;

}