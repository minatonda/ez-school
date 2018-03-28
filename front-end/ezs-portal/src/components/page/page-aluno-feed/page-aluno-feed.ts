import { Vue, Component, Prop } from 'vue-property-decorator';
import { AppRouter } from '../../../app.router';
import { AppRouterPath } from '../../../app.router.path';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { ApplicationService } from '../../../module/service/application.service';
import { InstituicaoBusinessAulaModel } from '../../../../../ezs-common/src/model/server/instituicao-business-aula.model';
import { InstituicaoBusinessAulaDetalheAlunoModel } from '../../../../../ezs-common/src/model/server/instituicao-business-aula-detalhe-aluno.model';
import { CursoModel } from '../../../../../ezs-common/src/model/server/curso.model';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';
import { DateUtil } from '../../../../../ezs-common/src/util/date/date.util';
import { NotifyUtil } from '../../../../../ezs-common/src/util/notify/notify.util';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import * as lodash from 'lodash';

interface UI {
    instituicaoBusinessAulasByAluno: Array<InstituicaoBusinessAulaDetalheAlunoModel>;
    uiQUeryKeyAluno: UIQueryKey;
}

interface UIQueryKey {
    curso: CursoModel;
    instituicao: InstituicaoModel;
    idInstituicaoCursoOcorrencia: number;
    periodoSequencia: number;
    dataInicio: string;
    dataExpiracao: string;
}

@Component({
    template: require('./page-aluno-feed.html'),
    filters: Object.assign({}, DateUtil) as any
})
export class PageAlunoFeedComponent extends Vue {

    @Prop()
    alias: string;

    ui: UI = {
        instituicaoBusinessAulasByAluno: undefined,
        uiQUeryKeyAluno: undefined
    };

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.ui.instituicaoBusinessAulasByAluno = await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoBusinessAulaByAluno(ApplicationService.getUsuarioInfo().id, true);
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    getUiQueryKeysFromInstituicaoBusinessAulas(instituicaoBusinessAulas: Array<InstituicaoBusinessAulaModel>) {
        return lodash.unionBy(instituicaoBusinessAulas, x => x.idInstituicaoCursoOcorrencia).sort((a, b) => {
            let sorteableA = a.idInstituicaoCursoOcorrencia + '-' + a.periodoSequencia;
            let sorteableB = b.idInstituicaoCursoOcorrencia + '-' + b.periodoSequencia;
            if (sorteableA < sorteableB) return -1;
            if (sorteableA > sorteableB) return 1;
            return 0;
        }).map(x => {
            let uiQueryKey: UIQueryKey = {
                curso: x.curso,
                instituicao: x.instituicao,
                idInstituicaoCursoOcorrencia: x.idInstituicaoCursoOcorrencia,
                periodoSequencia: x.periodoSequencia,
                dataInicio: x.dataInicio,
                dataExpiracao: x.dataExpiracao
            };
            return uiQueryKey;
        });
    }

    getInstituicaoBusinessAulasByUiQueryKeys(uiQueryKey: UIQueryKey, instituicaoBusinessAulas: Array<InstituicaoBusinessAulaModel>) {
        return instituicaoBusinessAulas.sort((a, b) => {
            let sorteableA = a.idInstituicaoCursoOcorrencia + '-' + a.periodoSequencia;
            let sorteableB = b.idInstituicaoCursoOcorrencia + '-' + b.periodoSequencia;
            if (sorteableA < sorteableB) return -1;
            if (sorteableA > sorteableB) return 1;
            return 0;
        }).filter(x => x.instituicao.id === uiQueryKey.instituicao.id && x.curso.id === uiQueryKey.curso.id);
    }

    selectUiQUeryKeyAluno(uiQueryKey: UIQueryKey) {
        this.ui.uiQUeryKeyAluno = uiQueryKey;
    }

    doGoToHistoricoCurso(id: number) {
        AppRouter.push({ name: AppRouterPath.ALUNO_HISTORICO_CURSO, params: { idInstituicaoCursoOcorrencia: id.toString() } });
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