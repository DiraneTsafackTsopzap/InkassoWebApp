import { defineStore } from 'pinia'
import axios from 'axios'
import { useSwal } from '@/utility/useSwal'
const { showSuccess, showError } = useSwal()

export const useForderungsService = defineStore('forderung', {
  state: () => ({
    forderungen: [],
    error: null
  }),

  actions: {
    async createForderung(form) {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.post(
          'http://localhost:5000/api/forderungs',
          form,
          {
            headers: {
              Authorization: `Bearer ${token}`
            }
          }
        );
        showSuccess('Forderung wurde erstellt');
        return response.data;
      } catch (err) {
        this.error = err.response?.data?.message || 'Ein fehler ist aufgetreten';
        showError(this.error);
        throw err;
      }
    },

    async GetForderungenByKundeId(userId) {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.get(
          `http://localhost:5000/api/forderungs/bykunde/${userId}`,
          {
            headers: {
              Authorization: `Bearer ${token}`
            }
          }
        );
        this.forderungen = response.data;
        return response.data;
      } catch (err) {
        this.error = err.response?.data?.message || 'Ein fehler ist aufgetreten';
        showError(this.error);
        throw err;
      }
    }







    ,
    async GetForderungBySchuldnerId(id) {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.get(
          `http://localhost:5000/api/forderungs/byschuldner/${id}`,
          {
            headers: {
              Authorization: `Bearer ${token}`
            }
          }
        );
        this.forderungen = response.data;
        return response.data;
      } catch (err) {
        this.error = err.response?.data?.message || 'Erreur lors de la récupération des créances';
        showError(this.error);
        throw err;
      }
    }

    ,
    async GetAllForderungen() {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.get(
          `http://localhost:5000/api/forderungs`,
          {
            headers: {
              Authorization: `Bearer ${token}`
            }
          }
        );
        this.forderungen = response.data;
        return response.data;
      } catch (err) {
        this.error = err.response?.data?.message || 'Ein fehler ist aufgetreten';
        showError(this.error);
        throw err;
      }
    }
    ,


    // Recuprer la creance by id

    async GetForderungsById(id) {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.get(
          `http://localhost:5000/api/forderungs/${id}`,
          {
            headers: {
              Authorization: `Bearer ${token}`
            }
          }
        );
        this.forderungen = response.data;
        return response.data;
      } catch (err) {
        this.error = err.response?.data?.message || 'Ein fehler ist aufgetreten';
        showError(this.error);
        throw err;
      }
    }

    ,


    // update le statut

    async UpdateForderung(id, status) {
      try {
        const token = localStorage.getItem('token'); // Token récupéré ici
        const response = await axios.put(
          `http://localhost:5000/api/forderungs/${id}/statut`,
          { status }, // corps (body)
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        return response.data;
      } catch (err) {
        this.error = err.response?.data?.message || 'Ein fehler ist aufgetreten';
        showError(this.error);
        throw err;
      }
    }
    ,


    async ForderungLoeschen(id) {
      try {
        const token = localStorage.getItem('token');
        const response = await axios.delete(`http://localhost:5000/api/forderungs/${id}`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        console.log(response.data)
        this.GetAllForderungen();
        return response.data;

      } catch (err) {
        this.error = err.response?.data?.message || 'Ein fehler ist aufgetreten';
        showError(this.error);
        throw err;
      }
    }



  }
})
