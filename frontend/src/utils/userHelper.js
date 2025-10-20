import { useSessionStore } from "@/stores/sessionStore";

export function getCurrentUserId(){
    const session = useSessionStore()
    return session.currentUser?.id ?? null
}