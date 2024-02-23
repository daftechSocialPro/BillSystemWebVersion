import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
import { SelectList } from 'src/models/ResponseMessage.Model';
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
  batchValues : SelectList[]=[]
  selectedProperty=[
  ]

  changeFiledNames = [
    'BankCode',
    'billSalesGroup'
  ]
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
    private scsMaintainService : ScsMaintainService,
    private customerService: CssCustomerService
  ) {}

  ngOnInit(): void {
    this.getCustomerForBatchRecord();
  }

  getCustomerForBatchRecord() {
    this.customerService.getCustomer().subscribe({
      next: (res) => {

        this.Customer = res;
        this.paginationCustomerValues = this.Customer.map((item:ICustomerDto )=>({

          bankAccount: item.bankAccount,
          bookNo: item.bookNo,
          meterClass: item.meterClass,
          readerName: item.readerName,
          meterDigit: item.meterDigit,
          paymentMod: item.paymentMode,
          village: item.village,
          watersource: item.waterSource,

        } ))
        this.selectedProperty=this.paginationCustomerValues
        this.paginatedCustomer(res);
        console.log(this.paginationCustomerValues)

      },
      error: (error) => {
        console.error('Error fetching customer data:', error);
      }
    });
  }
  // getCustomerForBatchRecord() {
  //   this.customerService.getCustomer().subscribe(
  //     (res: ICustomerDto[]) => {
  //       this.Customer = res;
  //       this.paginationCustomerValues = res.map((item: ICustomerDto) => {
  //         return {
  //           bankAccount: item.bankAccount,
  //           bookNo: item.bookNo,
  //           meterClass: item.meterClass,
  //           readerName: item.readerName,
  //           meterDigit: item.meterDigit,
  //           paymentMod: item.paymentMode,
  //           village: item.village,
  //           watersource: item.waterSource,
  //         };
  //       });
  //       console.log(this.paginationCustomerValues)

  //       // Call the paginatedCustomer() method with the response
  //       this.paginatedCustomer(res);
  //     },
  //     (error) => {
  //       console.error('Error fetching customer data:', error);
  //     }
  //   );
  // }


getBanks (){
this.scsMaintainService.getBanks().subscribe({
  next:(res)=>{
    this.batchValues = res
  }
})

}
  filter(value: string) {
    if (value=='BankCode'){

      this.getBanks()

    }
    else if (value=='') {
      this.batchValues = []
    }
  }
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
