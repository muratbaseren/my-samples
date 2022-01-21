Vue.component('header-component', {
    props: ['menuItems'],
    template: '<header><ul><li v-for="item in menuItems">{{item}}</li></ul></header>'
})