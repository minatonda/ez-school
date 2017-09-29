import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { InstituicaoCategoria } from '../../../common/factory/instituicao-categoria/instituicao-categoria';
import { InstituicaoCategoriaFactory } from '../../../common/factory/instituicao-categoria/instituicao-categoria.factory';
import { BroadcastEvent } from '../../../common/vue/broadcast/broadcast.events';
import { BroadcastEventBus } from '../../../common/vue/broadcast/broadcast.event-bus';
import { Notify, MESSAGES } from '../../../common/modules/notify/notify';
import { RouterManager } from '../../../common/vue/router/router.manager';
import { RoutePathType } from '../../../common/vue/router/route-path-type';
import { prop } from 'vue-property-decorator/lib/vue-property-decorator';

@Component({
    template: require('./instituicao-categoria-add-upd.html')
})
export class InstituicaoCategoriaAddUpdComponent extends Vue {

    @prop()
    alias: string;
    @prop()
    operation: RoutePathType;

    model: InstituicaoCategoria = new InstituicaoCategoria();

    constructor() {
        super();
    }

    created() {

    }
    
    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RoutePathType.upd) {
                this.model = await InstituicaoCategoriaFactory.dtl(parseInt(this.$route.params.id), true);
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
                    await InstituicaoCategoriaFactory.add(this.model, true);
                    break;
                }
                case (RoutePathType.upd): {
                    await InstituicaoCategoriaFactory.upd(this.model, true);
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