import { Repository, Connection } from "typeorm";

export class Service < T > {

    constructor(connection: Connection, classType: T) {
        this._repository = connection.getRepository(classType.constructor.name);
    }
    protected _repository: Repository < T > ;

    public async save(model: T): Promise < T > {
        return this._repository.save(model);
    }

    public async remove(model: T) {
        this._repository.remove(model);
    }

    public async find(id: number): Promise < T > {
        return await this._repository.findOneById(id);
    }

}