import moment from 'moment';

class Util {

    public validCPF = (value: string) => {
        let cpf = value.replace(/[\D]/g, '');
        if (cpf.length !== 11 || /^([0-9])\1*$/.test(cpf)) {
            return false;
        }
        else {
            let digits = cpf.substring(9);
            let first = this.validCPFGetFirstDigit(cpf);
            let second = this.validCPFGetSecondDigit(cpf);
            let er = first + '' + second + '';
            return digits === er;
        }
    }

    public validBirthDayElder = (value: string, minAge: number, inFormat?: string) => {
        let momentBirthDay = moment(value, inFormat);
        let momentNow = moment();

        let diff = momentNow.diff(momentBirthDay);
        let duration = moment.duration(diff);

        return duration.asYears() > minAge;
    }

    private validCPFGetFirstDigit = (value: string) => {
        let mult = 10, sum = 0;
        for (let i = 0; i < 9; i++) {
            sum += mult * parseInt(value.charAt(i) + '');
            mult--;
        }
        let rest = 11 - sum % 11;
        if (rest >= 10) rest = 0;
        return rest;
    }

    private validCPFGetSecondDigit = (value: string) => {
        let mult = 11, sum = 0;
        for (let i = 0; i < 10; i++) {
            sum += mult * parseInt(value.charAt(i) + '');
            mult--;
        }
        let rest = 11 - sum % 11;
        if (rest >= 10) rest = 0;
        return rest;
    }



}

export const ValidationUtil = new Util();