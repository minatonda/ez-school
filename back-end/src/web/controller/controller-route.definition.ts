import { Request, Response } from "express";
import { HttpMethod } from "../../common/http/http-method";

export class ControllerRouteDefinition {
    constructor(path: string, controllerMethod: (req: Request, res: Response) => void, httpMethod: HttpMethod) {
        this.path = path;
        this.controllerMethod = controllerMethod;
        this.httpMethod = httpMethod;
    }
    path: string;
    controllerMethod: (req: Request, res: Response) => void;
    httpMethod: HttpMethod;
}