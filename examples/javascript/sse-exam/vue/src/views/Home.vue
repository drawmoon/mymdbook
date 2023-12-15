<template>
  <div class="home">
      <h1>Please enter your name</h1>
      <input type="text" :value="login" @change="changeLoginAction" class="login-control" />
      <button  class="login-control" @click="goToChat" >Go chat!</button>
  </div>
</template>

<script>
// @ is an alias to /src

import { mapActions, mapGetters } from 'vuex';
import router from '../router';

export default {
  name: 'home',
  methods: {
    goToChat() {
      document.cookie = `user=${this.login}`
      router.push('/chat')
    },
    ...mapActions(['changeLoginAction']),
  },
  computed: mapGetters(['login'])
}
</script>

<style scoped>
.home {
  display: flex;
  flex-direction: column;
  padding: 10px;
}

.login-control {
  font-size: 20px;
  margin-bottom: 10px;
}
</style>
