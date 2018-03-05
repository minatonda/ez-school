import { UsuarioInfoModel } from './usuario-info.model';
import { BaseModel } from './base.model';

export class UsuarioModel extends BaseModel < string > {

    username: string;
    password: string;
    usuarioInfo: UsuarioInfoModel;

}