import { BaseModel } from './base.model';
import { CategoriaProfissionalModel } from './categoria-profissional.model';

export class AreaInteresseModel extends BaseModel < number > {
    categoriaProfissional: CategoriaProfissionalModel;
}