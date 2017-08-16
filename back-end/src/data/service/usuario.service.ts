import { Service } from "./service";
import { Usuario } from "../model/usuario";
import { Connection } from "typeorm";
import { UsuarioInfoService } from "./usuario-info.service";

export class UsuarioService extends Service < Usuario > {

    constructor(connection: Connection) {
        super(connection, new Usuario());
        this.usuarioInfoService = new UsuarioInfoService(connection);
    }

    private usuarioInfoService: UsuarioInfoService;

    public externalAdd = async(usuario: Usuario) => {
        let model = this.save(usuario);
        this.usuarioInfoService.save(usuario.usuarioInfo);
        return model;
    }

}