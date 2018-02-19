import { BaseError } from './base.error';
import { ErrorResponse } from './response.error';

export class RequestError extends BaseError {

    constructor(message: string, url: string, statusCode: number, response: ErrorResponse) {
        super('Request Error', message);
        this.url = url;
        this.statusCode = statusCode;
        this.errorResponse = response;
    }
    
    url: string;
    statusCode: number;
    errorResponse: ErrorResponse;
}