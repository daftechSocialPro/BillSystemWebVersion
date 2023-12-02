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
            url: '/customer-service/home',           
            breadcrumbs: false
          },
          {
            id: 'setup',
            title: 'Setup',
            type: 'item',       
            url: '/customer-service/setup',           
            breadcrumbs: false
          },
          
          {
            id: 'customer',
            title: 'Customers',
            type: 'item',
           
            url:'/customer-service/customer' ,
         
            breadcrumbs: false
          },
          
          {
            id: 'report',
            title: 'Reports',
            type: 'item',       
            url: '/customer-service/report',        
            breadcrumbs: false
          },
          {
            id: 'bill-report',
            title: 'Bill Report',
            type: 'item',       
            url: '/customer-service/bill-report',        
            breadcrumbs: false
          },
          
        ]
      },
      {
        id: 'Authentication',
        title: 'DWM',
        type: 'collapse',
        icon: 'ti ti-report',
        children: [
          {
            id: 'home1',
            title: 'Home',
            type: 'item',       
            url: '/dwm/home',           
            breadcrumbs: false
          },
          {
            id: 'mobile-user',
            title: 'Mobile Users Mgmt',
            type: 'item',       
            url: '/dwm/mobile-users-mgmt',           
            breadcrumbs: false
          },

          
          {
            id: 'qr-code',
            title: 'Generate QR Code',
            type: 'item',       
            url: '/dwm/qr-code',        
            breadcrumbs: false
          },
          
          {
            id: 'reading-sheet',
            title: 'Reading Sheet',
            type: 'item',       
            url: '/dwm/reading-sheet',        
            breadcrumbs: false
          },
          
          {
            id: 'reader-tracking',
            title: 'Reader Tracking',
            type: 'item',
           
            url: '/dwm/reader-tracking',
         
            breadcrumbs: false
          },
          
          
          {
            id: 'reports',
            title: 'Reports ',
            type: 'item',         
            url: '/dwm/report',     
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
