import { Vue } from 'vue-property-decorator';
import axios, { AxiosResponse } from 'axios';
import { Component, Prop } from 'vue-property-decorator';
import { RouterPathType, RouterPath } from '../../../util/router/router.path';
import { BroadcastEventBus, BroadcastEvent } from '../../../util/broadcast/broadcast.event-bus';
import { RouterManager } from '../../../util/router/router.manager';
import { CardTableColumn, CardTableMenu, CardTableMenuEntry } from '../../common/card-table/card-table.types';
import { CursoFactory } from '../../../util/factory/curso/curso.factory';
import { MateriaFactory } from '../../../util/factory/materia/materia.factory';
import { Curso } from '../../../util/factory/curso/curso';
import { CursoGrade } from '../../../util/factory/curso/curso-grade';
import { Materia } from '../../../util/factory/materia/materia';

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
                case (RouterPathType.add):
                    {
                        let result = await CursoFactory.add(this.model, true);
                        RouterManager.redirectRoute(RouterPath.CURSO_UPD, result);
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