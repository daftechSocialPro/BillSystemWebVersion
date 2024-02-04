import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CssHomeComponent } from './css-home/css-home.component';
import { CustomerServiceRoutingModule } from './customer-service-routing.module';
import { CssSetupComponent } from './css-setup/css-setup.component';
import { CssCustomerComponent } from './css-customer/css-customer.component';
import { CssReportComponent } from './css-report/css-report.component';
import { CssBillReportComponent } from './css-bill-report/css-bill-report.component';
import { AddCssCustomerComponent } from './css-customer/add-css-customer/add-css-customer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CssImportComponent } from './css-customer/css-import/css-import.component';
import { PaginatorModule } from 'primeng/paginator';
import { DetailCustomerComponent } from './css-customer/detail-customer/detail-customer.component';
import { CssChangeActionComponent } from './css-customer/css-change-action/css-change-action.component';
import { CssBatchRecordsComponent } from './css-customer/css-batch-records/css-batch-records.component';

@NgModule({
  declarations: [
    CssHomeComponent,
    CssSetupComponent,
    CssCustomerComponent,
    CssReportComponent,
    CssBillReportComponent,
    AddCssCustomerComponent,
    CssBatchRecordsComponent,
    CssImportComponent,
    DetailCustomerComponent,
    CssChangeActionComponent
  ],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, CustomerServiceRoutingModule, PaginatorModule, ReactiveFormsModule]
})
export class CustomerServiceModule {}
