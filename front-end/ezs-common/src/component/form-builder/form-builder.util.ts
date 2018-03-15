import { FormBuilderInput, FormBuilderInputSelector, FormBuilderInputDateCatcher, FormBuilderInputFlagger, FormBuilderInputText } from './form-builder.types';

class Util {

    isFormBuilderInputSelector(input: FormBuilderInput) {
        return input instanceof FormBuilderInputSelector;
    }

    isFormBuilderInputDateCatcher(input: FormBuilderInput) {
        return input instanceof FormBuilderInputDateCatcher;
    }

    isFormBuilderInputFlagger(input: FormBuilderInput) {
        return input instanceof FormBuilderInputFlagger;
    }

    isFormBuilderInputText(input: FormBuilderInput) {
        return input instanceof FormBuilderInputText;
    }

}

export const FormBuilderUtil = new Util();