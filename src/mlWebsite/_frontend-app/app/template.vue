<template>
    <p>{{ msg }}</p>
</template>

<script>
    //import OtherComponent from './otherComponent.vue';
    export default {
       //components : { OtherComponent },
        data() {
            return msg : "Hello, world!";
        } 
    }
</script>

<style>
    /*We can potentially use sass in here, but Visual Studio won't understand it.*/
    .some-class {
        color : blue;
    }
</style>
