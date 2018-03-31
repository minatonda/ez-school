import { BaseModel } from './base.model';

export class EnderecoModel extends BaseModel < string > {
    rua: string = null;
    numero: string = null;
    complemento: string = null;
    bairro: string = null;
    cidade: string = null;
    estado: string = null;
}