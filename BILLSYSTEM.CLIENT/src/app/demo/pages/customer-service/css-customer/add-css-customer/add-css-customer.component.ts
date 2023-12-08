import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { Subscriber } from 'rxjs';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
import { ICustomerCategoryDto } from 'src/models/system-control/ICustomerCategoryDto';
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

  @Input() contractNo: string
  showSection1: boolean = false;
  showSection2: boolean = true;
  Customer: ICustomerDto
  CustomerForm!: FormGroup;
  customerCategories: ICustomerCategoryDto[]
  ketena: IKetenaDto[]
  kebele: IKebelesDto[]
  village: IGeneralSettingDto[]
  billCycle: IGeneralSettingDto[]
  meterSize: IMeterSizeDto[]
  meterType: IGeneralSettingDto[]
  meterDigit: IGeneralSettingDto[]
  countryOrgin: IGeneralSettingDto[]
  meterModel: IGeneralSettingDto[]
  meterClass:IGeneralSettingDto[]
  waterSource: IGeneralSettingDto[]
  constructor(
    private activeModal: NgbActiveModal,
    private messageService: MessageService,
    private controlService: CssCustomerService,
    private controlServicedata: ScsDataService,
    private formBuilder: FormBuilder) { }
  ngOnInit(): void {
    this.CustomerForm = this.formBuilder.group({
      customerName: ['', Validators.required],
      ketena: ['', Validators.required],
      kebele: ['', Validators.required],
      houseNo: ['', Validators.required],
      village: ['', Validators.required],
      telephone: ['', Validators.required],
      mobile: ['', Validators.required],
      bookNo: ['', Validators.required],
      accountNo: ['', Validators.required],
      contractNo: ['', Validators.required],
      ordinaryNo: ['', Validators.required],
      custCategoryCode: ['', Validators.required],
      meterno: ['', Validators.required],
      meterSizeCode: ['', Validators.required],
      meterType: ['', Validators.required],
      meterDigit: ['', Validators.required],
      meterCountryOrigin: ['', Validators.required],
      meterModel: ['', Validators.required],
      installationDate: ['', Validators.required],
      readerName: ['', Validators.required],
      paymentPlace: ['', Validators.required],
      paymentDuration: ['', Validators.required],
      meterAltitude: ['', Validators.required],
      meterLongitude: ['', Validators.required],
      meterClass: ['', Validators.required],
      waterSource: ['', Validators.required],
      meterStatus: ['', Validators.required],
      sdPaid: ['', Validators.required],
      meterStartReading: ['', Validators.required],
      onlineGroup: ['', Validators.required],
      billSalesGroup: ['', Validators.required],
      bankCode: ['', Validators.required],
      paymentMode: ['', Validators.required],

    })
    
    if (this.contractNo) {
      this.controlService.getCustomerForUpdate(this.contractNo).subscribe({
        next: (res) => {
          console.log(res);
          this.Customer = res
          this.CustomerForm.controls['customerName'].setValue(this.Customer.customerName)
          this.CustomerForm.controls['ketena'].setValue(this.Customer.Ketena)
          this.CustomerForm.controls['kebele'].setValue(this.Customer.kebele)
          this.CustomerForm.controls['houseeNo'].setValue(this.Customer.houseNo)
          this.CustomerForm.controls['villagee'].setValue(this.Customer.village)
          this.CustomerForm.controls['telephone'].setValue(this.Customer.telephone)
          this.CustomerForm.controls['mobile'].setValue(this.Customer.mobile)
          this.CustomerForm.controls['bookNo'].setValue(this.Customer.bookNo)
          this.CustomerForm.controls['accountNo'].setValue(this.Customer.accountNo)
          this.CustomerForm.controls['contractNo'].setValue(this.Customer.contractNo)
          this.CustomerForm.controls['ordinaryNo'].setValue(this.Customer.ordinaryNo)
          this.CustomerForm.controls['cutomerCategoryCode'].setValue(this.Customer.custCategoryCode)
          this.CustomerForm.controls['meterno'].setValue(this.Customer.meterno)
          this.CustomerForm.controls['meterSizeCode'].setValue(this.Customer.meterSizeCode)
          this.CustomerForm.controls['meterType'].setValue(this.Customer.meterType)
          this.CustomerForm.controls['meterDigit'].setValue(this.Customer.meterDigit)
          this.CustomerForm.controls['meterCountryOrigin'].setValue(this.Customer.meterCountryOrigin)
          this.CustomerForm.controls['meterModel'].setValue(this.Customer.meterModel)
          this.CustomerForm.controls['installationDate'].setValue(this.Customer.installationDate)
          this.CustomerForm.controls['meterStartReading'].setValue(this.Customer.meterStartReading)
          this.CustomerForm.controls['meterAltitude'].setValue(this.Customer.meterAltitude)
          this.CustomerForm.controls['paymentPlace'].setValue(this.Customer.paymentPlace)
          this.CustomerForm.controls['paymentDuration'].setValue(this.Customer.paymentDuration)
          this.CustomerForm.controls['paymentMode'].setValue(this.Customer.paymentMode)
          this.CustomerForm.controls['meterAltitude'].setValue(this.Customer.meterAltitude)
          this.CustomerForm.controls['meterLongitude'].setValue(this.Customer.meterLongitude)
          this.CustomerForm.controls['meterClass'].setValue(this.Customer.meterClass)
          this.CustomerForm.controls['waterSource'].setValue(this.Customer.waterSource)
          this.CustomerForm.controls['meterStatus'].setValue(this.Customer.meterStatus)
          this.CustomerForm.controls['sdPaid'].setValue(this.Customer.sdPaid)
          this.CustomerForm.controls['onlineGroup'].setValue(this.Customer.onlineGroup)
          this.CustomerForm.controls['billSalesGroup'].setValue(this.Customer.billSalesGroup)
          this.CustomerForm.controls['bankCode'].setValue(this.Customer.bankCode)



        }
      })
    }
    this.getCustomerCategories()
    this.getKebeless()
    this.getKetenas()
    this.getVillages()
    this.getBillCycles()
    this.getMeterSizes()
    this.getMeterTypes()
    this.geteMeterDigit()
    this.getCountryOrgin()
    this.getMeterModel()
    this.getWaterSource()
    this.getMeterClass()

  }
  toggleSection(section: string) {
    if (section === 'section1') {
      this.showSection1 = true;
      this.showSection2 = false;
    } else if (section === 'section2') {
      this.showSection1 = false;
      this.showSection2 = true;
    }
  }
  getCustomerCategories() {
    this.controlServicedata.getCustomerCategory().subscribe({
      next: (res) => {
        this.customerCategories = res
      }
    })
  }
  getMeterClass(){
    this.controlServicedata.getGeneralSetting('METERCLASS').subscribe({
      next:(res)=>{this.meterClass=res}
    })
  }
  getKebeless() {
    this.controlServicedata.getKebeles().subscribe({
      next: (res) => {
        this.kebele = res
      }
    })
  }
  getKetenas() {
    this.controlServicedata.getKetena().subscribe({
      next: (res) => {
        this.ketena = res
      }
    })
  }
  getVillages() {
    this.controlServicedata.getGeneralSetting('Village').subscribe({
      next: (res) => {
        this.village = res
      }
    })
  }
  getBillCycles() {
    this.controlServicedata.getGeneralSetting('BOOK NUMBER').subscribe({
      next: (res) => {
        this.billCycle = res
      }
    })
  }

  getMeterSizes() {
    this.controlServicedata.getMeterSize().subscribe({
      next: (res) => {
        this.meterSize = res
      }
    })
  }

  getMeterTypes() {
    this.controlServicedata.getGeneralSetting('METERTYPE').subscribe({
      next: (res) => {
        this.meterType = res
      }
    })
  }
  geteMeterDigit() {
    this.controlServicedata.getGeneralSetting('METERDIGIT').subscribe({
      next: (res) => {
        this.meterDigit = res
      }
    })
  }
  getCountryOrgin() {
    this.controlServicedata.getGeneralSetting('COUNTRYORIGIN').subscribe({
      next: (res) => {
        this.countryOrgin = res
      }
    })
  }

  getMeterModel() {
    this.controlServicedata.getGeneralSetting('METERMODEL').subscribe({
      next: (res) => {
        this.meterModel = res
      }
    })
  }

  getWaterSource() {
    this.controlServicedata.getGeneralSetting('SOURCEOFWATER').subscribe({
      next: (res) => {
        this.waterSource = res
      }
    })
  }
  submit() {

    if (this.CustomerForm.valid) {
      let addcustomer: ICustomerDto = {
        customerName: this.CustomerForm.value.customerName,
        Ketena: this.CustomerForm.value.ketena,
        kebele: this.CustomerForm.value.kebele,
        houseNo: this.CustomerForm.value.houseNo,
        village: this.CustomerForm.value.village,
        telephone: this.CustomerForm.value.telephone,
        mobile: this.CustomerForm.value.mobile,
        bookNo: this.CustomerForm.value.bookNo,
        accountNo: this.CustomerForm.value.accountNo,
        contractNo: this.CustomerForm.value.contractNo,
        ordinaryNo: this.CustomerForm.value.ordinaryNo,
        custCategoryCode: this.CustomerForm.value.custCategoryCode,
        meterno: this.CustomerForm.value.meterno,
        meterSizeCode: this.CustomerForm.value.meterSizeCode,
        meterType: this.CustomerForm.value.meterType,
        meterDigit: this.CustomerForm.value.meterDigit,
        meterCountryOrigin: this.CustomerForm.value.meterCountryOrigin,
        meterModel: this.CustomerForm.value.meterModel,
        installationDate: this.CustomerForm.value.installationDate,
        meterStartReading: this.CustomerForm.value.meterStartReading,
        meterAltitude: this.CustomerForm.value.meterAltitude,
        meterLongitude: this.CustomerForm.value.meterLongitude,
        meterClass: this.CustomerForm.value.meterClass,
        waterSource: this.CustomerForm.value.waterSource,
        meterStatus: this.CustomerForm.value.meterStatus,
        sdPaid: this.CustomerForm.value.sdPaid,
        onlineGroup: this.CustomerForm.value.onlineGroup,
        billSalesGroup: this.CustomerForm.value.billSalesGroup,
        paymentMode: this.CustomerForm.value.paymentMode,
        paymentPlace: this.CustomerForm.value.paymentPlace,
        bankCode: this.CustomerForm.value.bankCode,
        paymentDuration: this.CustomerForm.value.paymentDuration,
        userID: '',
        fiscalYear: 0,
        monthIndex: 0,
        custId: '',
        materSource: '',
        regDate: undefined,
        readerName: '',
        bankAccount: '',
        trasnferBY: '',
        billOfficerId: '',
        transferDT: '',
        field01: '',
        field02: '',
        field03: 0,
        enterBy: '',
        modifyBy: '',
        modifyDate: undefined,
        dataSynched: ''
      }

      console.log(addcustomer)
      // this.controlService.addCustomer(addcustomer).subscribe({
      //   next: (res) => {

      //     if (res.success) {
      //       this.messageService.add({ severity: 'success', summary: 'Successfull', detail: res.message });

      //       this.closeModal()

      //     } else {
      //       this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: res.message });

      //     }

      //   }, error: (err) => {
      //     this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: err.message });
      //     console.log(err)
      //   }
      // })


    }  else {


    }
    
  }

  update(){

  }


closeModal(){

  this.activeModal.close()

}
}
