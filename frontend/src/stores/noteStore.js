import { defineStore } from "pinia";
import axios from "axios";

export const useNoteStore = defineStore("note", {
    state: () => ({
        notes: [],
        users: []
    }),
    actions: {
        async getAllNotes() {
            const { data } = await axios.get("https://localhost:7108/Note/get-all")
            this.notes = data
        },
        async createNote(note) {
            try {
                await axios.post("https://localhost:7108/Note/create", note);
                await this.getAllNotes();
            } catch (error) {
                if (error.response && error.response.data) {
                    
                    const errors = error.response.data.errors;
                    if (errors) {
                        const firstField = Object.keys(errors)[0];
                        throw new Error(errors[firstField][0]); 
                    }
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }
        },
        async getAllUser(){
            const {data} = await axios.get("https://localhost:7108/User/get-all")
            return this.users = data
        },
        async deleteNote(id) {
            await axios.delete(`https://localhost:7108/Note/delete/${id}`)
            this.getAllNotes();
        },
        async updateNote(note) {
            
            try {
                await axios.put(`https://localhost:7108/Note/update/${note.id}`, note)
            } catch (error) {
                 if (error.response && error.response.data) {
                    
                    const errors = error.response.data.errors;
                    if (errors) {
                        const firstField = Object.keys(errors)[0];
                        throw new Error(errors[firstField][0]); 
                    }
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }

        },
        async getById(id) {
            const { data } = await axios.get(`https://localhost:7108/Note/get-by-id/${id}`)
            return data
        },
        async filterNoteByDate(startDate, endDate) {
            try {
                const { data } = await axios.get("https://localhost:7108/Note/filterbydate", {
                    params: { startDate, endDate }
                });

                this.notes = data;
            } catch (error) {

                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                } else {
                    throw new Error("Error al conectar con el servidor");
                }
            }
        }

    },

})