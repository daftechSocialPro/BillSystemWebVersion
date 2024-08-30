import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './theme/layout/admin/admin.component';
import { GuestComponent } from './theme/layout/guest/guest.component';
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    component: AdminComponent,
    children: [
      {
        path: '',
        redirectTo: '/dwm/home',
        pathMatch: 'full'
      },  
    
      {
        path: 'typography',
        loadComponent: () => import('./demo/elements/typography/typography.component')
      },
      {
        path: 'color',
        loadComponent: () => import('./demo/elements/element-color/element-color.component')
      },
      {
        path: 'sample-page',
        loadComponent: () => import('./demo/sample-page/sample-page.component')
      },
      {
        path: 'employees',
        loadComponent: () => import('./demo/pages/employees/employees.component')

      },
      {
        path: 'users',
        loadComponent: () => import('./demo/pages/users/users.component')

      },
      
      {
        path: 'system-control',
        loadChildren: () => import('./demo/pages/system-control/system-control.module').then((m) => m.SystemControlModule)


      },
      {
        path: 'dwm',
        loadChildren: () => import('./demo/pages/dwm/dwm.module').then((m) => m.DwmModule)
      },    
      {
        path: 'customer-service',
        loadChildren: () => import('./demo/pages/customer-service/customer-service.module').then((m) => m.CustomerServiceModule)


      }

    ]
  },
  {
    path: '',
    component: GuestComponent,
    children: [
      {
        path: 'auth',
        loadChildren: () => import('./demo/pages/authentication/authentication.module').then((m) => m.AuthenticationModule)
      }
    ]
  },



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
