import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { CursoModel } from '../../../../../ezs-common/src/model/server/curso.model';

export class PageCursoListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn((item: CursoModel) => item.nome, () => 'Nome'),
        new CardTableColumn((item: CursoModel) => item.descricao, () => 'Descrição')
    ];
    menu = { row: [], main: [] };
    routePathAdd = AppRouterPath.CURSO_ADD;
    routePathUpdate = AppRouterPath.CURSO_UPD;
    query = Factory.CursoFactory.all;
    queryRemove = Factory.CursoFactory.disable;
  
}