import { Vue, Component } from 'vue-property-decorator';
import { AppRouter } from '../../../app.router';
import { AppRouterPath } from '../../../app.router.path';
import { Factory } from '../../../module/constant/factory.constant';
import { ApplicationService } from '../../../module/service/application.service';
import { InstituicaoBusinessAulaModel } from '../../../../../ezs-common/src/model/server/instituicao-business-aula.model';
import { InstituicaoBusinessAulaDetalheAlunoModel } from '../../../../../ezs-common/src/model/server/instituicao-business-aula-detalhe-aluno.model';
import { CursoModel } from '../../../../../ezs-common/src/model/server/curso.model';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';
import { DateUtil } from '../../../../../ezs-common/src/util/date/date.util';
import * as lodash from 'lodash';

interface UI {
    instituicaoBusinessAulasByProfessor: Array < InstituicaoBusinessAulaModel > ;
    instituicaoBusinessAulasByAluno: Array < InstituicaoBusinessAulaDetalheAlunoModel > ;
    uiQueryKeyProfessor: UIQueryKey;
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
    template: require('./page-home.html'),
    filters: Object.assign({}, DateUtil) as any
})
export class PageHomeComponent extends Vue {

    ui: UI = {
        instituicaoBusinessAulasByAluno: undefined,
        instituicaoBusinessAulasByProfessor: undefined,
        uiQueryKeyProfessor: undefined,
        uiQUeryKeyAluno: undefined
    };

    async mounted() {
        this.ui.instituicaoBusinessAulasByProfessor = await Factory.InstituicaoFactory.allInstituicaoBusinessAulaByProfessor(ApplicationService.getUsuarioInfo().id);
        this.ui.instituicaoBusinessAulasByAluno = await Factory.InstituicaoFactory.allInstituicaoBusinessAulaByAluno(ApplicationService.getUsuarioInfo().id);
    }

    getUiQueryKeysFromInstituicaoBusinessAulas(instituicaoBusinessAulas: Array < InstituicaoBusinessAulaModel > ) {
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

    getInstituicaoBusinessAulasByUiQueryKeys(uiQueryKey: UIQueryKey, instituicaoBusinessAulas: Array < InstituicaoBusinessAulaModel > ) {
        return instituicaoBusinessAulas.filter(x => x.instituicao.id === uiQueryKey.instituicao.id && x.curso.id === uiQueryKey.curso.id);
    }

    selectUiQUeryKeyAluno(uiQueryKey: UIQueryKey) {
        this.ui.uiQUeryKeyAluno = uiQueryKey;
    }

    selectUiQUeryKeyProfessor(uiQueryKey: UIQueryKey) {
        this.ui.uiQueryKeyProfessor = uiQueryKey;
    }

    doGoToAulaGerenciamentoNota(id: number) {
        AppRouter.push({ name: AppRouterPath.AULA_GERENCIAMENTO_NOTA, params: { idInstituicaoCursoOcorrenciaPeriodoProfessor: id.toString() } });
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