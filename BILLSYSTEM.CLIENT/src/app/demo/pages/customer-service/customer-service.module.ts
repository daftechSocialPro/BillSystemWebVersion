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
import { CssTransferCustomerComponent } from './css-customer/transfer-customer/transfer-customer.component';
import { CssChangeMeterComponent } from './css-customer/css-change-meter/css-change-meter.component';
import { CssGeneralComponent } from './css-report/css-general/css-general.component';
import { CssCustomerInfoComponent } from './css-report/css-customer-info/css-customer-info.component';
import { CssStatusComponent } from './css-report/css-status/css-status.component';
import { CustByBillInfoComponent } from './css-report/css-customer-info/cust-by-bill-info/cust-by-bill-info.component';
import { CustByGroupComponent } from './css-report/css-customer-info/cust-by-group/cust-by-group.component';
import { CustbymeterComponent } from './css-report/css-customer-info/cust-by-meter/custbymeter.component';
import { CustByRegistrationComponent } from './css-report/css-customer-info/cust-by-registration/cust-by-registration.component';
import { ChangeMeterComponent } from './css-report/css-status/change-meter/change-meter.component';
import { DisconnectCustComponent } from './css-report/css-status/disconnect-cust/disconnect-cust.component';
import { ReconnectCustComponent } from './css-report/css-status/reconnect-cust/reconnect-cust.component';
import { TerminateCustComponent } from './css-report/css-status/terminate-cust/terminate-cust.component';


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
    CssChangeActionComponent,
    CssTransferCustomerComponent,
    CssChangeMeterComponent,
    CssGeneralComponent,
    CssCustomerInfoComponent,
    CssStatusComponent,
    CustByBillInfoComponent,
    CustByGroupComponent,
    CustbymeterComponent,
    CustByRegistrationComponent,
    ChangeMeterComponent,
    DisconnectCustComponent,
    ReconnectCustComponent,
    TerminateCustComponent,
  ],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, CustomerServiceRoutingModule, PaginatorModule, ReactiveFormsModule]
})
export class CustomerServiceModule {}
