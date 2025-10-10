import { defineStore } from "pinia";
import axios from "axios";

export const usePositionStore = defineStore('position',{
    state: () =>({
        positions: []
    }),
    actions: {
        async getAllPosition(){
            try {
               const {data} =  await axios.get(`https://localhost:7108/Position/get-all`)
               return this.positions = data.value
            } catch (error) {
                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }
        },
        async createPosition(position){
           try {
              await axios.post('https://localhost:7108/Position/create',position)
              this.getAllPosition()
           }  catch (error) {
                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }
        },
        async deletePosition(id){
            await axios.delete(`https://localhost:7108/Position/delete/${id}`)
            this.getAllPosition()
        },
        async updatePosition(position){
            try {
                await axios.put(`https://localhost:7108/Position/update/${position.id}`,position)
            }  catch (error) {
                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }
        },
        async getPositionById(id){
            const {data} = await axios.get(`https://localhost:7108/Position/get-by-id/${id}`)
            return data.value
        }
    }
})