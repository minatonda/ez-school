import { AppRouterPath } from "../../../app.router.path";
import { CardTableColumn, CardTableMenu } from "../../../../../ezs-common/src/component/card-table/card-table.types";

export interface PageListaPropsInterface {
    columns: Array < CardTableColumn > ;
    menu: CardTableMenu;
    routePathAdd: AppRouterPath;
    routePathUpdate: AppRouterPath;
    query: any;
    queryAdd ? : any;
    queryUpdate ? : any;
    queryRemove: any;
}