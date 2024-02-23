import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsSetupService } from 'src/app/services/system-control/scs-setup.service';
import { UserService } from 'src/app/services/user.service';
import { ICustomerMeterChangePostDto } from 'src/models/customer-service/ICustomerChangeMeterDto';
import { ICustomerGetDto } from 'src/models/customer-service/ICustomerGetDto';
import { ICustomerMeterStatusGetDto } from 'src/models/customer-service/ICustomerMeterStatusDto';
import { IAccountPeriodDto } from 'src/models/system-control/IAccountPeriod';
import { IGeneralSettingDto } from 'src/models/system-control/IGeneralSettingDto';
import { IMeterSizeDto } from 'src/models/system-control/IMeterSizeDto';

@Component({
  selector: 'app-css-change-meter',
  templateUrl: './css-change-meter.component.html',
  styleUrls: ['./css-change-meter.component.scss']
})
export class CssChangeMeterComponent implements OnInit {
  @Input() customer: ICustomerGetDto;

  customerMeterChangeHistory: ICustomerMeterStatusGetDto[];
  currentMonthYear : IAccountPeriodDto
  changeMeterForm: FormGroup;
  MCReasons: IGeneralSettingDto[]
  meterSizes: IMeterSizeDto[];
  meterDigits: IGeneralSettingDto[];
  meterType: IGeneralSettingDto[];
  meterModel: IGeneralSettingDto[];
  countryOrgin: IGeneralSettingDto[];


  constructor(
    private activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private customerService: CssCustomerService,
    private controlService:ScsDataService,
    private setUpService : ScsSetupService,
    private messageService: MessageService,
    private userService : UserService
  ) {}

  ngOnInit(): void {
    this.getMeterSizes();
    this.getmeterType();
    this.getCountryOrgin();
    this.getmetermodel();
    this.getmeterDigit();
    this.getReasons();
    this.getCurrentFicalMonth()
    if (this.customer) {
      this.getCustMeterHis();
      this.changeMeterForm = this.formBuilder.group({
        reason: ['', Validators.required],
        entryDate: ['', Validators.required],
        curMeterNo:[''],
        curMeterSizeCode:[''],
        curMeterType:[''],
        curMeterDigit:[''],
        curMeterOrigin:[''],
        curMeterModel:[''],
        curInstallationDate:[''],
        curStartreading: ['']
      });
    }
  }

  getCurrentFicalMonth(){
    this.setUpService.getAccountPeriod().subscribe({

      next: (res) => {
        this.currentMonthYear = res;
      }
    })
  }

  getCustMeterHis(){

  }
  getReasons(){
    this.controlService.getGeneralSetting('METERCHANGEREASON').subscribe({
      next:(res)=>{
        this.MCReasons = res;
      }
    })
}
getMeterSizes() {
  this.controlService.getMeterSize().subscribe({
    next: (res) => {
      this.meterSizes = res;
    }
  });
}
getmeterDigit() {
  this.controlService.getGeneralSetting('METERDIGIT').subscribe({
    next: (res) => {
      this.meterDigits = res;
    }
  });
}
getmeterType() {
  this.controlService.getGeneralSetting('METERTYPE').subscribe({
    next: (res) => {
      this.meterType = res;
    }
  });
}
getCountryOrgin() {
  this.controlService.getGeneralSetting('COUNTRYORIGIN').subscribe({
    next: (res) => {
      this.countryOrgin = res;
    }
  });
}
getmetermodel() {
  this.controlService.getGeneralSetting('METERMODEL').subscribe({
    next: (res) => {
      this.meterModel = res;
    }
  });
}

UpdateMeter() {
  if (this.changeMeterForm.valid) {
    var meterStatusPostDto: ICustomerMeterChangePostDto = {
      fiscalYear: this.currentMonthYear.fiscalYear,
      monthIndex: this.currentMonthYear.monthIndex,
      custId: this.customer.custId,
      disDate: this.changeMeterForm.value.entryDate,
      reason: this.changeMeterForm.value.reason,
      typeOfAction: this.changeMeterForm.value.meterStatus
    };

    this.customerService.updateCustomerMeterStatus(meterStatusPostDto).subscribe({
      next: (res) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Meter Status Updated Successfully' });
        this.closeModal();
      }
    });
  }
}
closeModal() {
  this.activeModal.close();
}

}
