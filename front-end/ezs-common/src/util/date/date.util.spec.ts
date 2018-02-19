import { DateUtil } from './date.util';
import { expect } from 'chai';
import moment from 'moment';

describe('Date Travel Util', () => {

    before(async () => {

    });

    it('getDateFormatted - should return formated with default in format', async () => {
        expect(DateUtil.getDateFormatted('2017-12-27 20:00:00', 'DD/MM/YYYY HH:mm:ss')).to.equal('27/12/2017 20:00:00');
    });

    it('getDuration - should return duration between two dates', async () => {
        expect(DateUtil.getDuration('2017-12-27 11:00:00', '2017-12-27 12:15:00').asSeconds()).to.equal(4500);
    });

    it('isAfter - should return true', async () => {
        expect(DateUtil.isAfter('2017-12-28 11:00:00', '2017-12-27 11:00:00')).to.equal(true);
    });

    it('isAfter - should return false', async () => {
        expect(DateUtil.isAfter('2017-12-27 11:00:00', '2017-12-28 11:00:00')).to.equal(false);
    });

    it('isAfterOrEqualToday - should valid is today', async () => {
        expect(DateUtil.isAfterOrEqualToday(moment())).to.equal(true);
    });

    it('isAfterOrEqualToday - should valid is after today', async () => {
        expect(DateUtil.isAfterOrEqualToday(moment().add(1, 'day').format('YYYY-MM-DD hh:mm:ss'))).to.equal(true);
    });

    it('isAfterOrEqualToday - should return false', async () => {
        expect(DateUtil.isAfterOrEqualToday(moment().add(-1, 'day').format('YYYY-MM-DD hh:mm:ss'))).to.equal(false);
    });

    after(async () => {

    });

});