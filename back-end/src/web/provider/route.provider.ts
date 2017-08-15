import { Connection } from "typeorm";
import { ControllerRouteDefinition } from "../controller/controller-route.definition";
import { ControllerInterface } from "../controller/controller.interface";
import { UsuarioController } from "../controller/usuario/usuario.controller";

export class RouteProvider {
    
    public getRoutes(connection: Connection) {
        var _routesList = new Array < ControllerRouteDefinition > ();
        for (let controller of this._getControllers(connection)) {
            _routesList = _routesList.concat(controller.getRoutes());
        }
        return _routesList;
    }

    private _getControllers(connection: Connection): Array < ControllerInterface > {
        var _controllerList = new Array < ControllerInterface > ();
        _controllerList.push(new UsuarioController(connection));
        return _controllerList;
    }

}