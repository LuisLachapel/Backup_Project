import { defineStore } from "pinia";
import { useSessionStore } from "./sessionStore";
import axios from "axios";

export const useNoteStore = defineStore("note", {
    state: () => ({
        notes: [],
        users: []
    }),
    actions: {
        async getAllNotes() {
            const { data } = await axios.get("https://localhost:7108/Note/get-all")
            this.notes = data.value
        },
        async getNotesByUser(id) {
            try {
                const { data } = await axios.get(`https://localhost:7108/Note/get-by-user/${id}`)
                this.notes = data.value
            } catch (error) {
                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }
        },
        async createNote(note, isGlobalView = false) {
            const session = useSessionStore()
            const userId = session.currentUser?.id

            const payload = isGlobalView
                ? { ...note }
                : { ...note, userId }

            await axios.post(`https://localhost:7108/Note/create`, payload)


            if (isGlobalView) {
                await this.getAllNotes()
            } else {
                await this.getNotesByUser(userId)
            }
        },
        async getAllUser() {
            const { data } = await axios.get("https://localhost:7108/User/get-all")
            return this.users = data
        },
        async deleteNote(id, isGlobalView = false, userId) {
            await axios.delete(`https://localhost:7108/Note/delete/${id}`)
            if (isGlobalView) {
            await this.getAllNotes(); 
        } else {
            await this.getNotesByUser(userId); 
        }
        },
        async updateNote(note, isGlobalView = false) {
            const session = useSessionStore()
            const userId = session.currentUser?.id

            const payload = isGlobalView
                ? { ...note }
                : { ...note, userId }

            await axios.put(`https://localhost:7108/Note/update/${note.id}`, payload)


            if (isGlobalView) {
                await this.getAllNotes()
            } else {
                await this.getNotesByUser(userId)
            }
        },
        async getById(id) {
            const { data } = await axios.get(`https://localhost:7108/Note/get-by-id/${id}`)
            return data.value
        },
        async filterNoteByDate(startDate, endDate) {
            try {
                const { data } = await axios.get("https://localhost:7108/Note/filterbydate", {
                    params: { startDate, endDate }
                });

                this.notes = data.value;
            } catch (error) {
                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }
        },
        async getSummary(startDate, endDate) {
            try {
                const { data } = await axios.get("https://localhost:7108/Note/summary", {
                    params: { startDate, endDate }

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
        async download(type, startDate, endDate) {
            try {
                const endpoint = {
                    pdf: {
                        url: 'https://localhost:7108/Note/note-report',
                        extension: '.pdf'
                    },
                    excel: {
                        url: 'https://localhost:7108/Note/excel-report',
                        extension: '.xlsx'
                    }
                }

                const { url, extension } = endpoint[type]
                const response = await axios.get(url, {
                    params: {
                        startDate: startDate || null,
                        endDate: endDate || null
                    },
                    responseType: "blob",
                    validateStatus: () => true
                });

                const contentType = response.headers["content-type"];

                if (contentType && contentType.includes("application/json")) {
                    const text = await response.data.text();
                    const errorJson = JSON.parse(text);
                    throw new Error(errorJson.message || "Error desconocido");
                }

                const today = new Date()
                const fileName = `Listado de notas ${today.getDay()}-${today.getMonth() + 1}-${today.getFullYear()}${extension}`;
                const fileUrl = window.URL.createObjectURL(response.data);
                const link = document.createElement('a');
                link.href = fileUrl;
                link.setAttribute('download', fileName);
                document.body.appendChild(link);
                link.click();
                link.remove();
            } catch (error) {
                console.error("Error al descargar:", error);
                throw new Error(error.message || "Error desconocido");
            }
        }

    },

})