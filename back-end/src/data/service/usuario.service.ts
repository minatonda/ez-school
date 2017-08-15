import { Service } from "./service";
import { Usuario } from "../model/usuario";
import { Connection } from "typeorm";

export class UsuarioService extends Service < Usuario > {

    constructor(connection: Connection) {
        super(connection, new Usuario());
    }

    public externalAdd = async(usuario: Usuario) => {
        let model = this.save(usuario);
        return model;
    }

}