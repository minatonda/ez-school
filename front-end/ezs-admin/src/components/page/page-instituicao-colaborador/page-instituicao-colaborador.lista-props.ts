import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';
import { InstituicaoColaboradorModel } from '../../../../../ezs-common/src/model/server/instituicao-colaborador.model';

export class PageInstituicaoColaboradorListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn({
            value: (item: InstituicaoColaboradorModel) => item.usuario.nome,
            label: () => 'Nome'
        })
    ];
    menu = { row: [], main: [] };
    routePathAdd = AppRouterPath.INSTITUICAO_COLABORADOR_ADD;
    routePathUpdate = AppRouterPath.INSTITUICAO_COLABORADOR_UPD;
    query = async () => await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoColaborador(AppRouter.app.$route.params.id);
    queryRemove = FACTORY_CONSTANT.InstituicaoFactory.disable;
    queryAdd = (path) => AppRouter.push({ name: path, params: { id: AppRouter.app.$route.params.id } });
    queryUpdate = (item, path) => AppRouter.push({ name: path, params: { id: AppRouter.app.$route.params.id, idInstituicaoColaborador: item.id } });

}