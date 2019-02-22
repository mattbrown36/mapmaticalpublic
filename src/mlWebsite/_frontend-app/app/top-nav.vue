<template>
    <div>
        <div class="navbar navbar-default navbar-fixed-top">
            <div class="container navbar-container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <li>
                            <router-link to="/">
                                <h2 class="logo-text">
                                    Some Logo Text
                                </h2>
                            </router-link>
                        </li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li>
                            <router-link to="/">
                                <h2 class="logo-text">
                                    Right Link
                                </h2> 
                            </router-link>
						</li>
                        <li>
                            <a v-on:click="logout">
                                <h2 class="logo-text">
                                    Log Out
                                </h2>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
   export default {
        data() {
            return {};
        },
        components: {
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
    .navbar-container {
        margin-left: 0px;
        margin-right: 0px;
        padding-right: 60px;
        width:100%;
    }

    h2 {
        font-size: large;
        margin-top: 10.5px;
    }
</style>