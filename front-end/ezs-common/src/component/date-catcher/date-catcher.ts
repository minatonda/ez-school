import { Vue } from 'vue-property-decorator';
import { Prop, Component, Model, Watch } from 'vue-property-decorator';
import { fail } from 'assert';
import { DateUtil } from '../../util/date/date.util';
import { TimeUtil } from '../../util//time/time.util';
import { NumberUtil } from '../../util//number/number.util';
import { ArrayUtil } from '../../util//array/array.util';
import moment, { Moment, months } from 'moment';

interface UI {
    value: any;
    enabled: boolean;
    canDisable: boolean;
    dateMain: moment.Moment;
    viewType: ViewType;
    containerStyle: any;
    text: string;
}

enum RenderType {
    TOP_TO_BOTTOM = 'top-to-bottom',
    BOTTOM_TO_TOP = 'bottom-to-top',
    FULL_SCREEN = 'full-screen'
}

enum ViewType {
    DAY = 'DAY',
    MONTH = 'MONTH',
    YEAR = 'YEAR'
}

@Component({
    template: require('./date-catcher.html'),
    filters: Object.assign({}, DateUtil, TimeUtil, NumberUtil) as any,
})
export class DateCatcherComponent extends Vue {

    ui: UI = {
        value: undefined,
        enabled: false,
        canDisable: true,
        dateMain: undefined,
        viewType: ViewType.DAY,
        containerStyle: {},
        text: undefined
    };

    @Prop([String, Date])
    value: string | Date;

    @Prop({ type: String, default: 'YYYY-MM-DD' })
    label: any;

    @Prop({ type: Boolean, default: false })
    modelAsString: boolean;

    @Prop({ type: Boolean, default: false })
    nullable: boolean;

    @Prop({ type: Boolean, default: false })
    onlyAutoComplete: boolean;

    @Prop({ type: String, default: 'top-to-bottom' })
    renderType: string;

    @Prop(String)
    name: string;

    @Prop(String)
    inputClass: string;

    @Prop(String)
    placeholder: string;

    @Prop({ type: Number, default: 10 })
    limit: number;

    @Prop({ type: Date, default: () => new Date() })
    startDate: number;

    @Prop({ type: String, default: () => 'YYYY-MM-DD' })
    format: string;

    @Prop({ type: Boolean, default: () => false })
    modelAsDate: boolean;

    @Prop({ type: Boolean, default: () => false })
    disableText: boolean;

    created() {
        this.select(this.value ? moment(this.value) : undefined);
    }

    @Watch('value')
    onValueChange(val) {
        if (!(val instanceof Date) || (val instanceof Date && !moment(val).isSame(moment(this.ui.value)))) {
            this.select(val ? moment(val) : undefined);
        }
    }

    async onTextChanged(val) {
        this.select(val ? this.dateFromText(val) : undefined);
    }

    dateFromText(text: string) {
        let dayStart = undefined;
        let dayEnd = undefined;

        let monthStart = undefined;
        let monthEnd = undefined;

        let yearStart = undefined;
        let yearEnd = undefined;

        let count = 0;

        while (count <= text.length) {
            if (this.label[count] === 'D') {
                if (dayStart === undefined) {
                    dayStart = count;
                    dayEnd = count;
                }
                else {
                    dayEnd++;
                }
            }
            else if (this.label[count] === 'M') {
                if (monthStart === undefined) {
                    monthStart = count;
                    monthEnd = count;
                }
                else {
                    monthEnd++;
                }
            }
            else if (this.label[count] === 'Y') {
                if (yearStart === undefined) {
                    yearStart = count;
                    yearEnd = count;
                }
                else {
                    yearEnd++;
                }
            }
            count++;
        }

        let day = text.substring(dayStart, dayEnd + 1) || moment(this.ui.dateMain.toDate()).format('DD');
        let month = text.substring(monthStart, monthEnd + 1) || moment(this.ui.dateMain.toDate()).format('MM');
        let year = text.substring(yearStart, yearEnd + 1) || moment(this.ui.dateMain.toDate()).format('YYYY');

        let mmnt = moment();
        mmnt.date(parseInt(day)).month(parseInt(month) - 1).year(parseInt(year));

        return mmnt;
    }

    onTextFieldBlur(val) {
        if (val) {
            this.ui.text = this.dateFromText(val).format(this.label);
        }
        if (this.canDisable()) {
            this.disable();
        }
    }

    enable() {
        this.ui.enabled = true;
        setImmediate(() => {
            this.doEnableContainerPositionFix();
        });
    }

    disable() {
        this.ui.enabled = false;
        setImmediate(() => {
            this.doDisableContainerPositionFix();
        });
    }

    enableDisable() {
        this.ui.canDisable = true;
    }

    disableDisable() {
        this.ui.canDisable = false;
    }

    doIncreaseDate() {
        switch (this.ui.viewType) {
            case (ViewType.DAY): {
                this.doIncreaseMonth();
                break;
            }
            case (ViewType.MONTH): {
                this.doIncreaseYear();
                break;
            }
            case (ViewType.YEAR): {
                this.doIncreaseDecade();
                break;
            }
        }
    }

    doDecreaseDate() {
        switch (this.ui.viewType) {
            case (ViewType.DAY): {
                this.doDecreaseMonth();
                break;
            }
            case (ViewType.MONTH): {
                this.doDecreaseYear();
                break;
            }
            case (ViewType.YEAR): {
                this.doDecreaseDecade();
                break;
            }
        }
    }

    doIncreaseMonth() {
        this.ui.dateMain.add(1, 'month');
        this.select(this.ui.dateMain);
        this.$forceUpdate();
    }

    doDecreaseMonth() {
        this.ui.dateMain.add(-1, 'month');
        this.select(this.ui.dateMain);
        this.$forceUpdate();
    }

    doIncreaseYear() {
        this.ui.dateMain.add(1, 'year');
        this.select(this.ui.dateMain);
        this.$forceUpdate();
    }

    doDecreaseYear() {
        this.ui.dateMain.add(-1, 'year');
        this.select(this.ui.dateMain);
        this.$forceUpdate();
    }

    doIncreaseDecade() {
        this.ui.dateMain.add(10, 'year');
        this.select(this.ui.dateMain);
        this.$forceUpdate();
    }

    doDecreaseDecade() {
        this.ui.dateMain.add(-10, 'year');
        this.select(this.ui.dateMain);
        this.$forceUpdate();
    }

    doEnableContainerPositionFix() {
        let element = this.$el.querySelector('.date-catcher-calendar');
        let widthscreen = screen.width;
        if (element.getBoundingClientRect().right > screen.width) {
            this.ui.containerStyle = { 'margin-left': `${this.$el.getBoundingClientRect().right - element.getBoundingClientRect().right}px` };
        }
    }

    doDisableContainerPositionFix() {
        this.ui.containerStyle = {};
    }

    select(date: moment.Moment) {
        this.setInternalValue(date);
    }

    isEnabled() {
        return this.ui.enabled;
    }

    isNullable() {
        return this.nullable;
    }

    isAnyItemSelected() {
        return !!this.ui.value;
    }

    isDateSelected(date: moment.Moment) {
        if (this.modelAsDate) {
            return date.format(this.format) === moment(this.ui.value).format(this.format);
        }
        else {
            return date.format(this.format) === this.ui.value;
        }
    }

    isDateToday(date: moment.Moment) {
        return date.format(this.format) === moment().format(this.format);
    }

    isDateOutOfCurrentMonth(date: moment.Moment) {
        return date.format('YYYY/MM') !== moment(this.ui.dateMain).format('YYYY/MM');
    }

    canDeselect() {
        return this.isNullable() && this.isAnyItemSelected();
    }

    canDisable() {
        return this.ui.canDisable;
    }

    setInternalValue(item: moment.Moment) {
        if (item && this.modelAsDate) {
            item.second(0);
            item.minute(0);
            item.hour(0);
            item.millisecond(0);
            this.ui.value = item.toDate();
            this.ui.dateMain = item;
        }
        else if (item) {
            this.ui.value = item.format(this.format);
            this.ui.dateMain = item;
        }
        else {
            let standardItem = moment(this.startDate, this.format);
            this.ui.dateMain = standardItem;
            this.ui.value = null;
        }
        this.$emit('change', this.ui.value);
        this.$emit('input', this.ui.value);
    }

    setViewType(viewType: ViewType) {
        this.ui.viewType = viewType;
    }

    isViewType(viewType: ViewType) {
        return this.ui.viewType === viewType;
    }

    getMask(format: string) {
        format = format.replace(/D/g, '#');
        format = format.replace(/M/g, '#');
        format = format.replace(/Y/g, '#');
        return format;
    }

    getMonthName(date: moment.Moment) {
        return date.format('MMMM');
    }

    getYear(date: moment.Moment) {
        return date.format('YYYY');
    }

    getFirstDateOfMonthToShow(startDate: moment.Moment) {
        let date = moment(`${startDate.format('YYYY')}-${startDate.format('MM')}-01`);
        while (date.weekday() > 0) {
            date.add(-1, 'day');
        }
        return date;
    }

    getFirstMonthOfYearToShow(startDate: moment.Moment) {
        let date = moment(`${startDate.format('YYYY')}-${startDate.format('01')}-01`);
        return date;
    }

    getFirstYearOfDecadeToShow(startDate: moment.Moment) {
        let year = parseInt(startDate.format('YYYY'));
        while (year % 10 !== 0) {
            year--;
        }
        let date = moment(`${year}-${startDate.format('01')}-01`);
        return date;
    }

    getDatesToShow(startDate: moment.Moment) {
        let dateToRun = moment(startDate);
        let dates = new Array<Moment>();
        for (let x = 0; x <= 35; x++) {
            dates.push(moment(dateToRun));
            dateToRun.add(1, 'day');
        }
        return dates;
    }

    getMonthsToShow(startDate: moment.Moment) {
        let dateToRun = moment(startDate);
        let dates = new Array<Moment>();
        for (let x = 0; x <= 12; x++) {
            dates.push(moment(dateToRun));
            dateToRun.add(1, 'month');
        }
        return dates;
    }

    getYearsToShow(startDate: moment.Moment) {
        let dateToRun = moment(startDate);
        let dates = new Array<Moment>();
        for (let x = 0; x <= 10; x++) {
            dates.push(moment(dateToRun));
            dateToRun.add(1, 'year');
        }
        return dates;
    }

    getDates() {
        return this.getDatesToShow(this.getFirstDateOfMonthToShow(moment(this.ui.dateMain)));
    }

    getMonths() {
        return this.getMonthsToShow(this.getFirstMonthOfYearToShow(moment(this.ui.dateMain)));
    }

    getYears() {
        return this.getYearsToShow(this.getFirstYearOfDecadeToShow(moment(this.ui.dateMain)));
    }

    getPlaceholder() {
        if (this.ui.value) {
            return this.ui.dateMain.format(this.label);
        }
        else {
            return this.placeholder;
        }
    }

    getListClass() {
        let classObject = {};
        classObject['top-to-bottom'] = this.renderType === RenderType.TOP_TO_BOTTOM;
        classObject['bottom-to-top'] = this.renderType === RenderType.BOTTOM_TO_TOP;
        classObject['full-screen'] = this.renderType === RenderType.FULL_SCREEN;
        return classObject;
    }

    getDateClass(date: moment.Moment) {
        let classObject = {};
        classObject['date-selected'] = this.isDateSelected(date);
        classObject['date-out-of-month'] = this.isDateOutOfCurrentMonth(date);
        classObject['date-today'] = this.isDateToday(date);
        return classObject;
    }

    getDaysOfWeek() {
        return moment.weekdaysShort();
    }

    chain = ArrayUtil.chain;

}