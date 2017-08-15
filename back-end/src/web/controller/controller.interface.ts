import { Response, Request } from "express";
import { ControllerRouteDefinition } from "./controller-route.definition";

export interface ControllerInterface {
    add(req: Request, res: Response): void;
    update(req: Request, res: Response): void;
    remove(req: Request, res: Response): void;
    all(req: Request, res: Response): void;
    detail(req: Request, res: Response): void;
    getRoutes(): Array < ControllerRouteDefinition > ;
}