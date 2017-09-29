import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { Instituicao } from '../../../common/factory/instituicao/instituicao';
import { InstituicaoFactory } from '../../../common/factory/instituicao/instituicao.factory';
import { BroadcastEvent } from '../../../common/vue/broadcast/broadcast.events';
import { BroadcastEventBus } from '../../../common/vue/broadcast/broadcast.event-bus';
import { Notify, MESSAGES } from '../../../common/modules/notify/notify';
import { RouterManager } from '../../../common/vue/router/router.manager';
import { RoutePathType } from '../../../common/vue/router/route-path-type';
import { prop } from 'vue-property-decorator/lib/vue-property-decorator';

@Component({
    template: require('./instituicao-add-upd.html')
})
export class InstituicaoAddUpdComponent extends Vue {

    @prop()
    alias: string;
    @prop()
    operation: RoutePathType;

    model: Instituicao = new Instituicao();

    constructor() {
        super();
    }

    created() {

    }
    
    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RoutePathType.upd) {
                this.model = await InstituicaoFactory.dtl(parseInt(this.$route.params.id), true);
            }
        }
        catch (e) {
            RouterManager.redirectRoutePrevious();
        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    async save() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RoutePathType.add): {
                    await InstituicaoFactory.add(this.model, true);
                    break;
                }
                case (RoutePathType.upd): {
                    await InstituicaoFactory.upd(this.model, true);
                    break;
                }
            }
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

}