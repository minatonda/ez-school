import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { MateriaModel } from '../../../../../ezs-common/src/model/server/materia.model';

export class PageMateriaListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn((item: MateriaModel) => item.nome, () => 'Nome'),
        new CardTableColumn((item: MateriaModel) => item.descricao, () => 'Descrição')
    ];
    menu = { row: [], main: [] };
    routePathAdd = AppRouterPath.MATERIA_ADD;
    routePathUpdate = AppRouterPath.MATERIA_UPD;
    query = Factory.MateriaFactory.all;
    queryRemove = Factory.MateriaFactory.disable;

}