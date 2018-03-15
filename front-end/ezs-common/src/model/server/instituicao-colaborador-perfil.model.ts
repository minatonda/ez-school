import { BaseModel } from './base.model';
import { UsuarioModel } from './usuario.model';

export class InstituicaoColaboradorPerfilModel extends BaseModel < number > {

    nome: string;
    roles: Array < string > ;

}