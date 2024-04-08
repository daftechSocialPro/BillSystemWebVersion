import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
import { SelectList } from 'src/models/ResponseMessage.Model';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
import { ICustomerGetDto } from 'src/models/customer-service/ICustomerGetDto';
import { CustomerBillOfficerDto } from 'src/models/customer-service/ICustomerHomeDataDto';
import { IBillSectionDto } from 'src/models/system-control/IBillSectionDto';
import { IKebelesDto } from 'src/models/system-control/IKebelesDto';
import { IKetenaDto } from 'src/models/system-control/IKetenaDto';

@Component({
  selector: 'app-transfer-customer',
  templateUrl: './transfer-customer.component.html',
  styleUrls: ['./transfer-customer.component.scss']
})
export class CssTransferCustomerComponent implements OnInit {
  @Input() Customer: ICustomerDto[];
  paginationCustomer: ICustomerDto[] = [];
  filteredCustomer: ICustomerDto[] = [];
  ketenas: IKetenaDto[];
  kebeles: IKebelesDto[];
  billOfficer: SelectList[];
  totalRecords: number = 0;
  searchText: string = '';
  first: number = 0;
  rows: number = 5;
  selected: boolean = false;
  selectedBillOfficer: string = ''


  selectedCustomers :string[]=[]


  constructor(
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private activeModal: NgbActiveModal,
    private controlService: ScsDataService,
    private customerService: CssCustomerService,
    private maintainService: ScsMaintainService,
  ) { }

  ngOnInit(): void {
    // this.getCustomerForTransfer();
    this.getKetenas();
    this.getCustomers();
    this.getBillOfficers()
  }

  getCustomers() {
    this.customerService.getCustomer().subscribe({
      next: (res) => {
        this.Customer = res;

        this.paginatedCustomer(this.Customer);
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
  getBillOfficers() {
    this.maintainService.getBillOffciersForTransfer().subscribe({
      next: (res) => {
        this.billOfficer = res;
      }
    });
  }

  isTransferSelected(empID: string): boolean {
    return this.Customer.some((transfer) => transfer.billOfficerId === empID);
  }

  toggleTransfer(custId: string, empID: string, value: any): void {
    if (value.checked) {
      const transfer: ICustomerDto = {
        custId: '',
        billOfficerId: empID,
        fiscalYear: 0,
        monthIndex: 0,
        customerName: '',
        ketena: '',
        kebele: '',
        houseNo: '',
        village: '',
        telephone: '',
        bookNo: '',
        accountNo: '',
        contractNo: '',
        mapNumber: '',
        custCategoryCode: '',
        meterno: '',
        meterSizeCode: '',
        meterType: '',
        meterDigit: 0,
        meterCountryOrigin: '',
        meterModel: '',
        meterStartReading: 0,
        sdPaid: '',
        billCycle: '',
        meterClass: '',
        meterStatus: '',
        regDate: undefined,
        readerName: '',
        paymentPlace: '',
        paymentMode: '',
        bankAccount: ''
      };
      this.Customer.push(transfer);
    } else {
      this.Customer = this.Customer.filter((transfer) => transfer.billOfficerId !== empID);
    }
  }

  toggleSelectAll() {
    this.selected = !this.selected;
    this.paginationCustomer.forEach((record) => {
      record.selected = this.selected;
      var inp= {checked:this.selected}
      this.toggleContractNo(record.contractNo ,inp)
    });
    // this.filteredPermissions.forEach(permission => (permission.selected = this.selected));
  }


  
  toggleContractNo(contractNo: string ,value: any): void {
    if (value.checked) {
 

      // Add the permission to userPermissions
      this.selectedCustomers.push(contractNo);
      console.log(this.selectedCustomers)

    
    } else {
      // Remove the permission from userPermissions
      this.selectedCustomers = this.selectedCustomers.filter((item) => item !== contractNo);

      console.log(this.selectedCustomers)

      
    }
  }

  isContractNoSelected(contractNo: string): boolean {
   
    return this.selectedCustomers.some((item) => item === contractNo);
  }

  transferCustomer (){

    if (this.selectedCustomers.length==0){
      this.messageService.add({severity:'error',summary:'Verfication Failed !!!', detail:'No Customer Where Selected !!!'})
     return
    }


    var billOfficerCustomerDto : CustomerBillOfficerDto ={
      billOfficerId : this.selectedBillOfficer,
      customerContractNos :this.selectedCustomers
    }

    
    this.customerService.updateCustomerBillOfficerId(billOfficerCustomerDto).subscribe({
      next:(res)=>{
        if (res.success){
          this.messageService.add({severity:'success',summary:'Customer Transferd!!!', detail:res.message})
          this.getCustomers()
    
        }
        else {
          this.messageService.add({severity:'error',summary:'Something Went Wrong !!!', detail:res.message})
    
        }
      },error:(err)=>{
        this.messageService.add({severity:'error',summary:'Something Went Wrong !!!', detail:err.message})
      }
    })
    


  }





  filterCustomerByBillOfficer(){
    this.filteredCustomer = this.Customer.filter((item) => {
      return (item.billOfficerId != this.selectedBillOfficer)
    })
    this.paginatedCustomer(this.filteredCustomer)
  }





  filterCustomer(value: string) {

    this.filteredCustomer = this.Customer.filter((item) => {
      return (item.kebele == value)
    })

    if (this.searchText != '') {
      this.filteredCustomer = this.filteredCustomer.filter((item) => {
        return (item.customerName.toLowerCase().includes(this.searchText.toLowerCase()))
      })
    }


    this.paginatedCustomer(this.filteredCustomer)



  }

  onPageChange(event: any, gInterfaces?: ICustomerDto[]) {
    this.first = event.first;
    this.rows = event.rows;
    if (gInterfaces) {
      this.paginatedCustomer(gInterfaces);
    } else {
      this.paginatedCustomer(this.Customer);
    }
  }

  paginatedCustomer(gInterfaces: ICustomerDto[]) {
    this.totalRecords = gInterfaces.length;
    this.paginationCustomer = gInterfaces.slice(this.first, this.first + this.rows);
  }

  closeModal() {
    this.activeModal.close();
  }
}
