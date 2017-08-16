import { Connection } from "typeorm";
import { UsuarioService } from "../../../data/service/usuario.service";
import { UsuarioViewModel } from "../../view-model/usuario/usuario.view-model";
import { UsuarioViewModelAdapter } from "../../view-model/usuario/usuario-adapter";

export class UsuarioControllerService {

    private usuarioService: UsuarioService;
    constructor(connection: Connection) {
        this.usuarioService = new UsuarioService(connection);
    }

    public externalAdd = async(usuarioAddExternal: UsuarioViewModel) => {
        let model = UsuarioViewModelAdapter.getModel(usuarioAddExternal);
        model = await this.usuarioService.externalAdd(model);
        return UsuarioViewModelAdapter.getViewModel(model);
    }

}