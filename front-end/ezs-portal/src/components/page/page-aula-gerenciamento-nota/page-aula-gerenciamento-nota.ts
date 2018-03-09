import { Vue, Component } from 'vue-property-decorator';
import { AlunoModel } from '../../../../../ezs-common/src/model/server/aluno.model';
import { AppBroadcastEventBus, AppBroadcastEvent } from '../../../app.broadcast-event-bus';
import { AppRouter } from '../../../app.router';
import { I18N_MESSAGE } from '../../../../../ezs-common/src/constant/i18n-template-messages.contant';
import { NotifyUtil, NOTIFY_TYPE } from '../../../../../ezs-common/src/util/notify/notify.util';
import { ApplicationService } from '../../../module/service/application.service';
import { Factory } from '../../../module/constant/factory.constant';
import { InstituicaoCursoOcorrenciaPeriodoAlunoModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-periodo-aluno.model';
import { InstituicaoCursoOcorrenciaNotaModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia-nota.model';


interface UI {
    alunos: Array < InstituicaoCursoOcorrenciaPeriodoAlunoModel > ;
    idTags: Array < string > ;
    instituicaoCursoOcorrenciaNotas: Array < InstituicaoCursoOcorrenciaNotaModel > ;
    formulaNotaFinal: Array < string > ;
}

@Component({
    template: require('./page-aula-gerenciamento-nota.html')
})
export class PageAulaGerenciamentoNotaComponent extends Vue {

    ui: UI = {
        alunos: [],
        idTags: [],
        instituicaoCursoOcorrenciaNotas: [],
        formulaNotaFinal: []
    };

    async created() {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            this.ui.alunos = await Factory.InstituicaoFactory.allInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOCorrenciaPeriodoProfessor(AppRouter.app.$route.params.idInstituicaoCursoOcorrenciaPeriodoProfessor);
            this.ui.instituicaoCursoOcorrenciaNotas = await Factory.InstituicaoFactory.allInstituicaoCursoOcorrenciaNotasByInstituicaoCursoOCorrenciaPeriodoProfessor(AppRouter.app.$route.params.idInstituicaoCursoOcorrenciaPeriodoProfessor);
            this.ui.idTags = this.ui.instituicaoCursoOcorrenciaNotas.map(x => x.idTag).filter((x, i, array) => array.indexOf(x) === i);
            this.ui.formulaNotaFinal = await Factory.InstituicaoFactory.formulaNotaFinal(AppRouter.app.$route.params.idInstituicaoCursoOcorrenciaPeriodoProfessor);
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.CONSULTAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
            AppRouter.back();
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    async doSaveNotas(instituicaoCursoOcorrenciaNotas: Array < InstituicaoCursoOcorrenciaNotaModel > , formulaNotaFinal: Array < string > ) {
        try {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.EXIBIR_LOADER);
            await Factory.InstituicaoFactory.saveInstituicaoCursoOcorrenciaNotas(AppRouter.app.$route.params.idInstituicaoCursoOcorrenciaPeriodoProfessor, instituicaoCursoOcorrenciaNotas);
            await Factory.InstituicaoFactory.saveFormulaNotaFinal(AppRouter.app.$route.params.idInstituicaoCursoOcorrenciaPeriodoProfessor, formulaNotaFinal);
            NotifyUtil.notifyI18N(I18N_MESSAGE.MODELO_SALVAR, ApplicationService.getLanguage(), NOTIFY_TYPE.SUCCESS);
        }
        catch (e) {
            NotifyUtil.notifyI18NError(I18N_MESSAGE.MODELO_SALVAR_FALHA, ApplicationService.getLanguage(), NOTIFY_TYPE.ERROR, e);
        }
        finally {
            AppBroadcastEventBus.$emit(AppBroadcastEvent.ESCONDER_LOADER);
        }
    }

    addIdTag(idTag: string) {
        this.ui.idTags.push(idTag);
        this.ui.instituicaoCursoOcorrenciaNotas = this.generateInstituicaoCursoOcorrenciaNotas(this.ui.idTags, this.ui.alunos);
    }

    incrementFormulaNotaFinal(value) {
        this.ui.formulaNotaFinal.push(value);
    }

    decrementFormulaNotaFinal() {
        this.ui.formulaNotaFinal.pop();
    }

    getFormulaNotaFinalAsText() {
        return this.ui.formulaNotaFinal.join('');
    }

    generateInstituicaoCursoOcorrenciaNotas(idTags: Array < string > , alunos: Array < InstituicaoCursoOcorrenciaPeriodoAlunoModel > ) {
        let instituicaoCursoOcorrenciaNotas = new Array < InstituicaoCursoOcorrenciaNotaModel > ();
        alunos.forEach(aluno => {
            idTags.forEach(idTag => {
                let instituicaoCursoOcorrenciaNota = new InstituicaoCursoOcorrenciaNotaModel();
                instituicaoCursoOcorrenciaNota.idTag = idTag;
                instituicaoCursoOcorrenciaNota.instituicaoCursoOcorrenciaPeriodoAluno = aluno;
                instituicaoCursoOcorrenciaNota.valor = 0;

                instituicaoCursoOcorrenciaNota = this.ui.instituicaoCursoOcorrenciaNotas.find(x => {
                    return x.idTag === idTag && x.instituicaoCursoOcorrenciaPeriodoAluno.id === aluno.id;
                }) || instituicaoCursoOcorrenciaNota;

                instituicaoCursoOcorrenciaNotas.push(instituicaoCursoOcorrenciaNota);
            });
        });
        return instituicaoCursoOcorrenciaNotas;
    }

    getInstituicaoCursoOcorrenciaNotaByInstituicaoCursoOcorrenciaPeriodoAlunoAndIdTag(instituicaoCursoOcorrenciaPeriodoAluno: InstituicaoCursoOcorrenciaPeriodoAlunoModel, idTag: string) {
        return this.ui.instituicaoCursoOcorrenciaNotas.find(x => {
            return x.idTag === idTag && x.instituicaoCursoOcorrenciaPeriodoAluno.id === instituicaoCursoOcorrenciaPeriodoAluno.id;
        });
    }

}