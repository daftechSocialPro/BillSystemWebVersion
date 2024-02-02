import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { DWMService } from 'src/app/services/dwm/dwm.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
import { ICustomerGetDto } from 'src/models/customer-service/ICustomerGetDto';
import { ICustomerMeterStatusGetDto } from 'src/models/customer-service/ICustomerMeterStatusDto';
import { IGeneralSettingDto } from 'src/models/system-control/IGeneralSettingDto';

@Component({
  selector: 'app-css-change-action',
  templateUrl: './css-change-action.component.html',
  styleUrls: ['./css-change-action.component.scss']
})
export class CssChangeActionComponent implements OnInit {
  @Input() customer: ICustomerGetDto;

  customerMeterStatusHistory: ICustomerMeterStatusGetDto[];

  Customer: ICustomerGetDto[];
  SCReasons:IGeneralSettingDto[]
  meterStatus: string[] = [];

  totalRecords: number = 0;
  searchText: string = '';
  first: number = 0;
  rows: number = 5;
  paginationCustomer: ICustomerGetDto[] = [];

  meterStatusForm: FormGroup;

  constructor(
    private activeModal: NgbActiveModal,

    private formBuilder: FormBuilder,

    private customerService: CssCustomerService,
    private controlService:ScsDataService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    console.log(this.customer);
    if (this.customer) {
      this.getCustMeterHis();
      this.meterStatusForm = this.formBuilder.group({
        meterStatus:['', Validators.required],
        reason: ['', Validators.required],
        entryDate: ['', Validators.required]
      });
    }
  }

  getCustMeterHis() {
    this.customerService.getCustomerMeterStatus(this.customer.custId).subscribe({
      next: (res) => {
        this.customerMeterStatusHistory = res;

        if (res) {
          if (res[0].typeOfAction == 'RECONNECT') {
            this.meterStatus = [];
            this.meterStatus.push('DISCONNECT');
            this.meterStatus.push('TERMINATE');
          }
          if (res[0].typeOfAction == 'TERMINATE') {
            this.meterStatus = [];
          }
          if (res[0].typeOfAction == 'DISCONNECT') {
            this.meterStatus = [];
            this.meterStatus.push('RECONNECT');
            this.meterStatus.push('TERMINATE');
          }
        } else {
          this.meterStatus.push('DISCONNECT');
          this.meterStatus.push('TERMINATE');
        }
      }
    });
  }
getSCReasons(reason:string){
        this.controlService.getGeneralSetting(reason).subscribe({
      next:(res)=>{
        this.SCReasons = res
      }
    })
  }
  onReasonchange() {
    throw new Error('Method not implemented.');
  }


  paginatedCustomer(ginterfces: ICustomerGetDto[]) {
    this.totalRecords = ginterfces.length;
    this.paginationCustomer = ginterfces.slice(this.first, this.first + this.rows);
  }

  closeModal() {
    this.activeModal.close();
  }
}
