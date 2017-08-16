import { Connection } from "typeorm";
import { Request, Response } from "express";
import { HttpMethod } from "../../../common/http/http-method";
import { UsuarioControllerService } from "./usuario.controller.service";
import { Controller } from "../controller";
import { ControllerInterface } from "../controller.interface";
import { ControllerRouteDefinition } from "../controller-route.definition";
import { UsuarioViewModel } from "../../view-model/usuario/usuario.view-model";

export class UsuarioController extends Controller implements ControllerInterface {

    private _usuarioControllerService: UsuarioControllerService;

    constructor(connection: Connection) {
        super('Usuario');
        this._usuarioControllerService = new UsuarioControllerService(connection);
    }

    public async add(req: Request, res: Response) {
        try {
            var _retorno = {};
            res.send(_retorno);
        }
        catch (error) {
            this._resolveError(req, res, error);
        }
    }
    public async update(req: Request, res: Response) {
        try {
            var _retorno = {};
            res.send(_retorno);
        }
        catch (error) {
            this._resolveError(req, res, error);
        }
    }
    public async remove(req: Request, res: Response) {
        throw new Error("Method not implemented.");
    }
    public async all(req: Request, res: Response) {
        throw new Error("Method not implemented.");
    }
    public async detail(req: Request, res: Response) {
        throw new Error("Method not implemented.");
    }

    public externalAdd = async(req: Request, res: Response) => {
        try {
            let viewModel = req.body as UsuarioViewModel;
            let retorno = await this._usuarioControllerService.externalAdd(viewModel);
            res.send(retorno);
        }
        catch (error) {
            this._resolveError(req, res, error);
        }
    }

    public getRoutes(): Array < ControllerRouteDefinition > {
        return [
            new ControllerRouteDefinition('/usuario/external/add', this.externalAdd, HttpMethod.PUT)
        ];
    }

}