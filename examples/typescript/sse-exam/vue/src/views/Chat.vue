<template>
  <div class="chat">
      <div class="messages">
        <div v-for="message in messages" :key="message.id" class="message">
          {{ message.user }}: {{ message.message }}
        </div>
      </div>

      <div class="controls">
        <input type="text" v-model="message" class="message-input" placeholder="Write something.." />
        <button class="message-button" @click="sendMessage">Send!</button>
      </div>
      
  </div>
</template>

<script>
// @ is an alias to /src

import { mapActions, mapGetters } from 'vuex';
import router from '../router';

export default {
  name: 'chat',
  data() {
    return {
      message: '',
    }
  },
  methods: {
    sendMessage() {
      fetch('http://localhost:3000/message/send', {
        headers: {
          'Content-Type': 'application/json',
        },
        credentials: 'include',
        method: 'POST',
        body: JSON.stringify({ 
          user: this.login,
          message: this.message,
        })
      });
      this.message = '';
    },
    ...mapActions(['pushMessageAction']),
  },
  computed: mapGetters(['login', 'messages']),
  created() {
    const pushMessageAction = this.pushMessageAction;
    const eventSource = new EventSource("http://localhost:3000/message/connect", { withCredentials: true });
    eventSource.onmessage = function(event) {
      console.log("New message", event.data);
      pushMessageAction(JSON.parse(event.data));
    };
  }
}
</script>

<style scoped>
.chat {
  display: flex;
  flex-direction: column;
  height: 100vh;
}

.messages {
  flex: 1;
  padding: 10px;
  overflow: auto;
  display: flex;
  flex-direction: column;
}

.messages .message {
  padding: 10px;
  border-radius: 10px;
  background-color: blanchedalmond;
  margin: 10px;
}

.controls {
  display: flex;
}

.controls .message-input {
  flex: 1
}

.controls > input {
  font-size: 20px;
}
</style>
