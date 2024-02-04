import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { DWMService } from 'src/app/services/dwm/dwm.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
import { ICustomerDto, ICustomerPostDto } from 'src/models/customer-service/ICustomerDto';
import { IMobileUsersDto } from 'src/models/dwm/IMobileUsersDto';
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

  @Input() contractNo: string
  maxOrdinaryNo: number;
  customer: ICustomerDto
  reasons:IGeneralSettingDto[]
  Customer: ICustomerDto[]
  customerForm!: FormGroup;
  billOfficer: IBillSectionDto[]
  ketenas: IKetenaDto[]
  kebeles: IKebelesDto[]
  villages: IGeneralSettingDto[]
  billDuties: IBillEmpDutiesDto[]
  onlineSalesGroups: any
  readers: IMobileUsersDto[]
  swerages: any
  billCycles: IGeneralSettingDto[]
  customerCategories: ICustomerCategoryDto[]
  meterSizes: IMeterSizeDto[]
  meterDigit: IGeneralSettingDto[]
  meterType:IGeneralSettingDto[]
  meterModel:IGeneralSettingDto[]
  months: IFiscalMonthDto[]
  countryOrgin:IGeneralSettingDto[]

  constructor(
    private activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private controlService: ScsDataService,
    private dwmService: DWMService,
    private maintainService: ScsMaintainService,
    private customerService: CssCustomerService,
    private messageService: MessageService
  ) {
    this.customerForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      meterNo: ['', Validators.required],
      meterSize: ['', Validators.required],
      customerCategories: ['',Validators.required],
      contractNo: ['',Validators.required],
      ketena: ['', Validators.required],
      kebele: ['', Validators.required],
      readerName: ['', Validators.required],
      phoneNumber: [''],
      mapNumber: [''],
      houseNumber: [''],
      village: [''],
      billCycle: [''],
      ordinaryNo: [''],
      installationDate: [''],
      updateInitial: [false],
      reason:[''],
      startReading: [''],
      sweragePaid: [''],
      monhtIndex: [''],
      fiscalYear: [''],
      billOfficerId:[''],
      accountNo:[''],
      meterDigit:[''],
      meterModel:[''],
      countryOrgin:[''],
      meterType:[''],
      paymentMode:['',]

    })
  }





  ngOnInit(): void {

    this.getSingleCustomer()
    this.getKebeles()
    this.getKetenas()
    this.getVillages()
    this.getBillCycles()
    this.getCustomerCategoriess()
    this.getMeterSizes()
    this.getMobileUsers()
    this.getBillDuties()
    this.getBillOfficers()
    this.getReasons()
    this.getMonths()
    this.getmeterType()
    this.getCountryOrgin()
    this.getmetermodel()
    this.getmeterDigit()
    this.fetchCustomers();

    if (this.customer && this.Customer.length > 0) {
      const lastCustomer = this.Customer[this.Customer.length - 1];
      if (lastCustomer && lastCustomer.ordinaryNo){
      this.customerForm.controls['ordinaryNo'].setValue(lastCustomer.ordinaryNo);
    } else{ console.log('no objects');}
  }
  }

  fetchCustomers() {
    this.customerService.getCustomer().subscribe({
      next: (customers: ICustomerDto[]) => {
        this.Customer = customers;
        this.maxOrdinaryNo = this.calculateMaxOrdinaryNo();
        //  console.log('maxOrdinaryNo:', this.maxOrdinaryNo);
        this.customerForm.controls['ordinaryNo'].setValue(this.Customer.length + 1);

      },
      error: (err) => {
        console.error('Error fetching customers:', err);
      }
    });

  }


  calculateMaxOrdinaryNo(): number {
    return this.Customer.length > 0 ? Math.max(...this.Customer.map(customer => customer.ordinaryNo)) : 0;
  }



  onKetenachange() {
    const selectedKetenaCode = this.customerForm.get('ketena').value;
    const filteredKebeles = this.kebeles.filter(item => item.ketenaCode === selectedKetenaCode);
    this.customerForm.controls['kebele'].setValue('');
    this.customerForm.controls['kebele'].setValue(filteredKebeles[0].kebeleCode)


  }

  getFilteredKebeles() {
    const selectedKetenaCode = this.customerForm.get('ketena').value;
    return this.kebeles&& this.kebeles.filter(item => item.ketenaCode === selectedKetenaCode);

  }



  getSingleCustomer():void {

    this.customerService.getSingleCustomer(this.contractNo).subscribe({
      next: (res) => {
        console.log("cust",res)
        this.customer = res
        this.customerForm.controls['fullName'].setValue(this.customer.customerName)
        this.customerForm.controls['phoneNumber'].setValue(this.customer.telephone)
        this.customerForm.controls['fiscalYear'].setValue(this.customer.regFiscalYear)
        this.customerForm.controls['monhtIndex'].setValue(this.customer.regMonthIndex)
        this.customerForm.controls['customerCategory'].setValue(this.customer.custCategoryCode)
        this.customerForm.controls['contractNo'].setValue(this.customer.contractNo)
        this.customerForm.controls['meterSize'].setValue(this.customer.meterSizeCode)
        this.customerForm.controls['readerName'].setValue(this.customer.readerName)
        this.customerForm.controls['village'].setValue(this.customer.village)
        this.customerForm.controls['billCycle'].setValue(this.customer.billCycle)
        this.customerForm.controls['ketena'].setValue(this.customer.ketena)
        this.customerForm.controls['kebele'].setValue(this.customer.kebele)
        this.customerForm.controls['installationDate'].setValue(this.customer.installationDate)
        this.customerForm.controls['ordinaryNo'].setValue(this.customer.ordinaryNo)
        this.customerForm.controls['houseNumber'].setValue(this.customer.houseNo)
        this.customerForm.controls['mapNumber'].setValue(this.customer.mapNumber)
        this.customerForm.controls['meterNo'].setValue(this.customer.meterno)
        this.customerForm.controls['sweragePaid'].setValue(this.customer.sdPaid)

      }
    })

  }


  getBillOfficers() {
    this.maintainService.getBillSection().subscribe({
      next: (res) => {
        this.billOfficer = res
      }

    })
  }
  getMonths() {
    this.controlService.getFiscalMonth().subscribe({
      next: (res) => {
        this.months = res
      }
    })
  }
  getKetenas() {
    this.controlService.getKetena().subscribe({
      next: (res) => {
        this.ketenas = res
      }
    })
  }

  getKebeles() {
    this.controlService.getKebeles().subscribe({
      next: (res) => {
        this.kebeles = res
      }
    })
  }
getReasons(){
  this .controlService.getGeneralSetting("METERCHANGEREASON").subscribe({
    next: (res) => {
      this.reasons = res
    }
  })
}
  getVillages() {

    this.controlService.getGeneralSetting("Village").subscribe({
      next: (res) => {
        this.villages = res
      }
    })
  }

  getBillCycles() {

    this.controlService.getGeneralSetting("BOOK NUMBER").subscribe({
      next: (res) => {
        this.billCycles = res
      }
    })
  }
  getCustomerCategoriess() {

    this.controlService.getCustomerCategory().subscribe({
      next: (res) => {
        this.customerCategories = res

      }
    })
  }
  getMeterSizes() {

    this.controlService.getMeterSize().subscribe({
      next: (res) => {
        this.meterSizes = res

      }
    })
  }
  getmeterDigit() {
    this.controlService.getGeneralSetting("METERDIGIT").subscribe({
      next: (res) => { this.meterDigit = res }
    })
  }

  getmeterType(){
    this.controlService.getGeneralSetting("METERTYPE").subscribe({
      next: (res) => { this.meterType = res }
    })
  }
getCountryOrgin(){
  this.controlService.getGeneralSetting("COUNTRYORIGIN").subscribe({
    next: (res) => { this.countryOrgin = res }
  })
}
  getmetermodel(){
    this.controlService.getGeneralSetting("METERMODEL").subscribe({
      next: (res) => { this.meterModel = res }
    })
  }

  getMobileUsers() {

    this.dwmService.getMobileUsers().subscribe({
      next: (res) => {
        this.readers = res

      }
    })

  }

  getBillDuties() {

    this.maintainService.getBillEmpDuties().subscribe({
      next: (res) => {
        this.billDuties = res
      }
    })

  }

  closeModal() {

    this.activeModal.close()

  }

  submit() {
    if (this.customerForm.valid) {
      const formData = this.customerForm.value;
      console.log(formData);}
    var customerPost: ICustomerDto = {
      customerName: this.customerForm.value.fullName,
      // yyyrecordno: this.customerForm.value.yyyrecordno,
      // userID: this.customerForm.value.userID,
      fiscalYear: this.customerForm.value.fiscalYear,
      monthIndex: this.customerForm.value.monhtIndex,
      custId: this.customerForm.value.custId,
      ketena: this.customerForm.value.ketena,
      kebele: this.customerForm.value.kebele,
      houseNo: this.customerForm.value.houseNo,
      village: this.customerForm.value.village,
      telephone: this.customerForm.value.telephone,
      // mobile: this.customerForm.value.mobile,
      bookNo: this.customerForm.value.bookNo,
      accountNo: this.customerForm.value.accountNo,
      contractNo: this.customerForm.value.contractNo,
      mapNumber: this.customerForm.value.mapNumber,
      custCategoryCode: this.customerForm.value.custCategoryCode,
      meterno: this.customerForm.value.meterno,
      meterSizeCode: this.customerForm.value.meterSizeCode,
      meterType: this.customerForm.value.meterType,
      meterDigit: this.customerForm.value.meterDigit,
      meterCountryOrigin: this.customerForm.value.meterCountryOrigin,
      meterModel: this.customerForm.value.meterModel,
      meterStartReading: this.customerForm.value.meterStartReading,
      sdPaid: this.customerForm.value.sdPaid,
      billCycle: this.customerForm.value.billCycle,
      meterClass: this.customerForm.value.meterClass,
      waterSource: this.customerForm.value.waterSource,
      meterStatus: this.customerForm.value.meterStatus,
      regDate: this.customerForm.value.regDate,
      paymentMode: this.customerForm.value.paymentMode,
      readerName: this.customerForm.value.readerName,
      bankAccount: this.customerForm.value.bankAccount,
      billOfficerId: this.customerForm.value.billOfficerId,
      reason: this.customerForm.value.reason,
    }
    console.log("customer post", customerPost)
    this.customerService.updateCustomer(customerPost).subscribe({
      next: (res) => {
        if (res.success) {

          this.messageService.add({ severity: 'success', summary: 'Successfully Added !!!', detail: res.message })
          this.closeModal()

        }
        else {
          this.messageService.add({ severity: 'error', summary: 'Something went Wrong !!!', detail: res.message })

        }

      }, error: (err) => {

        console.error('error', err)
        this.messageService.add({ severity: 'error', summary: 'Something went Wrong !!!', detail: err })

      }
    })

  }
}
