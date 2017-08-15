import { ViewModelAdapterInterface } from "../view-model-adapter.interface";
import { ShortViewModel } from "../short/short.viewmodel";
import { Usuario } from "../../../data/model/usuario"; 
import { UsuarioInfo } from "../../../data/model/usuario-info";

export class UsuarioExternalAddViewModel {
    nome: string;
    nomeusuario: string;
    senha: string;
}

export class UsuarioExternalViewModelAdapter implements ViewModelAdapterInterface < Usuario, UsuarioExternalAddViewModel > {

    getViewModel(model: Usuario): UsuarioExternalAddViewModel {
        let viewModel = new UsuarioExternalAddViewModel();
        viewModel.nome = model.usuarioInfo.nome;
        viewModel.nomeusuario = model.nomeusuario;
        viewModel.senha = model.senha;
        return viewModel;
    }
    getModel(viewModel: UsuarioExternalAddViewModel): Usuario {
        let usuarioInfo = new UsuarioInfo();
        usuarioInfo.nome = viewModel.senha;

        let model = new Usuario();
        model.nomeusuario = viewModel.nomeusuario;
        model.senha = viewModel.senha;
        model.usuarioInfo = usuarioInfo;

        return model;
    }
    getShortViewModel(model: Usuario): ShortViewModel {
        throw new Error("Method not implemented.");
    }

}