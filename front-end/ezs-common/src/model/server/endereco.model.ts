import { BaseModel } from './base.model';

export class EnderecoModel extends BaseModel < string > {
    rua: string;
    numero: string;
    complemento: string;
    bairro: string;
    cidade: string;
    estado: string;
}