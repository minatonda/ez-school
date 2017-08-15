import { Environment } from "./common/environment/environment";
import { Server } from "./web/server";

var _httpPort = parseInt(process.env.PORT) || 8000;

try {
    var _server = new Server(Environment.LOCAL, _httpPort, onListening, onError);
    console.log("Aplicação iniciada");
}
catch (error) {
    console.error("Erro ao iniciar a aplicação");
    console.error(error);
}

function onListening() {
    var addr = _server.getHttpServer().address();
    var bind = typeof addr === "string" ? "pipe " + addr : "port " + addr.port;
    var _debug = require("debug")("express:server");
    _debug("Listening on " + bind);
}

function onError(error: any) {
    if (error.syscall !== "listen") {
        throw error;
    }

    var bind = typeof _httpPort === "string" ?
        "Pipe " + _httpPort :
        "Port " + _httpPort;

    switch (error.code) {
    case ("EACCES"):
        {
            console.error(bind + " requires elevated privileges");
            process.exit(1);
            break;
        }
    case ("EADDRINUSE"):
        {
            console.error(bind + " is already in use");
            process.exit(1);
            break;
        }
    default:
        {
            throw error;
        }
    }
}

function normalizePort(val: any) {
    var port = parseInt(val, 10);
    if (isNaN(port)) {
        return val;
    }
    if (port >= 0) {
        return port;
    }
    return false;
}