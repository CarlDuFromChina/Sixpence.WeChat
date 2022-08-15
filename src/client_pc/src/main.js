import App from './App';
import Vue from 'vue';
import Vuex from 'vuex';
import Antd from 'ant-design-vue';
import router from './router';
import store from './store';
import moment from 'moment';
import 'moment/locale/zh-cn';
import mavonEditor from 'mavon-editor';
import 'mavon-editor/dist/css/index.css';
import 'ant-design-vue/dist/antd.css';
import './lib';
import './components';
import './assets/icons';
import './style/index.less';
import './directives';
import 'current-device';

Vue.config.productionTip = false;
Vue.use(Antd);
Vue.use(moment);
Vue.use(Vuex);
Vue.use(mavonEditor);

Vue.prototype.$bus = new Vue();
Vue.filter('moment', (data, formatStr) => (sp.isNullOrEmpty(data) ? '' : moment(data).format(formatStr)));
moment.locale('zh-cn');
Vue.prototype.$moment = moment;

// 如果是移动端则跳转到移动端应用
if (window.device.mobile()) {
  window.location.href = process.env.VUE_APP_MOBILE_URL;
} else {
  /* eslint-disable no-new */
  new Vue({
    el: '#app',
    router,
    store,
    components: { App },
    template: '<App/>'
  });
}