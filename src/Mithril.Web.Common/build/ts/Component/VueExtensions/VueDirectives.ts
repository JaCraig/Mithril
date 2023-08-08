import Vue from 'vue';
import clickOutside from './Directives/clickOutside';

export function RegisterDirectives(app: Vue.App<Element>): Vue.App<Element> {
    app.directive("click-outside", clickOutside);
    return app;
}