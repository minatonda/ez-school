import { BaseError } from './base.error';

export class NetworkError extends BaseError {

    constructor(message: string, url: string) {
        super('Network Errror', message);
        this.url = url;
    }
    
    url: string;
}