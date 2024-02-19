import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { DWMService } from 'src/app/services/dwm/dwm.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
import { ICustomerDto, ICustomerPostDto } from 'src/models/customer-service/ICustomerDto';
import { IMobileUsersDto } from 'src/models/dwm/IMobileUsersDto';
import { IAccountPeriodDto } from 'src/models/system-control/IAccountPeriod';
import { IBillEmpDutiesDto } from 'src/models/system-control/IBillEmpDutiesDto';
import { ICustomerCategoryDto } from 'src/models/system-control/ICustomerCategoryDto';
import { IFiscalMonthDto } from 'src/models/system-control/IFiscalMonthDto';
import { IGeneralSettingDto } from 'src/models/system-control/IGeneralSettingDto';
import { IKebelesDto } from 'src/models/system-control/IKebelesDto';
import { IKetenaDto } from 'src/models/system-control/IKetenaDto';
import { IMeterSizeDto } from 'src/models/system-control/IMeterSizeDto';

@Component({
  selector: 'app-add-css-customer',
  templateUrl: './add-css-customer.component.html',
  styleUrls: ['./add-css-customer.component.scss']
})
export class AddCssCustomerComponent implements OnInit {
  customerForm!: FormGroup;
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
  months: IFiscalMonthDto[];

  @Input() contractNo: string;
  maxOrdinaryNo: number;
  showSection1: boolean = false;
  showSection2: boolean = true;
  selectedKebele: number;
  Customer: ICustomerDto[];
  CustomerForm!: FormGroup;
  ketena: IKetenaDto[];
  kebele: IKebelesDto[];
  village: IGeneralSettingDto[];
  billCycle: IGeneralSettingDto[];
  meterSize: IMeterSizeDto[];
  meterType: IGeneralSettingDto[];
  meterDigit: IGeneralSettingDto[];
  countryOrgin: IGeneralSettingDto[];
  meterModel: IGeneralSettingDto[];
  meterClass: IGeneralSettingDto[];
  waterSource: IGeneralSettingDto[];

  constructor(
    private activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private controlService: ScsDataService,
    private dwmService: DWMService,
    private dataService: ScsDataService,
    private maintainService: ScsMaintainService,
    private customerService: CssCustomerService,
    private messageService: MessageService
  ) {
    this.customerForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      customerCategory: ['', Validators.required],
      readerName: ['', Validators.required],
      meterNo: ['', Validators.required],
      meterSize: ['', Validators.required],
      contractNo: ['', Validators.required],
      phoneNumber: [''],
      ketena: ['', Validators.required],
      kebele: ['', Validators.required],
      village: [''],
      mapNumber: [''],
      houseNumber: [''],
      billCycle: [''],
      ordinaryNo: [''],
      installationDate: [''],
      updateInitial: [false],
      startReading: [''],
      sweragePaid: [''],
      monhtIndex: [''],
      fiscalYear: ['']
    });
  }

  ngOnInit(): void {
    this.getKebeles();
    this.getKetenas();
    this.getVillages();
    this.getBillCycles();
    this.getCustomerCategoriess();
    this.getMeterSizes();
    this.getMobileUsers();
    this.getBillDuties();
    this.getMonths();
    this.fetchCustomers();

    if (this.Customer && this.Customer.length > 0) {
      const lastCustomer = this.Customer[this.Customer.length - 1];
      this.customerForm.get('ordinaryNo').setValue(lastCustomer.ordinaryNo);
    }
  }

  fetchCustomers() {
    this.customerService.getCustomer().subscribe({
      next: (customers: ICustomerDto[]) => {
        this.Customer = customers;
        this.maxOrdinaryNo = this.calculateMaxOrdinaryNo();
        //  console.log('maxOrdinaryNo:', this.maxOrdinaryNo);
        this.customerForm.get('ordinaryNo').setValue(this.Customer.length + 1);
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

  onKetenachange() {
    const selectedKetenaCode = this.customerForm.get('ketena').value;
    const filteredKebeles = this.kebeles.filter((item) => item.ketenaCode === selectedKetenaCode);
    this.customerForm.get('kebele').setValue('');
    this.customerForm.get('kebele').patchValue(filteredKebeles[0].kebeleCode);
  }

  getFilteredKebeles() {
    const selectedKetenaCode = this.customerForm.get('ketena').value;

    return this.kebeles.filter((item) => item.ketenaCode === selectedKetenaCode);
  }

  ////////////

  // calculateMaxOrdinaryNo(customers: ICustomerDto[]): number {
  //   return customers.length > 0 ? Math.max(...customers.map(customer => customer.ordinaryNo)) : 0;
  // }
  // onKetenachanges() {
  //   const selectedKetenaCode = this.customerForm.get('ketena').value;
  //   const selectedKebeleCode = this.customerForm.get('kebele').value;

  //   this.fetchCustomersByKetenaAndKebele(selectedKetenaCode, selectedKebeleCode);
  // }

  // fetchCustomersByKetenaAndKebele(ketenaCode: number, kebeleCode: number) {
  //   this.customerService.getCustomer().subscribe({
  //     next: (customers: ICustomerDto[]) => {
  //       const filteredCustomers = customers.filter(
  //         customer => customer.ketena.ketenaCode === ketenaCode && customer.kebele.kebeleCode === kebeleCode
  //       );
  //       this.maxOrdinaryNo = this.calculateMaxOrdinaryNo(filteredCustomers);
  //       this.customerForm.get('ordinaryNo').setValue(filteredCustomers.length + 1);
  //     },
  //     error: (err) => {
  //       console.error('Error fetching customers:', err);
  //     }
  //   });
  // }

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

  getKebeles() {
    this.controlService.getKebeles().subscribe({
      next: (res) => {
        this.kebeles = res;
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

  closeModal() {
    this.activeModal.close();
  }

  submit() {
    var customerPost: ICustomerDto = {
      customerName: this.customerForm.value.fullName,
      telephone: this.customerForm.value.phoneNumber,
      ketena: this.customerForm.value.ketena,
      kebele: this.customerForm.value.kebele,
      readerName: this.customerForm.value.readerName,
      village: this.customerForm.value.village,
      mapNumber: this.customerForm.value.mapNumber,
      houseNo: this.customerForm.value.houseNumber,
      billCycle: this.customerForm.value.billCycle,
      custCategoryCode: this.customerForm.value.customerCategory,
      contractNo: this.customerForm.value.contractNo,
      ordinaryNo: this.customerForm.value.ordinaryNo,
      meterno: this.customerForm.value.meterNo,
      meterSizeCode: this.customerForm.value.meterSize,
      meterType: this.customerForm.value.meterType,
      installationDate: this.customerForm.value.installationDate,
      // upd: this.customerForm.value.updateInitial,
      // sta: this.customerForm.value.startReading,
      sdPaid: this.customerForm.value.sweragePaid,
      monthIndex: this.customerForm.value.monhtIndex,
      fiscalYear: this.customerForm.value.fiscalYear,
      billOfficerId: this.customerForm.value.billOfficerId,
      custId: this.customerForm.value.custId,
      reason: this.customerForm.value.reason,
      bookNo: this.customerForm.value.bookNo,
      accountNo: this.customerForm.value.accountNo,
      meterDigit: this.customerForm.value.meterDigit,
      meterCountryOrigin: this.customerForm.value.meterCountryOrigin,
      meterModel: this.customerForm.value.meterModel,
      meterStartReading:this.customerForm.value.meterStartReading,
      meterClass: this.customerForm.value.meterClass,
      waterSource: this.customerForm.value.waterSource,
      meterStatus: this.customerForm.value.meterStatus,
      regDate: this.customerForm.value.regDate,
      paymentMode: this.customerForm.value.paymentMode,
      paymentPlace: this.customerForm.value.paymentPlace,
      bankAccount: this.customerForm.value.bankAccount,
    };
    console.log('customer post', customerPost);
    this.customerService.createCustomer(customerPost).subscribe({
      next: (res) => {
        if (res.success) {
          this.messageService.add({ severity: 'success', summary: 'Successfully Added !!!', detail: res.message });
          this.closeModal();
        } else {
          this.messageService.add({ severity: 'error', summary: 'Something went Wrong !!!', detail: res.message });
        }
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Something went Wrong !!!', detail: err });
      }
    });
  }
}
