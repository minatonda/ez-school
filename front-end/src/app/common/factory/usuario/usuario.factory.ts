import { Factory } from '../factory';
import { UsuarioViewModel } from './usuario.view-model';

export class UsuarioFactory extends Factory {

    public static add(viewmodel: UsuarioViewModel) {
        return this.put(this.joinUrl('/usuario/external/add'), viewmodel);
    }

    public static async externalAdd(viewmodel: UsuarioViewModel) {
        let retorno = await this.put(this.joinUrl('/usuario/external/add'), viewmodel);
        return retorno as UsuarioViewModel;
    }

}