import { ShortViewModel } from "./short/short.viewmodel";

export interface ViewModelAdapterInterface < M, V > {

    getViewModel(model: M): V;
    getModel(viewModel: V): M;
    getShortViewModel(model: M): ShortViewModel;

}