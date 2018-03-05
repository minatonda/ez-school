import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';
import { InstituicaoCursoModel } from '../../../../../ezs-common/src/model/server/instituicao-curso.model';
import { BaseRouter } from '../../../../../ezs-common/src/base.router';
import * as moment from 'moment';

export class PageInstituicaoCursoListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn((item: InstituicaoCursoModel) => item && item.curso && item.curso.nome, () => 'Nome'),
        new CardTableColumn((item: InstituicaoCursoModel) => item.dataFim, () => 'Início')
    ];
    menu = {
        row: [
            new CardTableMenuEntry(
                (item: InstituicaoCursoModel) => AppRouter.push({
                    name: AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA,
                    params: {
                        id: AppRouter.app.$route.params.id,
                        idInstituicaoCurso: item.curso.id.toString(),
                        dataInicio: moment(item.dataInicio).format('DD-MM-YYYY')
                    }
                }),
                (item) => 'Gerenciar Ocorrências',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            )
        ],
        main: []
    };
    routePathAdd = AppRouterPath.INSTITUICAO_CURSO_ADD;
    routePathUpdate = AppRouterPath.INSTITUICAO_CURSO_UPD;
    query = async () => await Factory.InstituicaoFactory.allInstituicaoCurso(AppRouter.app.$route.params.id);
    queryRemove = async (id) => await Factory.InstituicaoFactory.disableInstituicaoCurso(id, AppRouter.app.$route.params.idInstituicao);
    queryAdd = (path) => AppRouter.push({ name: path, params: { idInstituicao: AppRouter.app.$route.params.idInstituicao } });
    queryUpdate = (item, path) => AppRouter.push({ name: path, params: { id: AppRouter.app.$route.params.id, idInstituicaoCurso: item.id } });

}