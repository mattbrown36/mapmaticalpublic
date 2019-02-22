<template>
    <div>
        <top-nav></top-nav>

        <div class="container main-container">
            <div class="col-sm-12">
                <div>
                    <router-view></router-view>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import topNav from "./top-nav.vue";
    export default {
        data() {
            return {};
        },
        components: {
            topNav,
        },
        methods : {
            logout(){
                this.$http.post('/Account/LogOff').then(function(){
                    window.location.reload(true);
                }, function () {
                    window.location.reload(true);
                });
            }
        },
    }
</script>

<style>
    .main-container {
        margin-top: 60px;
        margin-left: 0px;
        margin-right: 50px;
        width: 100%;
        padding-left: 0px;
    }
</style>
