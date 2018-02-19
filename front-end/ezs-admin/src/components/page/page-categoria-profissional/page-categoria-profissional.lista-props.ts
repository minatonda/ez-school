import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { CategoriaProfissionalModel } from '../../../../../ezs-common/src/model/server/categoria-profissional.model';

export class PageCategoriaProfissionalListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn((item: CategoriaProfissionalModel) => item.nome, () => 'Nome'),
        new CardTableColumn((item: CategoriaProfissionalModel) => item.descricao, () => 'Descrição')
    ];
    menu = { row: [], main: [] };
    routePathAdd = AppRouterPath.CATEGORIA_PROFISSIONAL_ADD;
    routePathUpdate = AppRouterPath.CATEGORIA_PROFISSIONAL_UPD;
    query = Factory.CategoriaProfissionalFactory.all;
    queryRemove = Factory.CategoriaProfissionalFactory.disable;
  
}