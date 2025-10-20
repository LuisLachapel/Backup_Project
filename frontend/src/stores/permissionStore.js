import { defineStore } from "pinia";
import axios from "axios";
import { getCurrentUserId } from "@/utils/userHelper";

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
        async getUserPermissionById(id){
            const {data} = await axios.get(`https://localhost:7108/Permission/get-user-permission-by-id/${id}`)
            return data;
        },
        async updateUserPermission(permission){
            try {
                await axios.put(`https://localhost:7108/Permission/update-permission`,{
                    userId: permission.id,
                    permissionIds: permission.permissions
                })
            }  catch (error) {
                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }
        },
        async insert(permission){
            try {
                await axios.post("https://localhost:7108/Permission/create",permission)
                await this.getPermissions()
            }  catch (error) {
                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }
        },
        async update(permission){
            try {
                const userId = getCurrentUserId();
                await axios.put(`https://localhost:7108/Permission/update/${permission.id}`,permission,{
                    params: {userId}
                })
                await this.getPermissions()
            }  catch (error) {
                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }
        }
    }
})