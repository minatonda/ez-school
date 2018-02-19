import { BaseError } from './base.error';

export class UIValidationError extends BaseError {

    constructor(message: string, object: any, errors: Array<string>) {
        super('Ui Validation Error', message);
        this.object = object;
        this.errors = errors;
    }
    
    object: any;
    errors: Array<string>;
}