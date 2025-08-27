import { defineStore } from "pinia";
import axios from "axios";

export const useUserStore = defineStore("user",{
    state: () =>({
        users: [],
        positions: []
    }),
    actions: {
        async getAllUser(){
            const {data} = await axios.get("https://localhost:7108/User/get-all")
            return this.users = data
        },
        async createUser(user){
            try {
                await axios.post("https://localhost:7108/User/create",user)
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
        async getAllPosition(){
            try {
               const {data} =  await axios.get(`https://localhost:7108/Position/get-all`)
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
        async deleteUser(id){
            await axios.delete(`https://localhost:7108/User/delete/${id}`)
            this.getAllUser()
        },
        async updateUser(user){
            try {
                await axios.put(`https://localhost:7108/User/update/${user.id}`,user)

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
        async getById(id){
          const {data} =  axios.get(`https://localhost:7108/User/get-by-id/${id}`)
          return data;
        }
    }
})