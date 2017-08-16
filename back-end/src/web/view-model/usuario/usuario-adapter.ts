import { Usuario } from "../../../data/model/usuario";
import { UsuarioInfoViewModel } from "./usuario-info.view-model";
import { UsuarioInfo } from "../../../data/model/usuario-info";
import { ShortViewModel } from "../short/short.viewmodel";
import { UsuarioViewModel } from "./usuario.view-model";

export class UsuarioInfoViewModelAdapter {
    public static getViewModel(model: UsuarioInfo): UsuarioInfoViewModel {
        let viewModel = new UsuarioInfoViewModel();
        viewModel.id = model.id;
        viewModel.nome = model.nome;
        viewModel.dataNascimento = model.dataNascimento;
        viewModel.rg = model.rg;
        viewModel.cpf = model.cpf;
        return viewModel;
    }
    public static getModel(viewModel: UsuarioInfoViewModel): UsuarioInfo {
        let model = new UsuarioInfo();
        model.id = viewModel.id;
        model.nome = viewModel.nome;
        model.nome = viewModel.nome;
        model.dataNascimento = viewModel.dataNascimento;
        model.rg = viewModel.rg;
        model.cpf = viewModel.cpf;
        return model;
    }
    public static getShortViewModel(model: UsuarioInfo): ShortViewModel {
        let viewModel = new ShortViewModel();
        viewModel.id = model.id;
        viewModel.label = model.nome;
        return viewModel;
    }
}

export class UsuarioViewModelAdapter {

    public static getViewModel(model: Usuario): UsuarioViewModel {
        let viewModel = new UsuarioViewModel();
        viewModel.nomeusuario = model.nomeusuario;
        viewModel.senha = model.senha;
        return viewModel;
    }
    public static getModel(viewModel: UsuarioViewModel): Usuario {
        let usuarioInfo = new UsuarioInfo();
        usuarioInfo.nome = viewModel.senha;
        let model = new Usuario();
        model.nomeusuario = viewModel.nomeusuario;
        model.senha = viewModel.senha;
        model.usuarioInfo = UsuarioInfoViewModelAdapter.getModel(viewModel.usuarioInfo);
        return model;
    }
    public static getShortViewModel(model: Usuario): ShortViewModel {
        let viewModel = new ShortViewModel();
        viewModel.id = model.id;
        viewModel.label = model.nomeusuario;
        return viewModel;
    }

}