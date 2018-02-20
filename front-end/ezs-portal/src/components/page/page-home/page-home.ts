import { Vue, Component } from 'vue-property-decorator';
import { AppRouter } from '../../../app.router';
import { AppRouterPath } from '../../../app.router.path';

@Component({
    template: require('./page-home.html')
})
export class PageHomeComponent extends Vue {

    mounted() {
        AppRouter.push(AppRouterPath.ROOT_ALUNO);
    }

}