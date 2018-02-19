import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';

export class PageInstituicaoListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn((item: InstituicaoModel) => item.nome, () => 'Nome'),
        new CardTableColumn((item: InstituicaoModel) => item.cnpj, () => 'CNPJ')
    ];
    menu = {
        row: [
            new CardTableMenuEntry(
                (item) => AppRouter.push({ name: AppRouterPath.INSTITUICAO_CURSO, params: { id: item.id } }),
                (item) => 'Gerenciar Cursos',
                (item) => ['fa', 'fa-book'],
                (item) => ['btn-primary']
            )
        ], main: []
    };
    routePathAdd = AppRouterPath.INSTITUICAO_ADD;
    routePathUpdate = AppRouterPath.INSTITUICAO_UPD;
    query = Factory.InstituicaoFactory.all;
    queryRemove = Factory.InstituicaoFactory.disable;

}