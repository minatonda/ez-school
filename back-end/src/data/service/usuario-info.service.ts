import { Service } from "./service";
import { UsuarioInfo } from "../model/usuario-info";
import { Connection } from "typeorm";

export class UsuarioInfoService extends Service<UsuarioInfo> {

    constructor(connection: Connection) {
        super(connection, new UsuarioInfo());
    }

}