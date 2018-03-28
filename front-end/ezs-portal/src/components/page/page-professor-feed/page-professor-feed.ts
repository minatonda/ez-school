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
import * as lodash from 'lodash';
import { NotifyUtil } from '../../../../../ezs-common/src/util/notify/notify.util';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';

interface UI {
    instituicaoBusinessAulasByProfessor: Array<InstituicaoBusinessAulaModel>;
    instituicaoBusinessAulasByAluno: Array<InstituicaoBusinessAulaDetalheAlunoModel>;
    uiQueryKeyProfessor: UIQueryKey;
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
    template: require('./page-professor-feed.html'),
    filters: Object.assign({}, DateUtil) as any
})
export class PageProfessorFeedComponent extends Vue {
    
    @Prop()
    alias: string;

    ui: UI = {
        instituicaoBusinessAulasByAluno: undefined,
        instituicaoBusinessAulasByProfessor: undefined,
        uiQueryKeyProfessor: undefined,
        uiQUeryKeyAluno: undefined
    };

    async mounted() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.ui.instituicaoBusinessAulasByProfessor = await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoBusinessAulaByProfessor(ApplicationService.getUsuarioInfo().id);
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

    selectUiQUeryKeyProfessor(uiQueryKey: UIQueryKey) {
        this.ui.uiQueryKeyProfessor = uiQueryKey;
    }

    doGoToAulaGerenciamentoNota(id: number) {
        AppRouter.push({ name: AppRouterPath.PROFESSOR_AULA_GERENCIAMENTO_NOTA, params: { idInstituicaoCursoOcorrenciaPeriodoProfessor: id.toString() } });
    }

    doGoToAulaGerenciamentoAusencia(id: number) {
        AppRouter.push({ name: AppRouterPath.PROFESSOR_AULA_GERENCIAMENTO_AUSENCIA, params: { idInstituicaoCursoOcorrenciaPeriodoProfessor: id.toString() } });
    }

}