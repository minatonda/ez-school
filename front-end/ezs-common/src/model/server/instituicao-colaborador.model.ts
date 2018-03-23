import { BaseModel } from './base.model';
import { UsuarioInfoModel } from './usuario-info.model';

export class InstituicaoColaboradorModel extends BaseModel<number> {

    constructor() {
        super();
        this.perfis = new Array<string>();
    }

    usuario: UsuarioInfoModel;
    perfis: Array<string>;

}