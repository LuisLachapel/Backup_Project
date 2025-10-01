import { defineStore } from "pinia";
import axios from "axios";

export const usePermissionStore = defineStore("permission",{
    state: () => ({
        permissions: []
    }),
    actions: {
        async getPermissions(){
            const {data} = await axios.get("https://localhost:7108/Permission/get-all")
            this.permissions = data
        },
        async getById(id){
            const {data} = await axios.get(`https://localhost:7108/Permission/get-by-id/${id}`)
            return data;
        },
        async insert(permission){
            try {
                axios.post("https://localhost:7108/Permission/create",permission)
                await this.getPermissions()
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
        async update(permission){
            try {
                await axios.put(`https://localhost:7108/Permission/update/${permission.id}`,permission)
                
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
        }
    }
})