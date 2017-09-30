import { Vue } from 'vue-property-decorator';
import { ComponentizerConfig } from './componentizer.config';

export class ComponentizerManager {

    public static registerGlobal(config: Array<ComponentizerConfig>) {
        for (let componentSpecification of config) {
            Vue.component(componentSpecification.alias, componentSpecification.component);
        }
    }

}