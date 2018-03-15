import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { MateriaModel } from '../../../../../ezs-common/src/model/server/materia.model';

export class PageMateriaListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn({
            value: (item: MateriaModel) => item.nome,
            label: () => 'Nome'
        }),
        new CardTableColumn({
            value: (item: MateriaModel) => item.descricao,
            label: () => 'Descrição'
        })
    ];
    menu = { row: [], main: [] };
    routePathAdd = AppRouterPath.MATERIA_ADD;
    routePathUpdate = AppRouterPath.MATERIA_UPD;
    query = FACTORY_CONSTANT.MateriaFactory.all;
    queryRemove = FACTORY_CONSTANT.MateriaFactory.disable;

}