import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType } from '../../../util/router/router.path';
import { BroadcastEventBus, BroadcastEvent } from '../../../util/broadcast/broadcast.event-bus';
import { InstituicaoCategoriaFactory } from '../../../util/factory/instituicao-categoria/instituicao-categoria.factory';
import { RouterManager } from '../../../util/router/router.manager';
import { InstituicaoCategoria } from '../../../util/factory/instituicao-categoria/instituicao-categoria';

@Component({
    template: require('./instituicao-categoria-management.html')
})
export class InstituicaoCategoriaManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: InstituicaoCategoria = new InstituicaoCategoria();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RouterPathType.upd) {
                this.model = await InstituicaoCategoriaFactory.dtl(this.$route.params.id, true);
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
                case (RouterPathType.add): {
                    await InstituicaoCategoriaFactory.add(this.model, true);
                    break;
                }
                case (RouterPathType.upd): {
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