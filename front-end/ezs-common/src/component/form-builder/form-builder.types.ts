import { FlaggerEntry } from '../flagger/flagger';

interface FormBuilderInputInterface {
    description: string;
    key: string;
}

interface FormBuilderInputSelectorInterface extends FormBuilderInputInterface {
    outputAs?: String;
    label?: any;
    query?: any;
    itens?: Array<any>;
    limit?: number;
}

interface FormBuilderInputDateCatcherInterface extends FormBuilderInputInterface {
    label?: string;
    modelAsString?: boolean;
    nullable?: boolean;
    onlyAutoComplete?: boolean;
    renderType?: string;
    limit?: number;
    startDate?: number;
    format?: string;
    modelAsDate?: boolean;
    disableText?: boolean;
}

interface FormBuilderInputFlaggerInterface extends FormBuilderInputInterface {
    entries: Array<FlaggerEntry>;
}

interface FormBuilderInputTextInterface extends FormBuilderInputInterface {
    mask?: string;
}

export class FormBuilderInput implements FormBuilderInputInterface {
    constructor(config: FormBuilderInputDateCatcherInterface) {
        for (let key in config) {
            this[key] = config[key];
        }
    }
    description: string;
    key: string;
}

export class FormBuilderInputSelector extends FormBuilderInput implements FormBuilderInputSelectorInterface {
    constructor(config: FormBuilderInputSelectorInterface) {
        super(config);
        for (let key in config) {
            this[key] = config[key];
        }
    }
    outputAs?: String;
    label?: any;
    query?: any;
    itens?: any[];
    limit?: number;
}

export class FormBuilderInputDateCatcher extends FormBuilderInput implements FormBuilderInputDateCatcherInterface {
    constructor(config: FormBuilderInputDateCatcherInterface) {
        super(config);
        for (let key in config) {
            this[key] = config[key];
        }
    }
    label?: string;
    modelAsString?: boolean;
    nullable?: boolean;
    onlyAutoComplete?: boolean;
    renderType?: string;
    inputClass?: string;
    limit?: number;
    startDate?: number;
    format?: string;
    modelAsDate?: boolean;
    disableText?: boolean;
    description: string;
    key: string;
}

export class FormBuilderInputFlagger extends FormBuilderInput implements FormBuilderInputFlaggerInterface {
    constructor(config: FormBuilderInputFlaggerInterface) {
        super(config);
        for (let key in config) {
            this[key] = config[key];
        }
    }
    entries: FlaggerEntry[];
    description: string;
    key: string;
}

export class FormBuilderInputText extends FormBuilderInput implements FormBuilderInputTextInterface {
    constructor(config: FormBuilderInputTextInterface) {
        super(config);
        for (let key in config) {
            this[key] = config[key];
        }
    }
    mask?: string;
    description: string;
    key: string;
}