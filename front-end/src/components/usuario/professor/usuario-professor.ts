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
    template: require('./usuario-professor.html')
})
export class UsuarioProfessorComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: Professor = new Professor();
    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            this.model = await ProfessorFactory.detail(this.$route.params.id);
        }
        catch (e) {
            try {
                let usuario = await UsuarioFactory.detail(this.$route.params.id);
                this.model = new Professor();
                this.model.usuarioInfo = usuario.usuarioInfo;
            }
            catch (e2) {
                RouterManager.redirectRoutePrevious();
            }
        }
        finally {
            BroadcastEventBus.$emit(BroadcastEvent.ESCONDER_LOADER);
        }
    }

    async save() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            switch (this.operation) {
                case (RouterPathType.add):
                    {
                        await ProfessorFactory.add(this.model, true);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await ProfessorFactory.update(this.model, true);
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