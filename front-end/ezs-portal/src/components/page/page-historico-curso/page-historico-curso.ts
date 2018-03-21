import { Vue, Component } from 'vue-property-decorator';
import { AppRouter } from '../../../app.router';
import { AppRouterPath } from '../../../app.router.path';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { ApplicationService } from '../../../module/service/application.service';
import { InstituicaoBusinessAulaModel } from '../../../../../ezs-common/src/model/server/instituicao-business-aula.model';
import { InstituicaoBusinessAulaDetalheAlunoModel } from '../../../../../ezs-common/src/model/server/instituicao-business-aula-detalhe-aluno.model';
import { CursoModel } from '../../../../../ezs-common/src/model/server/curso.model';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';
import { DateUtil } from '../../../../../ezs-common/src/util/date/date.util';
import * as lodash from 'lodash';
import { NotifyUtil } from '../../../../../ezs-common/src/util/notify/notify.util';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';

interface UI {
    instituicaoBusinessAulasByAluno: Array<InstituicaoBusinessAulaDetalheAlunoModel>;
    uiQUeryKeyAluno: UIQueryKey;
}

interface UIQueryKey {
    curso: CursoModel;
    instituicao: InstituicaoModel;
    periodoSequencia: number;
    dataInicio: string;
    dataExpiracao: string;
}

@Component({
    template: require('./page-historico-curso.html'),
    filters: Object.assign({}, DateUtil) as any
})
export class PageHistoricoCursoComponent extends Vue {

    ui: UI = {
        instituicaoBusinessAulasByAluno: undefined,
        uiQUeryKeyAluno: undefined
    };

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.ui.instituicaoBusinessAulasByAluno = await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoBusinessAulaByAlunoAndInstituicaoCursoOcorrencia(ApplicationService.getUsuarioInfo().id, AppRouter.app.$route.params.idInstituicaoCursoOcorrencia);
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    getUiQueryKeysFromInstituicaoBusinessAulas(instituicaoBusinessAulas: Array<InstituicaoBusinessAulaModel>) {
        return lodash.unionBy(instituicaoBusinessAulas, x => x.idInstituicaoCursoOcorrencia).map(x => {
            let uiQueryKey: UIQueryKey = {
                curso: x.curso,
                instituicao: x.instituicao,
                periodoSequencia: x.periodoSequencia,
                dataInicio: x.dataInicio,
                dataExpiracao: x.dataExpiracao
            };
            return uiQueryKey;
        });
    }

    getInstituicaoBusinessAulasByUiQueryKeys(uiQueryKey: UIQueryKey, instituicaoBusinessAulas: Array<InstituicaoBusinessAulaModel>) {
        return instituicaoBusinessAulas.filter(x => x.instituicao.id === uiQueryKey.instituicao.id && x.curso.id === uiQueryKey.curso.id);
    }

    getNotaBackgroundClass(valor: number) {
        if (valor === null) {
            return [];
        }
        else if (valor < 2.5) {
            return ['bg-danger'];
        }
        else if (valor < 5.0) {
            return ['bg-warning'];
        }
        else if (valor < 7.5) {
            return ['bg-info'];
        }
        else {
            return ['bg-success'];
        }
    }

}