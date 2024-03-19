import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { DWMService } from 'src/app/services/dwm/dwm.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
import { ScsSetupService } from 'src/app/services/system-control/scs-setup.service';
import { UserService } from 'src/app/services/user.service';
import { UserView } from 'src/models/auth/userDto';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
import { ICustomerGetDto } from 'src/models/customer-service/ICustomerGetDto';
import { ICustomerMeterStatusGetDto, ICustomerMeterStatusPostDto } from 'src/models/customer-service/ICustomerMeterStatusDto';
import { IAccountPeriodDto } from 'src/models/system-control/IAccountPeriod';
import { IFiscalMonthDto } from 'src/models/system-control/IFiscalMonthDto';
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
  SCReasons: IGeneralSettingDto[];
  meterStatus: string[] = [];

  totalRecords: number = 0;
  searchText: string = '';
  first: number = 0;
  rows: number = 5;
  paginationCustomer: ICustomerGetDto[] = [];

  meterStatusForm: FormGroup;

  userview: UserView;

  currentMonthYear: IAccountPeriodDto;

  constructor(
    private activeModal: NgbActiveModal,

    private formBuilder: FormBuilder,

    private customerService: CssCustomerService,
    private controlService: ScsDataService,
    private setUpService: ScsSetupService,
    private messageService: MessageService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    console.log(this.customer);

    this.userview= this.userService.getCurrentUser()
    console.log(this.userview)
    this.getCurrentFicalMonth()
    if (this.customer) {
      this.getCustMeterHis();
      this.meterStatusForm = this.formBuilder.group({
        meterStatus: ['', Validators.required],
        reason: ['', Validators.required],
        entryDate: ['', Validators.required]
      });
    }
  }

  getCurrentFicalMonth() {
    this.setUpService.getAccountPeriod().subscribe({
      next: (res) => {
        this.currentMonthYear = res;
      }
    });
  }
  getCustMeterHis() {
    this.customerService.getCustomerMeterStatus(this.customer.custId).subscribe({
      next: (res) => {
        this.customerMeterStatusHistory = res;

        if (res[0]) {
          if (res[0].typeOfAction == 'RECONNECT') {
            this.meterStatus = [];
            this.meterStatus.push('DISCONNETREASON');
            this.meterStatus.push('METERTERMINATEREASON');
          }
          if (res[0].typeOfAction == 'TERMINATE') {
            this.meterStatus = [];
          }
          if (res[0].typeOfAction == 'DISCONNECT') {
            this.meterStatus = [];
            this.meterStatus.push('RECONNECTREASON');
            this.meterStatus.push('METERTERMINATEREASON');
          }
        } else {
          this.meterStatus.push('DISCONNETREASON');
          this.meterStatus.push('METERTERMINATEREASON');
        }
      }
    });
  }



  paginatedCustomer(ginterfces: ICustomerGetDto[]) {
    this.totalRecords = ginterfces.length;
    this.paginationCustomer = ginterfces.slice(this.first, this.first + this.rows);
  }

  closeModal() {
    this.activeModal.close();
  }
  getReasons(value:string){



      this.controlService.getGeneralSetting(value).subscribe({
        next:(res)=>{
          this.SCReasons = res
        }
      })

  }
  UpdateMeterStatus(){
    if (this.meterStatusForm.valid) {

      var typeOfAction = ''
      if(this.meterStatusForm.value.meterStatus==='DISCONNETREASON'){
        typeOfAction= 'DISCONNECT'
      }
      if(this.meterStatusForm.value.meterStatus==='METERTERMINATEREASON'){
        typeOfAction= 'TERMINATE'
      }
      if(this.meterStatusForm.value.meterStatus==='RECONNECTREASON'){
        typeOfAction= 'RECONNECT'
      }

      var meterStatusPostDto:ICustomerMeterStatusPostDto ={

        fiscalYear : this.currentMonthYear.fiscalYear,
        monthIndex : this.currentMonthYear.monthIndex,
        custId : this.customer.custId,
        disDate:this.meterStatusForm.value.entryDate,
        reason: this.meterStatusForm.value.reason,
        typeOfAction : typeOfAction,
        enterBy:this.userview.userId
      }
    };
  }
  // UpdateMeterStatus() {
  //   if (this.meterStatusForm.valid) {
  //     var meterStatusPostDto: ICustomerMeterStatusPostDto = {
  //       fiscalYear: this.currentMonthYear.fiscalYear,
  //       monthIndex: this.currentMonthYear.monthIndex,
  //       custId: this.customer.custId,
  //       disDate: this.meterStatusForm.value.entryDate,
  //       reason: this.meterStatusForm.value.reason,
  //       typeOfAction: this.meterStatusForm.value.meterStatus
  //     };

  //     this.customerService.updateCustomerMeterStatus(meterStatusPostDto).subscribe({
  //       next:((res)=>{
  //         this.messageService.add({ severity:'success', summary: 'Success', detail: 'Meter Status Updated Successfully' });
  //         this.getCustMeterHis();
  //       })
  //     })





  // }}

}
