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
        },
        async getSummary(startDate, endDate){
            try {
                const {data} = await axios.get("https://localhost:7108/Note/summary",{
                params: {startDate, endDate}
                
            })
            return data;
            } catch (error) {
                 if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                } else {
                    throw new Error("Error al conectar con el servidor");
                }
            }
        },
        async download(type, startDate, endDate){
            try {
                const endpoint = {
                pdf:{
                    url: 'https://localhost:7108/Note/note-report',
                    extension: '.pdf'
                },
                excel:{
                    url: 'https://localhost:7108/Note/excel-report',
                    extension: '.xlsx'
                }
            }

            const {url, extension} = endpoint[type]
            const response = await axios.get(url,{
                params:{
                    startDate: startDate || null,
                    endDate: endDate || null
                },
                responseType: "blob"
            });

            const today = new Date()
            const fileName = `Listado de notas ${today.getDay()}-${today.getMonth() + 1}-${today.getFullYear()}${extension}`;
            const fileUrl = window.URL.createObjectURL(response.data);
            const link = document.createElement('a');
            link.href = fileUrl;
            link.setAttribute('download',fileName);
            document.body.appendChild(link);
            link.click();
            link.remove();
            } catch (error) {
                throw new Error("No se pudo generar el reporte de notas.");
            }
        }

    },

})