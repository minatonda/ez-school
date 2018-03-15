import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';
import { InstituicaoCursoModel } from '../../../../../ezs-common/src/model/server/instituicao-curso.model';
import { BaseRouter } from '../../../../../ezs-common/src/base.router';
import * as moment from 'moment';

export class PageInstituicaoCursoListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn({
            value: (item: InstituicaoCursoModel) => item.curso.nome,
            label: () => 'Nome'
        }),
        new CardTableColumn({
            value: (item: InstituicaoCursoModel) => item.dataFim,
            label: () => 'Início'
        })
    ];
    menu = {
        row: [
            new CardTableMenuEntry({
                label: (item) => 'Gerenciar Ocorrências',
                method: (item: InstituicaoCursoModel) => AppRouter.push({
                    name: AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA,
                    params: {
                        id: AppRouter.app.$route.params.id,
                        idInstituicaoCurso: item.curso.id.toString(),
                        dataInicio: moment(item.dataInicio).format('DD-MM-YYYY')
                    }
                }),
                btnClass: (item: InstituicaoCursoModel) => ['btn-primary'],
                iconClass: (item) => ['fa', 'fa-edit']
            })
        ],
        main: []
    };
    routePathAdd = AppRouterPath.INSTITUICAO_CURSO_ADD;
    routePathUpdate = AppRouterPath.INSTITUICAO_CURSO_UPD;
    query = async () => await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoCurso(AppRouter.app.$route.params.id);
    queryRemove = async (id) => await FACTORY_CONSTANT.InstituicaoFactory.disableInstituicaoCurso(id, AppRouter.app.$route.params.idInstituicao);
    queryAdd = (path) => AppRouter.push({ name: path, params: { id: AppRouter.app.$route.params.id } });
    queryUpdate = (item, path) => AppRouter.push({ name: path, params: { id: AppRouter.app.$route.params.id, idInstituicaoCurso: item.id } });

}