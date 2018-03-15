import { Vue } from 'vue-property-decorator';
import { Prop, Component, Model, Watch } from 'vue-property-decorator';

export interface Pagination {
    page: number;
    pageTotal?: number;
    limit: number;
    totalItens?: number;
}

@Component({
    template: require('./gp-pagination.html')
})
export class PaginationComponent extends Vue {

    data: Pagination = {
        page: 1,
        pageTotal: 1,
        limit: 10,
        totalItens: 1
    };

    @Prop({
        type: Object, default: {
            page: 1,
            pageTotal: 1,
            limit: 10,
            totalItens: 1
        }
    })
    defaultConfig: Pagination;

    @Watch('defaultConfig', { deep: true })
    onDefaultConfig(val) {
        this.data = val;
    }

    async doIncreasePage() {
        if (this.data.page < this.data.pageTotal) {
            this.data.page++;
            await this.doQuery(this.data.page, this.data.limit);
        }
    }

    async doDecreasePage() {
        if (this.data.page > 1) {
            this.data.page--;
            await this.doQuery(this.data.page, this.data.limit);
        }
    }

    async doIncreaseLimit() {
        if ((this.data.limit + 10) < this.data.totalItens) {
            this.data.limit += 10;
            await this.doQuery(this.data.page, this.data.limit);
        }
    }

    async doDecreaseLimit() {
        if (this.data.limit > 10) {
            this.data.limit -= 10;
            await this.doQuery(this.data.page, this.data.limit);
        }
    }

    public async doQuery(page: number, limit: number) {
        try {
            this.$emit('pagination-settings-changed', this.data);
        }
        catch (error) {
            console.error(error);
        }
    }


}