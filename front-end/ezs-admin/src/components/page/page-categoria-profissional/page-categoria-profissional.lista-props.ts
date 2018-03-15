import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { CategoriaProfissionalModel } from '../../../../../ezs-common/src/model/server/categoria-profissional.model';

export class PageCategoriaProfissionalListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn({
            value: (item: CategoriaProfissionalModel) => item.nome,
            label: () => 'Nome'
        }),
        new CardTableColumn({
            value: (item: CategoriaProfissionalModel) => item.descricao,
            label: () => 'Descrição'
        })
    ];
    menu = { row: [], main: [] };
    routePathAdd = AppRouterPath.CATEGORIA_PROFISSIONAL_ADD;
    routePathUpdate = AppRouterPath.CATEGORIA_PROFISSIONAL_UPD;
    query = FACTORY_CONSTANT.CategoriaProfissionalFactory.all;
    queryRemove = FACTORY_CONSTANT.CategoriaProfissionalFactory.disable;

}