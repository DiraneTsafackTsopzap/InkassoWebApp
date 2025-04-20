import { APP_ROUTE_NAMES } from '@/constants/routeNames'

import Register from '@/views/Auth/Register.vue'
import Login from '@/views/Auth/Login.vue'

import AddForderung from '@/views/Client/AddForderung.vue'

import { createRouter, createWebHistory } from 'vue-router'

import Dashboard from '@/views/Client/Dashboard.vue'
import AdminDashboard from '@/views/Admin/AdminDashboard.vue'
import ForderungsDetails from '@/views/Admin/ForderungsDetails.vue'
import SchuldnerDashboard from '@/views/Schuldner/SchuldnerDashboard.vue'


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/login',
      name: APP_ROUTE_NAMES.LOGIN,
      component: Login,
    },
    {
      path: '/',
      name: APP_ROUTE_NAMES.LOGIN,
      component: Login,
    },

    {
      path: '/schuldner-dashboard',
      name: APP_ROUTE_NAMES.SCHULDNERDASHBOARD, 
      component: SchuldnerDashboard,      
      meta: { requiresAuth: true, role: 'Schuldner' },
    },


    
    {
      path: '/admin-dashboard',
      name: APP_ROUTE_NAMES.AdminDashboard,
      component: AdminDashboard,
      meta: { requiresAuth: true, role: 'Admin' },
    },
    
    
    
    
    
    {
      path: '/forderungsdetails/:id',
      name: APP_ROUTE_NAMES.FORDERUNGSDETAILS,
      component: ForderungsDetails,
      meta: { requiresAuth: true, role: 'Admin' },
    },
    {
      path: '/register',
      name: APP_ROUTE_NAMES.REGISTER,
      component: Register,
    },
    
    //---------------------------------------------------------------------
    {
      path: '/add-forderung',
      name: APP_ROUTE_NAMES.ADDFORDERUNG,
      component: AddForderung,
      meta: { requiresAuth: true, role: 'Kunde' },
    },

    {
      path: '/dashboard',
      name: APP_ROUTE_NAMES.DASHBOARD,
      component: Dashboard,
      meta: { requiresAuth: true, role: 'Kunde' },
    },
  ],
})


router.beforeEach((to,from,next) => {
  const token = localStorage.getItem('token')
  const user = JSON.parse(localStorage.getItem('user'))

  if (to.meta.requiresAuth) {
    if (!token || !user) {
   
      return next({ name: APP_ROUTE_NAMES.LOGIN })
    }

    const userRole = user.rolle

   
    if (to.meta.role && to.meta.role !== userRole) {
      
      switch (userRole) {
        case 'Admin':
          return next({ name: APP_ROUTE_NAMES.AdminDashboard })
        case 'Kunde':
          return next({ name: APP_ROUTE_NAMES.DASHBOARD })
        case 'Schuldner':
          return next({ name: APP_ROUTE_NAMES.DASHBOARDSCHULDNER })
        default:
          return next({ name: APP_ROUTE_NAMES.LOGIN })
      }
    }
  }

  next()
})
export default router
