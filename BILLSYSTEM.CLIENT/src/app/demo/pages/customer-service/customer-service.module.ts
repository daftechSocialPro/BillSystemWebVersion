import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CssHomeComponent } from './css-home/css-home.component';
import { CustomerServiceRoutingModule } from './customer-service-routing.module';
import { CssSetupComponent } from './css-setup/css-setup.component';
import { CssCustomerComponent } from './css-customer/css-customer.component';
import { CssReportComponent } from './css-report/css-report.component';
import { CssBillReportComponent } from './css-bill-report/css-bill-report.component';


@NgModule({
  declarations: [    
    CssHomeComponent, CssSetupComponent, CssCustomerComponent, CssReportComponent, CssBillReportComponent
  ],
  imports: [
    CommonModule,
    CustomerServiceRoutingModule
    
  ]
})
export class CustomerServiceModule { }
