import { BaseError } from './base.error';

export class RequestError extends BaseError {
    constructor(message: string, url: string, statusCode: number) {
        super('Request Error', message);
        this.url = url;
        this.statusCode = statusCode;
    }
    url: string;
    statusCode: number;
}