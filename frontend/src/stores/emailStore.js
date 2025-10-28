import axios from "axios";
import { defineStore } from "pinia";
import { getCurrentUserId } from "@/utils/userHelper";

export const useEmailStore = defineStore("email",{
    state: () =>({

    }),
    actions:{
        async sendEmail(formData) {
            try {
                const userId = getCurrentUserId();
            const {data} = await axios.post("https://localhost:7108/Notification/send",formData,{
                params: {userId},
                headers:{ "Content-Type": "multipart/form-data" }
            })
            return data
            } catch (error) {
                if (error.response && error.response.data) {
                    throw new Error(error.response.data.message || "Error desconocido");
                }
                throw new Error("Error desconocido");
            }
        }
    }

})