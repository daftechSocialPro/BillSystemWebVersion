import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsSetupService } from 'src/app/services/system-control/scs-setup.service';
import { UserService } from 'src/app/services/user.service';
import { ICustomerMeterChangeGetDto, ICustomerMeterChangePostDto } from 'src/models/customer-service/ICustomerChangeMeterDto';
import { ICustomerGetDto } from 'src/models/customer-service/ICustomerGetDto';
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

  customerMeterChangeHistory: ICustomerMeterChangeGetDto[];
  currentMonthYear: IAccountPeriodDto;
  changeMeterForm: FormGroup;
  MCReasons: IGeneralSettingDto[];
  meterSizes: IMeterSizeDto[];
  meterDigits: IGeneralSettingDto[];
  meterType: IGeneralSettingDto[];
  meterModel: IGeneralSettingDto[];
  countryOrigin: IGeneralSettingDto[];

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
    this.getMeterSizes();
    this.getmeterType();
    this.getCountryOrgin();
    this.getmetermodel();
    this.getmeterDigit();
    this.getReasons();
    this.getCurrentFicalMonth();

    console.log("customer",this.customer)
    if (this.customer) {
      this.getCustMeterHis();
      this.changeMeterForm = this.formBuilder.group({
        reason: ['', Validators.required],

        curMeterNo: ['', Validators.required],
        curMeterSizeCode: [''],
        curMeterType: [''],
        curMeterDigit: [''],
        curMeterOrigin: [''],
        curMeterModel: [''],
        curInstallationDate: [''],
        curStartreading: [''],
        unpaidCons: ['']
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

    this.customerService.getCustomerMeterChange(this.customer.custId).subscribe({
      next: (res) => {
        this.customerMeterChangeHistory = res;

        console.log(res)
      }
    });
  }
  getReasons() {
    this.controlService.getGeneralSetting('METERCHANGEREASON').subscribe({
      next: (res) => {
        this.MCReasons = res;
      }
    });
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
        this.countryOrigin = res;
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
      var meterChangePostDto: ICustomerMeterChangePostDto = {
        fiscalYear: this.currentMonthYear.fiscalYear,
        monthIndex: this.currentMonthYear.monthIndex,
        curInstallationDate: this.changeMeterForm.value.curInstallationDate,
        curMeterDigit: this.changeMeterForm.value.curMeterDigit,
        reason: this.changeMeterForm.value.reason,
        curMeterNo: this.changeMeterForm.value.curMeterNo,
        curMeterSizeCode: this.changeMeterForm.value.curMeterSizeCode,
        curMeterType: this.changeMeterForm.value.curMeterType,
        curMeterOrigin: this.changeMeterForm.value.curMeterOrigin,
        curMeterModel: this.changeMeterForm.value.curMeterModel,
        curStartReading: this.changeMeterForm.value.curStartreading,
        unpaidCons: this.changeMeterForm.value.unpaidCons,
        custID:this.customer.custId
      };

      this.customerService.updateCustomerMeterChange(meterChangePostDto).subscribe({
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
