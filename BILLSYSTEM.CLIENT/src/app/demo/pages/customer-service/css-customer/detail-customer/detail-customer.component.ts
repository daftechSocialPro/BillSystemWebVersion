import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { DWMService } from 'src/app/services/dwm/dwm.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
import { ScsSetupService } from 'src/app/services/system-control/scs-setup.service';
import { ICustomerDto, ICustomerPostDto } from 'src/models/customer-service/ICustomerDto';
import { IMobileUsersDto } from 'src/models/dwm/IMobileUsersDto';
import { IAccountPeriodDto } from 'src/models/system-control/IAccountPeriod';
import { IBillEmpDutiesDto } from 'src/models/system-control/IBillEmpDutiesDto';
import { IBillSectionDto } from 'src/models/system-control/IBillSectionDto';
import { ICustomerCategoryDto } from 'src/models/system-control/ICustomerCategoryDto';
import { IFiscalMonthDto } from 'src/models/system-control/IFiscalMonthDto';
import { IGeneralSettingDto } from 'src/models/system-control/IGeneralSettingDto';
import { IKebelesDto } from 'src/models/system-control/IKebelesDto';
import { IKetenaDto } from 'src/models/system-control/IKetenaDto';
import { IMeterSizeDto } from 'src/models/system-control/IMeterSizeDto';

@Component({
  selector: 'app-detail-customer',
  templateUrl: './detail-customer.component.html',
  styleUrls: ['./detail-customer.component.scss']
})
export class DetailCustomerComponent implements OnInit {
  @Input() contractNo: string;
  maxOrdinaryNo: number;
  customer: ICustomerDto;
  reasons: IGeneralSettingDto[];
  Customer: ICustomerDto[];
  customerForm!: FormGroup;
  billOfficer: IBillSectionDto[];
  ketenas: IKetenaDto[];
  kebeles: IKebelesDto[];
  villages: IGeneralSettingDto[];
  billDuties: IBillEmpDutiesDto[];
  onlineSalesGroups: any;
  readers: IMobileUsersDto[];
  swerages: any;
  billCycles: IGeneralSettingDto[];
  customerCategories: ICustomerCategoryDto[];
  meterSizes: IMeterSizeDto[];
  meterDigits: IGeneralSettingDto[];
  meterType: IGeneralSettingDto[];
  meterModel: IGeneralSettingDto[];
  months: IFiscalMonthDto[];
  countryOrgin: IGeneralSettingDto[];

  accountPeriod : IAccountPeriodDto

  constructor(
    private activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private controlService: ScsDataService,
    private dwmService: DWMService,
    private setupService :ScsSetupService,
    private maintainService: ScsMaintainService,
    private customerService: CssCustomerService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.customerForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      meterNo: [''],
      meterSize: ['', Validators.required],
      customerCategory: ['', Validators.required],
      contractNo: [''],
      ketena: ['', Validators.required],
      kebele: ['', Validators.required],
      billOfficer: [''],
      //readerName: ['', Validators.required],
      phoneNumber: [''],
      mapNumber: [''],
      houseNumber: [''],
      village: [''],
      billCycle: [''],
      ordinaryNo: [''],
      installationDate: [''],
      updateInitial: [false],
      reason: [''],
      startReading: [''],
      sweragePaid: [''],
      monhtIndex: [''],
      fiscalYear: [''],
      billOfficerId: [''],
      accountNo: [''],
      meterDigit: [''],
      meterModel: [''],
      countryOrgin: [''],
      meterType: [''],
      paymentMode: ['']
    });

    this.getKetenas();
    this.getVillages();
    this.getBillCycles();
    this.getCustomerCategoriess();
    this.getMeterSizes();
    this.getMobileUsers();
    this.getBillDuties();
    this.getBillOfficers();
    this.getReasons();
    this.getMonths();
    this.getmeterType();
    this.getCountryOrgin();
    this.getmetermodel();
    this.getmeterDigit();
    this.getAccountPeriod();


    if(!this.contractNo){
      this.fetchCustomers();
    }
    if(this.contractNo){

      this.getSingleCustomer();
    }


    if (this.customer && this.Customer.length > 0) {
      const lastCustomer = this.Customer[this.Customer.length - 1];
      if (lastCustomer && lastCustomer.ordinaryNo) {
        this.customerForm.controls['ordinaryNo'].setValue(lastCustomer.ordinaryNo);
      } else {
        console.log('no objects');
      }
    }
  }


  getAccountPeriod(){
    this.setupService.getAccountPeriod().subscribe({
      next:(res)=>{
        this.accountPeriod=res

        this.customerForm.controls['monhtIndex'].setValue(res.monthIndex)
        this.customerForm.controls['fiscalYear'].setValue(res.fiscalYear)
      }
    })
  }

  getSingleCustomer() {

      this.customerService.getSingleCustomer(this.contractNo).subscribe({
        next: (customer: ICustomerDto) => {
          this.customer = customer;
          this.getKebelesForEdit(this.customer.ketena)
          console.log("this.customer",this.customer)
          if (this.customer) {
            this.customerForm.get('fullName').setValue(this.customer.customerName);
            this.customerForm.get('phoneNumber').setValue(this.customer.telephone);
            this.customerForm.get('fiscalYear').setValue(this.customer.regFiscalYear);
            this.customerForm.get('monhtIndex').setValue(this.customer.regMonthIndex);
            this.customerForm.get('customerCategory').setValue(this.customer.custCategoryCode);
            this.customerForm.get('contractNo').setValue(this.customer.contractNo);
            this.customerForm.get('meterSize').setValue(this.customer.meterSizeCode);
            this.customerForm.get('billOfficerId').setValue(this.customer.readerName);
            this.customerForm.get('village').setValue(this.customer.village);
            this.customerForm.get('billCycle').setValue(this.customer.billCycle);
            this.customerForm.get('ketena').setValue(this.customer.ketena);
            this.customerForm.get('kebele').setValue(this.customer.kebele);
            this.customerForm.get('installationDate').setValue(this.customer.installationDate);
            this.customerForm.get('ordinaryNo').setValue(this.customer.ordinaryNo);
            this.customerForm.get('houseNumber').setValue(this.customer.houseNo);
            this.customerForm.get('mapNumber').setValue(this.customer.mapNumber);
            this.customerForm.get('meterNo').setValue(this.customer.meterno);
            this.customerForm.get('sweragePaid').setValue(this.customer.sdPaid);


          }
          console.log('info', this.customer);
        },
        error: (err) => {
          console.error('Error updating customer:', err);
        }
      });

  }

  fetchCustomers() {
    this.customerService.getCustomer().subscribe({
      next: (customers: ICustomerDto[]) => {
        this.Customer = customers;

        this.maxOrdinaryNo = this.calculateMaxOrdinaryNo();
        this.customerForm.controls['ordinaryNo'].setValue(this.Customer.length + 1);
      },
      error: (err) => {
        console.error('Error fetching customers:', err);
      }
    });
  }

  onKebeleChange(ketena: string, kebele: string) {
    this.customerService.getContractNumber(kebele, ketena).subscribe({
      next: (res) => {
        console.log(res);
        this.customerForm.get('contractNo').setValue(res.toString());
      },
      error: (err) => {
        this.customerForm.get('contractNo').setValue('');
      }
    });
  }

  calculateMaxOrdinaryNo(): number {
    return this.Customer.length > 0 ? Math.max(...this.Customer.map((customer) => customer.ordinaryNo)) : 0;
  }
  onKetenachange(event: any) {
    const ketenaCode = event.target.value;
    console.log('const ketenaCode', ketenaCode);
    this.controlService.getKetenaKebeles(ketenaCode).subscribe({
      next: (res) => {
        console.log(res);
        this.kebeles = res;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  getKebelesForEdit(ketenaCode: string){
    this.controlService.getKetenaKebeles(ketenaCode).subscribe({
      next: (res) => {
        console.log(res);
        this.kebeles = res;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  getValue(value: string) {
    return Number(value);
  }

  getBillOfficers() {
    this.maintainService.getBillSection().subscribe({
      next: (res) => {
        this.billOfficer = res;
      }
    });
  }
  getMonths() {
    this.controlService.getFiscalMonth().subscribe({
      next: (res) => {
        this.months = res;
      }
    });
  }
  getKetenas() {
    this.controlService.getKetena().subscribe({
      next: (res) => {
        this.ketenas = res;
      }
    });
  }
  getReasons() {
    this.controlService.getGeneralSetting('METERCHANGEREASON').subscribe({
      next: (res) => {
        this.reasons = res;
      }
    });
  }
  getVillages() {
    this.controlService.getGeneralSetting('Village').subscribe({
      next: (res) => {
        this.villages = res;
      }
    });
  }
  getBillCycles() {
    this.controlService.getGeneralSetting('BOOK NUMBER').subscribe({
      next: (res) => {
        this.billCycles = res;
      }
    });
  }
  getCustomerCategoriess() {
    this.controlService.getCustomerCategory().subscribe({
      next: (res) => {
        this.customerCategories = res;
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
  getMobileUsers() {
    this.dwmService.getMobileUsers().subscribe({
      next: (res) => {
        this.readers = res;
      }
    });
  }
  getBillDuties() {
    this.maintainService.getBillEmpDuties().subscribe({
      next: (res) => {
        this.billDuties = res;
      }
    });
  }

  submit() {
    console.log('customerData', this.customerForm.value);
    if (this.customerForm.valid) {
      if (this.customer) {
        const customerData: ICustomerDto = {
          regFiscalYear: this.customerForm.value.fiscalYear,
          regMonthIndex: this.customerForm.value.monhtIndex,
          customerName: this.customerForm.value.fullName,


paymentMode:this.customerForm.value.paymentMode,
mapNumber:this.customerForm.value.mapNumber,
billCycle : this.customerForm.value.billCycle,
accountNo : this.customerForm.value.accountNo,
meterDigit:this.customerForm.value.meterDigit,
meterType : this.customerForm.value.meterType,
meterCountryOrigin : this.customerForm.value.countryOrgin,
meterModel : this.customerForm.value.meterModel,



          ketena: this.customerForm.value.ketena,
          kebele: this.customerForm.value.kebele,
          houseNo: this.customerForm.value.houseNumber,
          village: this.customerForm.value.village,
          telephone: this.customerForm.value.phoneNumber,
          contractNo: this.customerForm.value.contractNo,
          readerName: this.customerForm.value.readerName,
          ordinaryNo: this.customerForm.value.ordinaryNo,
          custCategoryCode: this.customerForm.value.customerCategory,
          meterno: this.customerForm.value.meterno,
          meterSizeCode: this.customerForm.value.meterSize,
          installationDate: this.customerForm.value.installationDate,
          meterStartReading: this.customerForm.value.startReading,
          sdPaid: this.customerForm.value.sweragePaid
        };

        console.log('customerData2', customerData);
        this.customerService.updateCustomer(customerData).subscribe({
          next: (res) => {
            if (res.success) {
              this.messageService.add({ severity: 'success', summary: 'Successfull', detail: res.message });

              this.closeModal();
            } else {
              this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: res.message });
            }
          },
          error: (err) => {
            this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: err.message });
          }
        });
      } else {
        const customerData: ICustomerDto = {
          regFiscalYear: this.customerForm.value.fiscalYear,
          regMonthIndex: this.customerForm.value.monhtIndex,
          customerName: this.customerForm.value.fullName,
          ketena: this.customerForm.value.ketena,
          kebele: this.customerForm.value.kebele,
          houseNo: this.customerForm.value.houseNumber,
          village: this.customerForm.value.village,
          telephone: this.customerForm.value.phoneNumber,
          contractNo: this.customerForm.value.contractNo,
          readerName: this.customerForm.value.billOfficerId,
          ordinaryNo: this.customerForm.value.ordinaryNo,
          custCategoryCode: this.customerForm.value.customerCategory,
          meterno: this.customerForm.value.meterNo,
          meterSizeCode: this.customerForm.value.meterSize,
          installationDate: this.customerForm.value.installationDate,
          meterStartReading: this.customerForm.value.startReading,
          sdPaid: this.customerForm.value.sweragePaid,
          paymentMode:this.customerForm.value.paymentMode,
mapNumber:this.customerForm.value.mapNumber,
billCycle : this.customerForm.value.billCycle,
accountNo : this.customerForm.value.accountNo,
meterDigit:this.customerForm.value.meterDigit,
meterType : this.customerForm.value.meterType,
meterCountryOrigin : this.customerForm.value.countryOrgin,
meterModel : this.customerForm.value.meterModel,
        };
        console.log('customerData', customerData);
        this.customerService.createCustomer(customerData).subscribe({
          next: (response) => {
            if (response.success) {
              this.messageService.add({ severity: 'success', summary: 'Successfull', detail: response.message });

              this.closeModal();
            } else {
              this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: response.message });
            }
          },
          error: (err) => {
            console.error('Error adding customer:', err);
          }
        });
      }
    } else {
      console.log('asdfghjkl;');
    }

    console.log(this.customerForm);
  }

  closeModal() {
    this.activeModal.close();
  }
}
