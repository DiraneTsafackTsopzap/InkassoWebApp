
<!-- Part 1  : Template -->
<template>
  
  <div class="d-flex flex-column min-vh-100 container py-5">
      
    <Header></Header>

       <div class="flex-grow-1">

        <RouterView />
       </div>
 

    <Footer></Footer>

  </div>
  
</template>






<!-- Part 2  : Script -->
<script setup>
import { RouterLink, RouterView } from 'vue-router'
import Header from '@/components/layout/Header.vue'
import Footer from '@/components/layout/Footer.vue'

</script>
