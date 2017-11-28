import { Vue } from 'vue-property-decorator';
import axios, { AxiosResponse } from 'axios';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType, RouterPath } from '../../../module/model/client/route-path';
import { BroadcastEventBus, BroadcastEvent } from '../../../module/broadcast.event-bus';
import { Router } from '../../../router';
import { CursoFactory } from '../../../module/factory/curso.factory';
import { Curso } from '../../../module/model/server/curso';

@Component({
    template: require('./curso-management.html')
})
export class CursoManagementComponent extends Vue {

    @Prop()
    alias: string;
    @Prop()
    operation: RouterPathType;

    model: Curso = new Curso();
    
    constructor() {
        super();
    }

    created() {

    }

    async mounted() {
        try {
            BroadcastEventBus.$emit(BroadcastEvent.EXIBIR_LOADER);
            if (this.operation === RouterPathType.upd) {
                this.model = await CursoFactory.detail(this.$route.params.id, true);
                this.model.grades = await CursoFactory.allGrade(this.model.id);
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
                case (RouterPathType.add):
                    {
                        let result = await CursoFactory.add(this.model, true);
                        Router.redirectRoute(RouterPath.CURSO_UPD, result);
                        break;
                    }
                case (RouterPathType.upd):
                    {
                        await CursoFactory.update(this.model, true);
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