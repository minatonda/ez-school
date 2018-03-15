import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { CursoModel } from '../../../../../ezs-common/src/model/server/curso.model';

export class PageCursoListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn({
            value: (item: CursoModel) => item.nome,
            label: () => 'Nome'
        }),
        new CardTableColumn({
            value: (item: CursoModel) => item.descricao,
            label: () => 'Descrição'
        })
    ];
    menu = { row: [], main: [] };
    routePathAdd = AppRouterPath.CURSO_ADD;
    routePathUpdate = AppRouterPath.CURSO_UPD;
    query = FACTORY_CONSTANT.CursoFactory.all;
    queryRemove = FACTORY_CONSTANT.CursoFactory.disable;
  
}