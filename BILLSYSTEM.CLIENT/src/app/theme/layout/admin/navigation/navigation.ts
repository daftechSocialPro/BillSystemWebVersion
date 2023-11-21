import { Injectable } from '@angular/core';

export interface NavigationItem {
  id: string;
  title: string;
  type: 'item' | 'collapse' | 'group';
  icon?: string;
  url?: string;
  classes?: string;
  external?: boolean;
  target?: boolean;
  breadcrumbs?: boolean;
  children?: Navigation[];
}

export interface Navigation extends NavigationItem {
  children?: NavigationItem[];
}
const NavigationItems = [
  {
    id: 'dashboard',
    title: 'Dashboard',
    type: 'group',
    icon: 'icon-navigation',
    children: [
      {
        id: 'default',
        title: 'Default',
        type: 'item',
        classes: 'nav-item',
        url: '/default',
        icon: 'ti ti-dashboard',
        breadcrumbs: false
      }
    ]
  },
  {
    id: 'page2',
    title: 'Bill System',
    type: 'group',
    icon: 'icon-navigation',
    children: [
      {
        id: 'Authentication',
        title: 'Software Control',
        type: 'collapse',
        icon: 'ti ti-settings',
        children: [
          {
            id: 'data',
            title: 'Home',
            type: 'item',       
            url: '/system-control/home',           
            breadcrumbs: false
          },
          {
            id: 'data',
            title: 'Data',
            type: 'item',       
            url: '/system-control/data',           
            breadcrumbs: false
          },
          
          {
            id: 'data',
            title: 'Maintain',
            type: 'item',
           
            url: '/system-control/maintain',
         
            breadcrumbs: false
          },
          
          {
            id: 'setup',
            title: 'Setup',
            type: 'item',       
            url: '/system-control/setup',        
            breadcrumbs: false
          },
          
          {
            id: 'setting',
            title: 'Setting',
            type: 'item',         
            url: '/system-control/setting',     
            breadcrumbs: false
          }
        ]
      },
      {
        id: 'Authentication',
        title: 'Customer Service',
        type: 'collapse',
        icon: 'ti ti-users',
        children: [
          {
            id: 'data',
            title: 'Home',
            type: 'item',       
            url: '/system-control/home',           
            breadcrumbs: false
          },
          {
            id: 'data',
            title: 'Setups',
            type: 'item',       
            url: '/system-control/data',           
            breadcrumbs: false
          },
          
          {
            id: 'data',
            title: 'Customers',
            type: 'item',
           
            url: '/system-control/maintain',
         
            breadcrumbs: false
          },
          
          {
            id: 'setup',
            title: 'Services',
            type: 'item',       
            url: '/system-control/setup',        
            breadcrumbs: false
          },
          
          {
            id: 'setting',
            title: 'Reports',
            type: 'item',         
            url: '/system-control/setting',     
            breadcrumbs: false
          }
        ]
      },
      {
        id: 'Authentication',
        title: 'Bill Produce',
        type: 'collapse',
        icon: 'ti ti-report',
        children: [
          {
            id: 'data',
            title: 'Home',
            type: 'item',       
            url: '/system-control/home',           
            breadcrumbs: false
          },
          {
            id: 'data',
            title: 'Data',
            type: 'item',       
            url: '/system-control/data',           
            breadcrumbs: false
          },
          
          {
            id: 'data',
            title: 'Maintain',
            type: 'item',
           
            url: '/system-control/maintain',
         
            breadcrumbs: false
          },
          
          {
            id: 'setup',
            title: 'Setup',
            type: 'item',       
            url: '/system-control/setup',        
            breadcrumbs: false
          },
          
          {
            id: 'setting',
            title: 'Setting',
            type: 'item',         
            url: '/system-control/setting',     
            breadcrumbs: false
          }
        ]
      },
      {
        id: 'Authentication',
        title: 'Online Bill Print',
        type: 'collapse',
        icon: 'ti ti-book',
        children: [
          {
            id: 'data',
            title: 'Home',
            type: 'item',       
            url: '/system-control/home',           
            breadcrumbs: false
          },
          {
            id: 'data',
            title: 'Data',
            type: 'item',       
            url: '/system-control/data',           
            breadcrumbs: false
          },
          
          {
            id: 'data',
            title: 'Maintain',
            type: 'item',
           
            url: '/system-control/maintain',
         
            breadcrumbs: false
          },
          
          {
            id: 'setup',
            title: 'Setup',
            type: 'item',       
            url: '/system-control/setup',        
            breadcrumbs: false
          },
          
          {
            id: 'setting',
            title: 'Setting',
            type: 'item',         
            url: '/system-control/setting',     
            breadcrumbs: false
          }
        ]
      },
    ]
  },
  
  {
    id: 'page',
    title: 'Configuration',
    type: 'group',
    icon: 'icon-navigation',
    children: [
      {
        id: 'Authentication',
        title: 'System Users',
        type: 'collapse',
        icon: 'ti ti-user',
        children: [
          {
            id: 'employee',
            title: 'Employees',
            type: 'item',
            url: '/employees',        
            breadcrumbs: false
          },
          {
            id: 'user',
            title: 'Users',
            type: 'item',
            url: '/users',
          
            breadcrumbs: false
          }
        ]
      }
    ]
  },
 
 
];

@Injectable()
export class NavigationItem {
  get() {
    return NavigationItems;
  }
}
