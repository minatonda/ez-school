import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType } from '../../../util/router/router.path';
import { BroadcastEventBus, BroadcastEvent } from '../../../util/broadcast/broadcast.event-bus';
import { RouterManager } from '../../../util/router/router.manager';
import { ProfessorFactory } from '../../../util/factory/professor/professor.factory';
import { Professor } from '../../../util/factory/professor/professor';
import { Usuario } from '../../../util/factory/usuario/usuario';
import { UsuarioInfo } from '../../../util/factory/usuario/usuario-info';
import { UsuarioFactory } from '../../../util/factory/usuario/usuario.factory';

@Component({
    template: require('./professor-management.html')
})
export class ProfessorManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: Professor = new Professor();
    usuarioInfos: Array<UsuarioInfo> = new Array<UsuarioInfo>();

    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            let usuarios = await UsuarioFactory.all();
            this.usuarioInfos = usuarios.map(x => x.usuarioInfo);
            if (this.operation === RouterPathType.upd) {
                this.model = await ProfessorFactory.dtl(this.$route.params.id, true);
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
                    await ProfessorFactory.add(this.model, true);
                    break;
                }
                case (RouterPathType.upd): {
                    await ProfessorFactory.upd(this.model, true);
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