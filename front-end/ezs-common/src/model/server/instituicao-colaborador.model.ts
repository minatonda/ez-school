import { BaseModel } from './base.model';
import { UsuarioInfoModel } from './usuario-info.model';

export class InstituicaoColaboradorModel extends BaseModel<number> {

    constructor() {
        super();
        this.perfis = new Array<string>();
    }

    usuario: UsuarioInfoModel = null;
    perfis: Array<string> = null;

}