import { CategoriaProfissionalModel } from './categoria-profissional.model';
import { BaseModel } from './base.model';
import { AreaInteresseModel } from './area-interesse.model';

export class UsuarioInfoModel extends BaseModel < string > {

    constructor() {
        super();
        this.areaInteresses = new Array < AreaInteresseModel > ();
    }
    
    nome: string;
    dataNascimento: string;
    rg: string;
    cpf: string;
    areaInteresses: Array < AreaInteresseModel > ;
    perfis: Array < string > ;

}