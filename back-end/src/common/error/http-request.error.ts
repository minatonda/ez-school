import { BaseError } from "./base.error";

export class HttpRequestError extends BaseError {

    constructor(message: string, statusCode: number, url: string) {
        super('HttpRequestError', message);
        this.statusCode = statusCode;
    }
    url: string;
    statusCode: number;

}