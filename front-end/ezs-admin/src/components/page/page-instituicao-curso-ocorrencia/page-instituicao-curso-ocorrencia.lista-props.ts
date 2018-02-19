import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { BaseRouter } from '../../../../../ezs-common/src/base.router';
import { InstituicaoCursoOcorrenciaModel } from '../../../../../ezs-common/src/model/server/instituicao-curso-ocorrencia.model';
import * as moment from 'moment';

export class PageInstituicaoCursoOcorrenciaListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn((item: InstituicaoCursoOcorrenciaModel) => { return moment(item.dataInicio).format('DD/MM/YYYY'); }, () => 'Data de Início'),
        new CardTableColumn((item: InstituicaoCursoOcorrenciaModel) => { return item.dataExpiracao ? moment(item.dataExpiracao).format('DD/MM/YYYY') : undefined; }, () => 'Data de Fim')
    ];
    menu = { row: [], main: [] };
    routePathAdd = AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA_ADD;
    routePathUpdate = AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA_UPD;
    query = async () => await Factory.InstituicaoFactory.allInstituicaoCursoOcorrencia(AppRouter.app.$route.params.id, AppRouter.app.$route.params.idInstituicaoCurso);
    queryRemove = async (id) => await Factory.InstituicaoFactory.disableInstituicaoCursoOcorrencia(AppRouter.app.$route.params.id, AppRouter.app.$route.params.idInstituicaoCurso, id);
    queryAdd = (path) => AppRouter.push({ name: path, params: { id: AppRouter.app.$route.params.id, idInstituicao: AppRouter.app.$route.params.idInstituicao, idInstituicaoCurso: AppRouter.app.$route.params.idInstituicaoCurso, idInstituicaoCursoOcorrencia: AppRouter.app.$route.params.idInstituicaoCursoOcorrencia } });
    queryUpdate = (item, path) => AppRouter.push({ name: path, params: { id: AppRouter.app.$route.params.id, idInstituicao: AppRouter.app.$route.params.idInstituicao, idInstituicaoCurso: AppRouter.app.$route.params.idInstituicaoCurso, idInstituicaoCursoOcorrencia: item.id } });

}