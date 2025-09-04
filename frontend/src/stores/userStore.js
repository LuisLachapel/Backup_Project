import { defineStore } from "pinia";
import axios from "axios";

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
                await axios.post("https://localhost:7108/User/create", user)
                await this.getAllUser();
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
            await axios.delete(`https://localhost:7108/User/delete/${id}`)
            this.getAllUser()
        },
        async updateUser(user) {
            try {
                await axios.put(`https://localhost:7108/User/update/${user.id}`, user)

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
            const { data } = await axios.get(`https://localhost:7108/User/get-by-id/${id}`)
            return data;
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
        async dowloadPdf(startDate, endDate) {
            try {
                const response = await axios.get('https://localhost:7108/User/report', {
                    params: {
                        startDate: startDate || null,
                        endDate: endDate || null
                    },
                    responseType: "blob"
                });

                const today = new Date();
                const fileName = `Listado de usuarios ${today.getDate()}-${today.getMonth() + 1}-${today.getFullYear()}.pdf`;

                const url = window.URL.createObjectURL(new Blob([response.data]));
                const link = document.createElement("a");
                link.href = url;
                link.setAttribute("download", fileName);
                document.body.appendChild(link);
                link.click();
                link.remove();
            } catch (error) {
                throw new Error("No se pudo generar el reporte de usuarios.");
            }
        },
        async downloadExcel(startDate, endDate) {
            try {
                const response = await axios.get("", {
                    params: {
                        startDate: startDate || null,
                        endDate: endDate || null
                    },
                    responseType: 'blob'
                });
                 const today = new Date();
        const fileName = `Listado de usuarios ${today.getDate()}-${today.getMonth() + 1}-${today.getFullYear()}.xlsx`;

        // response.data ya es un Blob, no envolverlo de nuevo
        const url = window.URL.createObjectURL(response.data);
        const link = document.createElement("a");
        link.href = url;
        link.setAttribute("download", fileName);
        document.body.appendChild(link);
        link.click();
        link.remove();

            } catch (error) {

            }
        }
    }
})