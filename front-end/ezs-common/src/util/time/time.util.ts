import moment, { Duration } from 'moment/moment';

export class Util {

    public getTimeFormatted = (timeString: string, outFormat: string, inFormat?: string) => {
        return moment(timeString, inFormat || 'HH:mm:ss').format(outFormat);
    }

    public getHumanized = (time: Duration, humanizeDefinitions: HumanizeDefinitions) => {

        let days = '';
        let hours = '';
        let minutes = '';

        let humanizedString = '';

        if (humanizeDefinitions.minuteRound) {
            if (humanizeDefinitions.minuteRoundCondition) {
                let passed = true;
                if (humanizeDefinitions.minuteRoundCondition.minDay !== undefined && time.days() * -1 < humanizeDefinitions.minuteRoundCondition.minDay) {
                    passed = false;
                }
                if (humanizeDefinitions.minuteRoundCondition.minHour !== undefined && time.hours() * -1 < humanizeDefinitions.minuteRoundCondition.minHour) {
                    passed = false;
                }
                if (humanizeDefinitions.minuteRoundCondition.minDay !== undefined && time.hours() * -1 < humanizeDefinitions.minuteRoundCondition.minDay) {
                    passed = false;
                }
                if (passed) {
                    time.add(this.getToRound(time.minutes(), humanizeDefinitions.minuteRound, true) * -1, 'minutes');
                }
            }
            else {
                time.add(this.getToRound(time.minutes(), humanizeDefinitions.minuteRound, true) * -1, 'minutes');
            }
        }
        switch (humanizeDefinitions.minute) {
            case (HumanizeDefinitionsType.ABREVIATE):
                {
                    minutes += this.humanizeMinute(time, true);
                    break;
                }
            case (HumanizeDefinitionsType.FULL):
            default:
                {
                    minutes += this.humanizeMinute(time);
                    break;
                }
        }

        if (humanizeDefinitions.hourRound) {
            let passed = true;
            if (humanizeDefinitions.hourRoundCondition.minDay !== undefined && time.days() * -1 < humanizeDefinitions.hourRoundCondition.minDay) {
                passed = false;
            }
            if (humanizeDefinitions.hourRoundCondition.minHour !== undefined && time.hours() * -1 < humanizeDefinitions.hourRoundCondition.minHour) {
                passed = false;
            }
            if (humanizeDefinitions.hourRoundCondition.minDay !== undefined && time.hours() * -1 < humanizeDefinitions.hourRoundCondition.minDay) {
                passed = false;
            }
            if (passed) {
                time.add(this.getToRound(time.hours(), humanizeDefinitions.hourRound, true) * -1, 'hours');
            }
            time.add(this.getToRound(time.hours(), humanizeDefinitions.hourRound, true) * -1, 'hours');
        }
        switch (humanizeDefinitions.hour) {
            case (HumanizeDefinitionsType.ABREVIATE):
                {
                    hours += this.humanizeHour(time, true);
                    break;
                }
            case (HumanizeDefinitionsType.FULL):
            default:
                {
                    hours += this.humanizeHour(time);
                    break;
                }
        }

        switch (humanizeDefinitions.day) {
            case (HumanizeDefinitionsType.ABREVIATE):
                {
                    days += this.humanizeDay(time, true);
                    break;
                }
            case (HumanizeDefinitionsType.FULL):
            default:
                {
                    days += this.humanizeDay(time);
                    break;
                }
        }

        if (humanizeDefinitions.hour === HumanizeDefinitionsType.NOT_SHOW && humanizeDefinitions.minute === HumanizeDefinitionsType.NOT_SHOW || humanizeDefinitions.day === HumanizeDefinitionsType.NOT_SHOW || !days || !hours) {
            humanizedString += days;
        }
        else {
            humanizedString += days + ' ';
        }

        if (humanizeDefinitions.minute === HumanizeDefinitionsType.NOT_SHOW || humanizeDefinitions.hour === HumanizeDefinitionsType.NOT_SHOW || !hours || !minutes) {
            humanizedString += hours;
        }
        else {
            humanizedString += hours + ' ';
        }

        humanizedString += minutes;

        return humanizedString;
    }

    public getPeriod = (hour) => {
        let res;
        if ((hour < 6)) {
            res = Period.MADRUGADA;
        }
        else if ((hour >= 6 && hour < 12)) {
            res = Period.MANHA;
        }
        else if ((hour >= 12 && hour < 18)) {
            res = Period.TARDE;
        }
        else if ((hour >= 18 && hour <= 24)) {
            res = Period.NOITE;
        }
        return res;
    }

    public getPeriodPreposition = (period: Period) => {
        switch (period) {
            case (Period.MANHA):
            case (Period.MADRUGADA):
                {
                    return 'de';
                }
            case (Period.NOITE):
            case (Period.TARDE):
                {
                    return 'á';
                }
        }
    }

    public getAverage = (listDuration: Array<Duration>, hasMoreThanOneResult) => {
        let maior = listDuration[0];
        let result = '';

        if (hasMoreThanOneResult) {
            listDuration.forEach(function (item) {
                if (!maior || item.asMilliseconds() > maior.asMilliseconds()) maior = item;
            });
        }
        if (maior.days()) {
            result += maior.days() + (TimeUnit.DIA + ', ');
        }
        if (maior.hours()) {
            result += maior.hours() + (TimeUnit.HORA + ' ');
        }
        if (maior.minutes()) {
            result += ' e ' + maior.minutes() + (TimeUnit.HORA + '');
        }
        return result;
    }

    private humanize = (time: Duration, textOnSingle: string, textOnPlural: string, timeUnit: string, abreviate?: boolean) => {
        let timeUnitVal = time.get(timeUnit as any);
        return (timeUnitVal) ? (timeUnitVal < 0 ? timeUnitVal * -1 : timeUnitVal) + (abreviate ? textOnSingle.substring(0, 1) : (' ' + (timeUnitVal === -1 ? textOnSingle : textOnPlural))) : '';
    }

    private humanizeDay = (time: Duration, abreviate?: boolean) => {
        return this.humanize(time, 'dia', 'dias', 'day', abreviate);
    }

    private humanizeHour = (time: Duration, abreviate?: boolean) => {
        return this.humanize(time, 'hora', 'horas', 'hour', abreviate);
    }

    private humanizeMinute = (time: Duration, abreviate?: boolean) => {
        return this.humanize(time, 'minuto', 'minutos', 'minute', abreviate);
    }

    private getToRound = (num: number, brk: number, onlyRoundUp?: boolean) => {
        let subtract = 0;
        let sum = 0;
        let internNum = num < 0 ? num * -1 : num;
        while (internNum % brk !== 0) {
            internNum += 1;
            sum += 1;
        }
        internNum = num < 0 ? num * -1 : num;
        while (internNum % brk !== 0) {
            internNum -= 1;
            subtract += 1;
        }
        return (sum > subtract && !onlyRoundUp) ? subtract * -1 : sum;
    }

}

export enum Period {
    MADRUGADA = 'madrugada',
    MANHA = 'manhã',
    TARDE = 'tarde',
    NOITE = 'noite'
}

export enum TimeUnit {
    D = 'd',
    H = 'h',
    M = 'm',
    DIA = 'dias',
    HORA = 'horas',
    MINUTE = 'minutos'
}

export enum HumanizeDefinitionsType {
    FULL = 'FULL',
    ABREVIATE = 'ABREVIATE',
    NOT_SHOW = 'NOT_SHOW',
}

export interface RoundCondition {
    minDay?: number;
    minHour?: number;
    minMinute?: number;
}

export interface HumanizeDefinitions {
    day?: HumanizeDefinitionsType;
    hour?: HumanizeDefinitionsType;
    minute?: HumanizeDefinitionsType;
    hourRound?: number;
    hourRoundCondition?: RoundCondition;
    minuteRound?: number;
    minuteRoundCondition?: RoundCondition;
}

export const TimeUtil = new Util();