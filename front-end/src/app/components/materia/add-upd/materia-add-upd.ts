import Vue from 'vue';
import Component from 'vue-class-component';
import axios, { AxiosResponse } from 'axios';
import { Materia } from '../../../common/factory/materia/materia';
import { MateriaFactory } from '../../../common/factory/materia/materia.factory';
import { BroadcastEvent } from '../../../common/vue/broadcast/broadcast.events';
import { BroadcastEventBus } from '../../../common/vue/broadcast/broadcast.event-bus';
import { Notify, MESSAGES } from '../../../common/modules/notify/notify';
import { RouterManager } from '../../../common/vue/router/router.manager';
import { RoutePathType } from '../../../common/vue/router/route-path-type';
import { prop } from 'vue-property-decorator/lib/vue-property-decorator';

@Component({
    template: require('./materia-add-upd.html')
})
export class MateriaAddUpdComponent extends Vue {

    @prop()
    alias: string;
    @prop()
    operation: RoutePathType;

    model: Materia = new Materia();

    constructor() {
        super();
    }

    created() {

    }
    
    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RoutePathType.upd) {
                this.model = await MateriaFactory.dtl(parseInt(this.$route.params.id), true);
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
                    await MateriaFactory.add(this.model, true);
                    break;
                }
                case (RoutePathType.upd): {
                    await MateriaFactory.upd(this.model, true);
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