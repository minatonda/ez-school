import { Connection } from "typeorm";
import { UsuarioService } from "../../../data/service/usuario.service";
import { UsuarioAddExternalViewModelAdapter, UsuarioAddExternalViewModel } from "../../view-model/usuario/usuario-external-add.view-model";

export class UsuarioControllerService {

    private usuarioService: UsuarioService;
    private usuarioExternalViewModelAdapter: UsuarioAddExternalViewModelAdapter;
    constructor(connection: Connection) {
        this.usuarioService = new UsuarioService(connection);
        this.usuarioExternalViewModelAdapter = new UsuarioAddExternalViewModelAdapter();
    }

    public externalAdd = async(usuarioAddExternal: UsuarioAddExternalViewModel) => {
        let model = this.usuarioExternalViewModelAdapter.getModel(usuarioAddExternal);
        model = await this.usuarioService.externalAdd(model);
        return this.usuarioExternalViewModelAdapter.getViewModel(model);
    }

}