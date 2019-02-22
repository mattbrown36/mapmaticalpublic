import "bootstrap/less/bootstrap.less";
import "../../wwwroot/css/site.css";

import PolyFill from './polyfill';
import axios from 'axios';

//Setup base api path:
if (window.location.href.includes("localhost:8080")) {
    //Means we're using webpack dev server:
    axios.defaults.baseURL = 'https://localhost:44361/api/v1';
    axios.defaults.withCredentials = true;
}
else {
    //Means we should use the normal api root:
    axios.defaults.baseURL = '/api/v1';
}

import Vue from 'vue';
import VueRouter from 'vue-router';

import MainPage from './main-page.vue';
import WorldsPage from './worlds-page.vue';


PolyFill.run();

Vue.use(VueRouter);

const router = new VueRouter({
    routes: [
        { path: "/", component : WorldsPage}
    ]
});

var vm = new Vue({
    el: '#router-mount',
    render: h => h(MainPage),
    router
});