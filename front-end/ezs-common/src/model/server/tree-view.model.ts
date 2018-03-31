import { BaseModel } from './base.model';

export class TreeViewModel<TID> extends BaseModel<TID> {
    children: Array<TreeViewModel<TID>> = null;
    childrenRequisite: boolean = null;
}