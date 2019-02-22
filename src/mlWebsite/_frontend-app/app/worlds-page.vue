<template>
    <div>
        <div v-for="world in worldSummaries">
            {{world.name}}
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    export default {
        data() {
            return {
                worldSummaries : [],
            };
        },
        created(){
            this.fetchData();
        },
        destroyed(){
        },
        watch: {
            '$route': 'fetchData',
        },
        components: {
        },
        methods : {
            fetchData(){
                axios.get('Worlds').then((response)=>{
                    this.worlds = response.data;
                });
            },
        },
    }
</script>

<style>
</style>