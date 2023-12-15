import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    login: 'Anonimous ' + ['Cat', 'Dog', 'Mouse', 'Poop']
    .sort(() => Math.random() * 2 - 1).pop(),
    messages: [{
      user: 'Admin',
      message: 'Welcome to chat!',
      id: -1,
    }],
  },
  mutations: {
    pushMessage(state, message) { state.messages.push(message) },
    changeLogin(state, login) { state.login = login }
  },
  actions: {
    pushMessageAction({ commit }, m) { commit('pushMessage', m) },
    changeLoginAction({ commit }, e) { commit('changeLogin', e.target.value) }
  },
  getters: {
    login: state => state.login,
    messages: state => state.messages,
  }
})
