import { ref, watch } from "vue";
import { defineStore } from "pinia";


export const useSessionStore = defineStore("session", () =>{
    const storedUser = sessionStorage.getItem("currentUser")
    const currentUser = ref(storedUser ? JSON.parse(storedUser) : null);

    const setUser = (user) =>{
        currentUser.value = user;
    };

    watch(currentUser, (newUser) =>{
        if(newUser){
           sessionStorage.setItem("currentUser", JSON.stringify(newUser));

        }else{
            sessionStorage.removeItem("currentUser")
        }

    },{deep: true})

    const logout = () =>{
        currentUser.value = null;
        sessionStorage.removeItem("currentUser");

    }


    return {currentUser,setUser,logout}
})