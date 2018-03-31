import { UsuarioInfoModel } from './usuario-info.model';
import { BaseModel } from './base.model';

export class UsuarioModel extends BaseModel < string > {

    username: string = null;
    password: string = null;
    usuarioInfo: UsuarioInfoModel = null;

}