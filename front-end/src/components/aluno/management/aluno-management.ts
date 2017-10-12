import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType } from '../../../util/router/router.path';
import { BroadcastEventBus, BroadcastEvent } from '../../../util/broadcast/broadcast.event-bus';
import { RouterManager } from '../../../util/router/router.manager';
import { AlunoFactory } from '../../../util/factory/aluno/aluno.factory';
import { Aluno } from '../../../util/factory/aluno/aluno';
import { Usuario } from '../../../util/factory/usuario/usuario';
import { UsuarioInfo } from '../../../util/factory/usuario/usuario-info';
import { UsuarioFactory } from '../../../util/factory/usuario/usuario.factory';

@Component({
    template: require('./aluno-management.html')
})
export class AlunoManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: Aluno = new Aluno();
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
                this.model = await AlunoFactory.dtl(this.$route.params.id, true);
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
                    await AlunoFactory.add(this.model, true);
                    break;
                }
                case (RouterPathType.upd): {
                    await AlunoFactory.upd(this.model, true);
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