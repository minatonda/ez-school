import { Vue } from 'vue-property-decorator';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType, RouterConfig } from '../../../module/model/client/route-path';
import { BroadcastEventBus, BroadcastEvent } from '../../../module/broadcast.event-bus';
import { Router } from '../../../router';
import { UsuarioFactory } from '../../../module/factory/usuario.factory';
import { UsuarioInfo } from '../../../module/model/server/usuario-info';
import { Usuario } from '../../../module/model/server/usuario';

@Component({
    template: require('./usuario-management.html')
})
export class UsuarioManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: Usuario = new Usuario();

    constructor() {
        super();
    }

    created() {
        this.model.usuarioInfo = new UsuarioInfo();
    }
    
    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RouterPathType.upd) {
                this.model = await UsuarioFactory.detail(this.$route.params.id, true);
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
                    await UsuarioFactory.add(this.model, true);
                    break;
                }
                case (RouterPathType.upd): {
                    await UsuarioFactory.update(this.model, true);
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

    public getRotasLabel(route: RouterConfig) {
        return route.alias;
    }

    public aoSelecionarRota(route: RouterConfig) {
        if (route) {
            Router.redirectRoute(route.path);
        }
    }

}