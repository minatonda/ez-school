import { createConnection, Connection, ConnectionOptions } from "typeorm";
import { Environment } from "../../common/environment/environment";
import * as fs from "fs";

export class ConnectionProvider {

    public static async getConnection(environment: Environment): Promise < Connection > {
        return await createConnection(this.getConfig(environment));
    }

    public static getConfig(environment: Environment): ConnectionOptions {
        let _listConfig = JSON.parse(fs.readFileSync('./ormconfig.json', 'utf8')) as Array < ConnectionOptions > ;
        return _listConfig.find((config) => config.name == environment) || _listConfig.find((config) => config.name == "default");
    }

}