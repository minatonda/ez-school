import moment, { Duration, Moment } from 'moment';
import { HumanizeDefinitionsType, TimeUtil } from './../time/time.util';


export class Util {

    public getDateFormatted = (dateTimeString: string, outFormat: string, inFormat?: string) => {
        let momentDate = moment(dateTimeString, inFormat);
        return momentDate.isValid() ? momentDate.format(outFormat) : '';
    }

    public getDuration = (start: any, end: any) => {
        let first = moment(end);
        let second = moment(start);
        return moment.duration(first.diff(second));
    }

    public isAfter = (date1: Moment | Date | string, date2: Moment | Date | string) => {
        return moment(date1).format('YYYY-MM-DD') > moment(date2).format('YYYY-MM-DD');
    }

    public isAfterOrEqualToday = (date: Moment | Date | string) => {
        return moment(date).format('YYYY-MM-DD') >= moment().format('YYYY-MM-DD');
    }

    public isValidDateString = (date: string) => {
        return /^\d{4}\-[0-1]{1}[0-9]\-[0-3]{1}[0-9]{1}$/.test(date);
    }

    public getDurationHumanized = (start: string, end: string, full: boolean) => {
        if (full) {
            return this.getDurationHumanizedFull(start, end);
        }
        else {
            return this.getDurationHumanizedAbreviate(start, end);
        }
    }

    public getDurationHumanizedAbreviate = (start: string, end: string) => {
        return TimeUtil.getHumanized(this.getDuration(start, end), {
            day: HumanizeDefinitionsType.ABREVIATE,
            hour: HumanizeDefinitionsType.ABREVIATE,
            minute: HumanizeDefinitionsType.ABREVIATE,
            minuteRound: 60,
            minuteRoundCondition: {
                minDay: 1
            }
        });
    }

    public getDurationHumanizedFull = (start: string, end: string) => {
        let departure = moment(start);
        let arrival = moment(end);
        return TimeUtil.getHumanized(moment.duration(departure.diff(arrival)), {
            day: HumanizeDefinitionsType.FULL,
            hour: HumanizeDefinitionsType.FULL,
            minute: HumanizeDefinitionsType.FULL,
            minuteRound: 60,
            minuteRoundCondition: {
                minDay: 1
            }
        });
    }

}

export const DateUtil = new Util();