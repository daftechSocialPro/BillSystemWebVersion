import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
@Component({
  selector: 'app-css-batch-records',
  templateUrl: './css-batch-records.component.html',
  styleUrls: ['./css-batch-records.component.scss']
})
export class CssBatchRecordsComponent implements OnInit {
  @Input() Customer: ICustomerDto[];
  paginationCustomer: ICustomerDto[] = [];
  totlRecords: number = 0;
  searchText: string = '';
  first: number = 0;
  rows: number = 5;
  selectedProperty:string
// property:any[]=[]

  paginationCustomerValues: {
    bankAccount: string,
    bookNo: string,
    meterClass: string,
    readerName: string,
    meterDigit: number,
    paymentMod: string,
    village: string,
    watersource: string
  }[] = [];

  constructor(
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private activeModal: NgbActiveModal,
    private customerService: CssCustomerService
  ) {}

  ngOnInit(): void {
    this.getCustomerForBatchRecord();
  }

  getCustomerForBatchRecord() {
    this.customerService.getCustomer().subscribe({
      next: (res) => {
        this.Customer = res;
        this.paginationCustomerValues = this.Customer.map(item => ({
          bankAccount: item.bankAccount,
          bookNo: item.bookNo,
          meterClass: item.meterClass,
          readerName: item.readerName,
          meterDigit: item.meterDigit,
          paymentMod: item.paymentMode,
          village: item.village,
          watersource: item.waterSource,
        }))
        // this.property=this.paginationCustomerValues
        this.paginatedCustomer(this.Customer);

      }
    });
  }



  filter() {}
  onPageChange(event: any, gInterface?: ICustomerDto[]) {
    this.first = event.first;
    this.rows = event.rows;
    if (gInterface) {
      this.paginatedCustomer(gInterface);
    } else {
      this.paginatedCustomer(this.Customer);
    }
  }
  paginatedCustomer(ginterfces: ICustomerDto[]) {
    this.totlRecords = ginterfces.length;
    this.paginationCustomer = ginterfces.slice(this.first, this.first + this.rows);
  }
  closeModal() {
    this.activeModal.close();
  }
}
