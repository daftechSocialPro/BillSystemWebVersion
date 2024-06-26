import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddCssCustomerComponent } from './add-css-customer/add-css-customer.component';
import { CssImportComponent } from './css-import/css-import.component';
import { ConfirmationService, MessageService, ConfirmEventType } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { DetailCustomerComponent } from './detail-customer/detail-customer.component';
import { ICustomerGetDto } from 'src/models/customer-service/ICustomerGetDto';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
import { CssChangeActionComponent } from './css-change-action/css-change-action.component';
import { CssBatchRecordsComponent } from './css-batch-records/css-batch-records.component';
import { CssTransferCustomerComponent } from './transfer-customer/transfer-customer.component';
import { CssChangeMeterComponent } from './css-change-meter/css-change-meter.component';

@Component({
  selector: 'app-css-customer',
  templateUrl: './css-customer.component.html',
  styleUrls: ['./css-customer.component.scss']
})
export class CssCustomerComponent implements OnInit {
  Customer: ICustomerGetDto[];
  customer: ICustomerDto[];

  displaySelectOption: boolean = false;
  selectedOption: string = '';

  filterdInterface: ICustomerGetDto[] = [];
  totlRecords: number = 0;
  searchText: string = '';
  first: number = 0;
  rows: number = 5;
  paginationCustomer: ICustomerGetDto[] = [];

  constructor(
    private modalService: NgbModal,

    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private customerService: CssCustomerService
  ) {}
  ngOnInit(): void {
    this.getCustomers();
  }

  filterInterfaces() {
    if (this.searchText.trim() === '') {
      this.filterdInterface = this.Customer;
    } else {
      this.filterdInterface = this.Customer.filter(
        (item) =>
          item.customerName.toLowerCase().includes(this.searchText.toLowerCase()) ||
          item.contractNo.toLowerCase().includes(this.searchText.toLowerCase())
      );
    }
    this.first = 0;
    this.onPageChange({ first: this.first, rows: this.rows }, this.filterdInterface);
  }

  getCustomers() {
    this.customerService.getCustomer().subscribe({
      next: (res) => {
        this.Customer = res;
        this.paginatedCustomer(this.Customer);
      }
    });
  }

  addcustomer() {
    let modalRef = this.modalService.open(DetailCustomerComponent, { backdrop: 'static', windowClass: 'custom-modal-width' });
    modalRef.result.then(() => {
      this.getCustomers();
    });
  }
  changeAction(customer:ICustomerGetDto) {
    let modalRef = this.modalService.open(CssChangeActionComponent, { backdrop: 'static', size: 'lg' });
    modalRef.componentInstance.customer = customer;
    modalRef.result.then(() => {
      this.getCustomers()
    });
  }
  changeMeter(customer:ICustomerGetDto) {
    let modalRef = this.modalService.open(CssChangeMeterComponent, { backdrop: 'static', windowClass: 'custom-modal-width' });
    modalRef.componentInstance.customer = customer;
    modalRef.result.then(() => {
      this.getCustomers()
    });
  }

  transferCustomer() {
    let modalRef = this.modalService.open(CssTransferCustomerComponent, { backdrop: 'static', size: 'lg' });
    // modalRef.componentInstance.customer = customer;
    modalRef.result.then(() => {
      this.getCustomers()
    });
  }
  addBatchRecords(){
    let modalRef = this.modalService.open(CssBatchRecordsComponent,  {backdrop: 'static', windowClass: 'custom-modal-width ' })
    modalRef.result.then(()=>{
      this.getCustomers()
    })
  }

  updateCustomer(customer: ICustomerDto) {
    let modalRef = this.modalService.open(DetailCustomerComponent, { backdrop: 'static', windowClass: 'custom-modal-width' });
    modalRef.result.then(() => {
      this.getCustomers();
      console.log(customer)
    });
  }

  showSelectOption() {
    this.displaySelectOption = true;
  }

  handleOptionSelection() {
    if (this.selectedOption === 'button') {
      this.addcustomer();
    } else {
    }
  }

  cssImport() {
    let modalRef = this.modalService.open(CssImportComponent, { size: 'lg', backdrop: 'static' });
    modalRef.result.then(() => {});
  }
  onPageChange(event: any, gInterface?: ICustomerGetDto[]) {
    this.first = event.first;
    this.rows = event.rows;
    if (gInterface) {
      this.paginatedCustomer(gInterface);
    } else {
      this.paginatedCustomer(this.Customer);
    }
  }
  paginatedCustomer(ginterfces: ICustomerGetDto[]) {
    this.totlRecords = ginterfces.length;
    this.paginationCustomer = ginterfces.slice(this.first, this.first + this.rows);
  }

  goToDetails(contractNo: string) {
    let modalRef = this.modalService.open(DetailCustomerComponent, { backdrop: 'static', windowClass: 'custom-modal-width' });
    modalRef.componentInstance.contractNo = contractNo;
    modalRef.result.then(() => {
      this.getCustomers();
    });
  }

  deleteCustomer(contractNo: string) {
    this.confirmationService.confirm({
      message: 'Are You sure you want to delete this Customer?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.customerService.deleteCustomer(contractNo).subscribe({
          next: (res) => {
            if (res.success) {
              this.messageService.add({ severity: 'success', summary: 'Successfull', detail: res.message });
              this.getCustomers();
            } else {
              this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: res.message });
            }
          },
          error: (err) => {
            this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: err });
          }
        });
      },
      reject: (type: ConfirmEventType) => {
        switch (type) {
          case ConfirmEventType.REJECT:
            this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
            break;
          case ConfirmEventType.CANCEL:
            this.messageService.add({ severity: 'warn', summary: 'Cancelled', detail: 'You have cancelled' });
            break;
        }
      },
      key: 'positionDialog'
    });
  }
}
