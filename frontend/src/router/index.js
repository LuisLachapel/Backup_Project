import { createRouter, createWebHistory } from 'vue-router'
import { useSessionStore } from '@/stores/sessionStore'
import Home from '@/views/Home.vue'


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import("@/views/Home.vue")
    },
    {
      path: '/auth',
      name: 'auth',
      component: () =>import("@/views/Auth.vue")

    },
    {
      path: '/users',
      name: 'users',
      component: () => import('@/views/Users.vue')
    },{
      path: '/position',
      name: 'position',
      component: () =>import('@/views/Position.vue')
    },
    {
      path: '/download',
      name: 'download',
      component: () =>import('@/components/DownloadModal.vue')
    }
    
  ],
})

router.beforeEach((to,from,next) =>{
  const session = useSessionStore()
  if(!session.currentUser && to.name !== "auth"){
    next({name: "auth"});
  }else{
    next();
  }
})

export default router
