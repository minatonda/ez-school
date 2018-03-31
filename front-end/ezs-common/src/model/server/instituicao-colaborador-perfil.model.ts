import { BaseModel } from './base.model';
import { UsuarioModel } from './usuario.model';

export class InstituicaoColaboradorPerfilModel extends BaseModel<number> {

    constructor() {
        super();
        this.roles = new Array<string>();
    }

    nome: string = null;
    roles: Array<string> = null;

}