import { ILogger } from './logger.interface';

export class Logger implements ILogger {

    info(msg: any) {
        console.info(msg);
    }

    warn(msg: any) {
        console.warn(msg);
    }

    error(msg: any) {
        console.error(msg);
    }

}