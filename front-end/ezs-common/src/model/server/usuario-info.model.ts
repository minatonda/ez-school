import { CategoriaProfissionalModel } from './categoria-profissional.model';
import { BaseModel } from './base.model';
import { AreaInteresseModel } from './area-interesse.model';

export class UsuarioInfoModel extends BaseModel < string > {

    constructor() {
        super();
        this.areaInteresses = new Array < AreaInteresseModel > ();
        this.roles = new Array < string > ();
    }

    nome: string;
    dataNascimento: string;
    rg: string;
    cpf: string;
    email: string;
    areaInteresses: Array < AreaInteresseModel > ;
    roles: Array < string > ;

}