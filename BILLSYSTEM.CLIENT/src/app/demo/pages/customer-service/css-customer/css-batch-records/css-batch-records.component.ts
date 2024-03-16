import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
//import { SelectList } from 'src/models/ResponseMessage.Model';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
import { IGeneralSettingDto } from 'src/models/system-control/IGeneralSettingDto';

interface SelectList {
  id: string;
  name: string;
}
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
  batchValues: SelectList[] = [];
  MeterClasses: IGeneralSettingDto[];
  MeterDigit:IGeneralSettingDto[];
  paymentDuration:ICustomerDto[]
  selectedProperty = [];

  changeFiledNames = ['Bank Code', 'BillSales Group', 'Meter Class','Meter Digit','Payment Duration'];


  constructor(
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private activeModal: NgbActiveModal,
    private scsMaintainService: ScsMaintainService,
    private customerService: CssCustomerService,
    private controlService: ScsDataService
  ) {}

  ngOnInit(): void {
    this.getCustomerForBatchRecord();
  }

  getCustomerForBatchRecord() {
    this.customerService.getCustomer().subscribe({
      next: (res) => {
        this.Customer = res;

        this.paginatedCustomer(res);
      },
      error: (error) => {
        console.error('Error fetching customer data:', error);
      }
    });
  }

  getBanks() {
    this.scsMaintainService.getBanks().subscribe({
      next: (res) => {
        this.batchValues = res.map((item)=>{
          return {
            id: item.empId,
            name:item.name
          }
        });
      }
    });
  }

  getBillOficers() {
    this.scsMaintainService.getBillOffciers().subscribe({
      next: (res) => {
        this.batchValues = res.map((item)=>{
         return {
          id:item.id,
          name:item.name
         }

        });
      }
    });
  }


  getMeterClasses() {
    this.controlService.getGeneralSetting('meterClass').subscribe({
      next: (res) => {
        this.batchValues = res.map((item) => {
          return {
            id: item.recordno,
            name: item.inputValues
          };
        });
        console.log(res);
      }
    });
  }
  getMeterDigit(){
    this.controlService.getGeneralSetting('meterDigit').subscribe({
      next:(res)=>{
        this.batchValues = res.map((item) => {
          return {
            id: item.recordno,
            name: item.inputValues
          };
        });
      }
    })
  }
  getPaymentDuration(){
    this.customerService.getCustomer().subscribe({
      next:(res)=>{
        this.batchValues=res.map((item)=>{
          return{
            id:item.paymentDuration,
            name:''
          }
        })
      }
    })
  }

  filter(value: string) {
    if (value == 'Bank Code') {
      this.getBanks();
    }
    if (value == 'BillSales Group') {
      this.getBillOficers();
    }
    if (value == 'Meter Class') {
      this.getMeterClasses();
    } else if (value == '') {
      this.batchValues = [];
    }
    if (value == 'Meter Digit') {
      this.getMeterDigit();
    } else if (value == '') {
      this.batchValues = [];
    }
    if (value == 'Payment Duration') {
      this.getPaymentDuration();
    } else if (value == '') {
      this.batchValues = [];
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
