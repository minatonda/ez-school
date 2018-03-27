import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';
import { InstituicaoColaboradorPerfilModel } from '../../../../../ezs-common/src/model/server/instituicao-colaborador-perfil.model';

export class PageInstituicaoColaboradorPerfilListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn({
            value: (item: InstituicaoColaboradorPerfilModel) => item.nome,
            label: () => 'Nome'
        })
    ];
    menu = { row: [], main: [] };
    routePathAdd = AppRouterPath.INSTITUICAO_COLABORADOR_PERFIL_ADD;
    routePathUpdate = AppRouterPath.INSTITUICAO_COLABORADOR_PERFIL_UPD;
    query = async () => await FACTORY_CONSTANT.InstituicaoFactory.allInstituicaoColaboradorPerfil(AppRouter.app.$route.params.id);
    queryRemove = async (id) => await FACTORY_CONSTANT.InstituicaoFactory.disableInstituicaoColaboradorPerfil(AppRouter.app.$route.params.id, id);
    queryAdd = (path) => AppRouter.push({ name: path, params: { id: AppRouter.app.$route.params.id } });
    queryUpdate = (item, path) => AppRouter.push({ name: path, params: { id: AppRouter.app.$route.params.id, idInstituicaoColaboradorPerfil: item.id } });

}