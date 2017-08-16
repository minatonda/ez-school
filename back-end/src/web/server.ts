import * as bodyParser from "body-parser";
import * as cookieParser from "cookie-parser";
import * as express from "express";
import * as logger from "morgan";
import * as path from "path";
import { Connection } from "typeorm";
import { HttpMethod } from "../common/http/http-method";
import http = require("http");
import errorHandler = require("errorhandler");
import methodOverride = require("method-override");
import { Environment } from "../common/environment/environment";
import { RouteProvider } from "./provider/route.provider";
import { ControllerRouteDefinition } from "./controller/controller-route.definition";
import { ConnectionProvider } from "../data/provider/connection.provider";

export class Server {

    private _app: express.Application;
    private _port: number;
    private _server: http.Server;
    private _environment: Environment;
    private _routeProvider: RouteProvider;
    private _connection: Connection;

    constructor(environment: Environment, port: number, onListening: (...args: any[]) => void, onError: (...args: any[]) => void) {
        this._routeProvider = new RouteProvider();
        this._environment = environment;
        this._port = port;
        this._initApp(onListening, onError);
    }

    public getHttpServer() {
        return this._server;
    }

    private async _initApp(onListening: (...args: any[]) => void, onError: (...args: any[]) => void) {
        this._app = express();
        this._connection = await ConnectionProvider.getConnection(this._environment);
        this._configure();
        this._initServer(this._port, onListening, onError, );
    }

    private _initServer(port: number, onListening: (...args: any[]) => void, onError: (...args: any[]) => void) {
        this._server = http.createServer(this._app);
        this._server.listen(port);
        this._server.on("listening", onListening);
        this._server.on("error", onError);
    }

    private _configure() {
        this._app.use(express.static(path.join(__dirname, "public")));
        //configure pug
        //this._app.set("views", path.join(__dirname, "views"));
        //this._app.set("view engine", "pug");
        //use logger middlware
        this._app.use(logger("dev"));
        //use json form parser middlware
        this._app.use(bodyParser.json());
        //use query string parser middlware
        this._app.use(bodyParser.urlencoded({
            extended: true
        }));
        //use cookie parker middleware middlware
        this._app.use(cookieParser("SECRET_GOES_HERE"));
        //use override middlware
        this._app.use(methodOverride());
        //catch 404 and forward to error handler
        this._app.use(function (err: any, req: express.Request, res: express.Response, next: express.NextFunction) {
            err.status = 404;
            next(err);
        });
        //error handling
        this._app.use(errorHandler());

        this._setRoutes();
    }

    private _setCors() {
        this._app.use(function (req, res, next) {
            res.header("Access-Control-Allow-Origin", "*");
            res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
            next();
        });
    }

    private _setLocals() {

    }

    private _setRoutes() {
        var router = this._createRouter(this._routeProvider.getRoutes(this._connection));
        this._app.use('/', router);
    }

    private _createRouter(listRouteSpecification: Array < ControllerRouteDefinition > ) {

        var router = express.Router();
        for (let routeSpecification of listRouteSpecification) {
            switch (routeSpecification.httpMethod) {
                case (HttpMethod.POST):
                    {
                        router.post(routeSpecification.path, routeSpecification.controllerMethod);
                        break;
                    };
                case (HttpMethod.PUT):
                    {
                        router.put(routeSpecification.path, routeSpecification.controllerMethod);
                        break;
                    };
                case (HttpMethod.GET):
                    {
                        router.get(routeSpecification.path, routeSpecification.controllerMethod);
                        break;
                    };
                case (HttpMethod.DELETE):
                    {
                        router.delete(routeSpecification.path, routeSpecification.controllerMethod);
                        break;
                    };
            }
        }

        return router;
    }
}