import { BaseError } from './base.error';

export class RedirectError extends BaseError {

    constructor(message: string, url: string) {
        super('Redirect Errror', message);
        this.url = url;
    }
    
    url: string;
}
