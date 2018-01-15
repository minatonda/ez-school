import { AreaInteresse } from './area-interesse';
import { UsuarioInfo } from './usuario-info';

export class Aluno {

    constructor() {
        this.areaInteresses = new Array<AreaInteresse>();
    }

    id?: string;
    usuarioInfo: UsuarioInfo;
    areaInteresses: Array<AreaInteresse>;
}