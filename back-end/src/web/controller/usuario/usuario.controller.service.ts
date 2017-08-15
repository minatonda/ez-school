import { Connection } from "typeorm";
import { UsuarioExternalAddViewModel, UsuarioExternalViewModelAdapter } from "../../view-model/usuario/usuario-external-add.view-model";
import { UsuarioService } from "../../../data/service/usuario.service";

export class UsuarioControllerService {

    private usuarioService: UsuarioService;
    private usuarioExternalViewModelAdapter: UsuarioExternalViewModelAdapter;
    constructor(connection: Connection) {
        this.usuarioService = new UsuarioService(connection);

        this.usuarioExternalViewModelAdapter = new UsuarioExternalViewModelAdapter();
    }

    public externalAdd = async(usuarioExternalAdd: UsuarioExternalAddViewModel) => {
        let model = this.usuarioExternalViewModelAdapter.getModel(usuarioExternalAdd);
        model = await this.usuarioService.externalAdd(model);
        return this.usuarioExternalViewModelAdapter.getViewModel(model);
    }

}