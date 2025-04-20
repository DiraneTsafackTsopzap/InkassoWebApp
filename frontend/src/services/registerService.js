import { defineStore } from 'pinia'
import axios from 'axios'
import { useSwal } from '@/utility/useSwal'
const { showError } = useSwal()
import { useRouter } from 'vue-router'
const router = useRouter()
import { APP_ROUTE_NAMES } from '@/constants/routeNames'



export const useRegisterService = defineStore('auth', {
  state: () => ({
    user: null,
    error: null,
    listesKunden: [],
  }),

  actions: {

    // Register User
    async registerUser(form) {
      try {
        const response = await axios.post('http://localhost:5000/api/register', form)

        this.error = null
        return response.data
      } catch (err) {
        this.error = err.response?.data?.message || 'Ein fehler ist aufgetreten'
        throw err
      }
    },

    // Login : http://localhost:5000/api/login

    async loginUser(form) {
      try {
        const response = await axios.post('http://localhost:5000/api/login', form)
        const { token, mein_user } = response.data

        if (token) {
          localStorage.setItem('token', token)
          localStorage.setItem('user', JSON.stringify(mein_user))

          this.user = mein_user
          this.error = null
        } else {
          this.error = 'Fehlender Token'
        }

        return response.data

      } catch (err) {
        this.error = err.response?.data?.message || 'Verbindungsfehler'
        throw err
      }
    }




    ,
    logoutUser() {
      this.user = null
      this.error = null
      localStorage.removeItem('token')
      localStorage.removeItem('user')
    },

    checkAuth() {
      const token = localStorage.getItem('token')
      const user = localStorage.getItem('user')


      console.log('Token:', localStorage.getItem('token'))
      console.log('User:', JSON.parse(localStorage.getItem('user')))


      if (token && user) {
        this.user = JSON.parse(user)
      } else {
        this.user = null
      }
    },



    // Get all Kunde
    async getKunden() {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.get(
          `http://localhost:5000/api/register`,
          {
            headers: {
              Authorization: `Bearer ${token}`
            }
          }
        );
        this.listesKunden = response.data;
        return response.data;
      } catch (err) {
        this.error = err.response?.data?.message || 'Ein fehler ist aufgetreten';
        showError(this.error);
        throw err;
      }
    },
    // Delete Kunde
    async deleteKunde(id) {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.delete(`http://localhost:5000/api/register/${id}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        return response.data;

      } catch (err) {
        this.error = err.response?.data?.message || 'Ein fehler ist aufgetreten';
        showError(this.error);
        throw err;
      }
    }

  }
})
