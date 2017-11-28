import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType, RouterPath } from '../../../module/model/client/route-path';
import { BroadcastEventBus, BroadcastEvent } from '../../../module/broadcast.event-bus';
import { Router } from '../../../router';
import { CategoriaProfissionalFactory } from '../../../module/factory/categoria-profissional.factory';
import { CategoriaProfissional } from '../../../module/model/server/categoria-profissional';

@Component({
    template: require('./categoria-profissional-management.html')
})
export class CategoriaProfissionalManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: CategoriaProfissional = new CategoriaProfissional();

    constructor() {
        super();
    }

    created() {

    }
    
    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RouterPathType.upd) {
                this.model = await CategoriaProfissionalFactory.detail(this.$route.params.id, true);
            }
        }
        catch (e) {
            Router.redirectRoutePrevious();
        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    async save() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RouterPathType.add): {
                    await CategoriaProfissionalFactory.add(this.model, true);
                    break;
                }
                case (RouterPathType.upd): {
                    await CategoriaProfissionalFactory.update(this.model, true);
                    break;
                }
            }
            Router.redirectRoute(RouterPath.CATEGORIA_PROFISSIONAL);
        }
        catch (e) {

        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

}