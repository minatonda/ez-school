import { UsuarioInfoModel } from './usuario-info.model';

export class UsuarioModel {
    id?: string;
    username: string;
    password: string;
    usuarioInfo: UsuarioInfoModel;
}