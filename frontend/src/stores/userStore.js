import { defineStore } from "pinia";
import axios from "axios";
import { useSessionStore } from "./sessionStore";


export const useUserStore = defineStore("user", {
    state: () => ({
        users: [],
        positions: []
    }),
    actions: {
        async getAllUser() {
            const { data } = await axios.get("https://localhost:7108/User/get-all")
            return this.users = data
        },
        async createUser(user) {
            try {
                const { currentUser } = useSessionStore()
                const userId = currentUser?.id
                await axios.post("https://localhost:7108/User/create", user, {
                    params: { userId }
                })
                await this.getAllUser();
            } catch (error) {
                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }

        },
        async getAllPosition() {
            try {
                const { data } = await axios.get(`https://localhost:7108/Position/get-all`)
                return this.positions = data
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
        async deleteUser(id) {
            try {
                const { currentUser } = useSessionStore()
                const userId = currentUser?.id
                await axios.delete(`https://localhost:7108/User/delete/${id}`, {
                    params: { userId }
                })
                this.getAllUser()
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
        async updateUser(user) {

            const { currentUser } = useSessionStore()
            const userId = currentUser?.id
            try {
                await axios.put(`https://localhost:7108/User/update/${user.id}`, user, {
                    params: { userId }
                })

            } catch (error) {
                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }
        },
        async getById(id) {
            const { data } = await axios.get(`https://localhost:7108/User/get-by-id/${id}`)
            return data.value;
        },
        async getSummary(startDate, endDate) {
            try {
                const { data } = await axios.get('https://localhost:7108/User/summary', {
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
                        url: 'https://localhost:7108/User/report',
                        extension: '.pdf'
                    },
                    excel: {
                        url: 'https://localhost:7108/User/excel-user-report',
                        extension: '.xlsx'
                    }
                };

                const { url, extension } = endpoint[type];
                const { currentUser } = useSessionStore();
                const userId = currentUser?.id;

                const response = await axios.get(url, {
                    params: {
                        startDate: startDate || null,
                        endDate: endDate || null,
                        userId: userId
                    },
                    responseType: "blob",
                    validateStatus: () => true // <-- Permite manejar manualmente códigos 403, 400, etc.
                });

                // 🔹 Verificamos si la respuesta es JSON (error) o archivo (blob)
                const contentType = response.headers["content-type"];

                if (contentType && contentType.includes("application/json")) {
                    const text = await response.data.text();
                    const errorJson = JSON.parse(text);
                    throw new Error(errorJson.message || "Error desconocido");
                }

                // 🔹 Si llega aquí, la respuesta es un archivo válido
                const today = new Date();
                const fileName = `Listado de usuarios ${today.getDate()}-${today.getMonth() + 1}-${today.getFullYear()}${extension}`;
                const fileUrl = window.URL.createObjectURL(response.data);
                const link = document.createElement("a");
                link.href = fileUrl;
                link.setAttribute("download", fileName);
                document.body.appendChild(link);
                link.click();
                link.remove();
            } catch (error) {
                console.error("Error al descargar:", error);
                throw new Error(error.message || "Error desconocido");
            }
        }

    }
})