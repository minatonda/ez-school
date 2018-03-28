import { Vue, Component } from 'vue-property-decorator';
import { AlunoModel } from '../../../../../ezs-common/src/model/server/aluno.model';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { AppRouter } from '../../../app.router';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { ApplicationService } from '../../../module/service/application.service';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { InstituicaoCursoOcorrenciaPeriodoAlunoModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-periodo-aluno.model';
import { InstituicaoCursoOcorrenciaNotaModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-nota.model';
import { I18N_ERROR_GENERIC } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { InstituicaoCursoOcorrenciaAusenciaModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-ausencia.model';
import { AppRouterPath } from '../../../app.router.path';
import * as moment from 'moment';

interface UI {
    alunos: Array<InstituicaoCursoOcorrenciaPeriodoAlunoModel>;
    instituicaoCursoOcorrenciaAusencias: Array<InstituicaoCursoOcorrenciaAusenciaModel>;
    alunosAusenciaTotal: Array<AlunoAusenciaTotal>;
    dataAula: Date;
}

interface AlunoAusenciaTotal {
    instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel;
    count: number;
}

@Component({
    template: require('./page-professor-aula-gerenciamento-ausencia.html')
})
export class PageProfessorAulaGerenciamentoAusenciaComponent extends Vue {

    ui: UI = {
        alunos: [],
        instituicaoCursoOcorrenciaAusencias: [],
        alunosAusenciaTotal: [],
        dataAula: undefined
    };

    async created() {
        this.onDataAulaChanged(new Date());
    }

    async onDataAulaChanged(dataAula) {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.ui.dataAula = dataAula;
            this.ui.alunos = await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOCorrenciaPeriodoProfessor(AppRouter.app.$route.params.idInstituicaoCursoOcorrenciaPeriodoProfessor);
            this.ui.instituicaoCursoOcorrenciaAusencias = await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoCursoOcorrenciaAusenciaByInstituicaoCursoOcorrenciaPeriodoProfessorAndDataAusencia(AppRouter.app.$route.params.idInstituicaoCursoOcorrenciaPeriodoProfessor, moment(this.ui.dataAula).format('YYYY-MM-DD'));
            this.doSetAlunoAusenciaTotalByInstituicaoCursoOcorrenciaAusencia(this.ui.alunos, this.ui.instituicaoCursoOcorrenciaAusencias);
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    async doSave(alunosAusenciasTotal: Array<AlunoAusenciaTotal>) {
        this.doSetInstituicaoCursoOcorrenciaAusenciaByAlunoAusenciaTotal(alunosAusenciasTotal);
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.doSetInstituicaoCursoOcorrenciaAusenciaByAlunoAusenciaTotal(alunosAusenciasTotal);
            await FACTORY_CONSTANT.InstituicaoFactory.saveInstituicaoCursoOcorrenciaAusencias(AppRouter.app.$route.params.idInstituicaoCursoOcorrenciaPeriodoProfessor, moment(this.ui.dataAula).format('YYYY-MM-DD'), this.ui.instituicaoCursoOcorrenciaAusencias);
            NotifyUtil.successG(I18N_ERROR_GENERIC.MODELO_SALVAR, ApplicationService.getLanguage());
        }
        catch (e) {
            NotifyUtil.exception(e, ApplicationService.getLanguage());
            AppRouter.back();
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    doBack() {
        AppRouter.push(AppRouterPath.ROOT);
    }

    doSetInstituicaoCursoOcorrenciaAusenciaByAlunoAusenciaTotal(alunosAusenciasTotal: Array<AlunoAusenciaTotal>) {
        alunosAusenciasTotal.forEach(x => {
            let ausencias = this.ui.instituicaoCursoOcorrenciaAusencias.filter(y => y.instituicaoCursoOcorrenciaPeriodoAluno.id === x.instituicaoCursoOcorrenciaPeriodoAluno.id);
            while ((x.count - ausencias.length) !== 0) {
                if ((x.count - ausencias.length) < 0) {
                    this.ui.instituicaoCursoOcorrenciaAusencias = this.ui.instituicaoCursoOcorrenciaAusencias.filter(x => x !== ausencias[0]);
                }
                if ((x.count - ausencias.length) > 0) {
                    let ausencia = new InstituicaoCursoOcorrenciaAusenciaModel();
                    ausencia.dataAusencia = this.ui.dataAula.toISOString();
                    ausencia.instituicaoCursoOcorrenciaPeriodoAluno = x.instituicaoCursoOcorrenciaPeriodoAluno;
                    this.ui.instituicaoCursoOcorrenciaAusencias.push(ausencia);
                }
                ausencias = this.ui.instituicaoCursoOcorrenciaAusencias.filter(y => y.instituicaoCursoOcorrenciaPeriodoAluno.id === x.instituicaoCursoOcorrenciaPeriodoAluno.id);
            }
        });
    }

    doSetAlunoAusenciaTotalByInstituicaoCursoOcorrenciaAusencia(alunos: Array<InstituicaoCursoOcorrenciaPeriodoAlunoModel>, instituicaoCursoOcorrenciaAusencias: Array<InstituicaoCursoOcorrenciaAusenciaModel>) {
        this.ui.alunosAusenciaTotal = this.ui.alunos.map(x => {
            return { instituicaoCursoOcorrenciaPeriodoAluno: x, count: this.ui.instituicaoCursoOcorrenciaAusencias.filter(y => y.instituicaoCursoOcorrenciaPeriodoAluno.id === x.id).length };
        });
    }

}