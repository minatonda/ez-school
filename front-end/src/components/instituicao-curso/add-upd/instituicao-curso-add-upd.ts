import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType } from '../../../util/router/router.path';
import { BroadcastEventBus, BroadcastEvent } from '../../../util/broadcast/broadcast.event-bus';
import { RouterManager } from '../../../util/router/router.manager';
import { InstituicaoCurso } from '../../../util/factory/instituicao/instituicao-curso';
import { InstituicaoFactory } from '../../../util/factory/instituicao/instituicao.factory';

@Component({
    template: require('./instituicao-curso-add-upd.html')
})
export class InstituicaoCursoAddUpdComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: InstituicaoCurso = new InstituicaoCurso();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RouterPathType.upd) {
                // this.model = await InstituicaoFactory.dtl(parseInt(this.$route.params.id), true);
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
                    await InstituicaoFactory.addCurso(1, this.model, true);
                    break;
                }
                case (RouterPathType.upd): {
                    // await InstituicaoFactory.upd(this.model, true);
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