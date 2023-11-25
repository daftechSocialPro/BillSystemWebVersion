import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddCssCustomerComponent } from './add-css-customer/add-css-customer.component';
import { CssImportComponent } from './css-import/css-import.component';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
import { ConfirmationService, MessageService, ConfirmEventType } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';

@Component({
  selector: 'app-css-customer',
  templateUrl: './css-customer.component.html',
  styleUrls: ['./css-customer.component.scss']
})
export class CssCustomerComponent implements OnInit  {
  Customer:ICustomerDto[]

  displaySelectOption: boolean = false;
  selectedOption: string = '';
  
  filterdInterface:ICustomerDto[]=[]
  totlRecords:number =0
  searchText: string = '';
  first: number = 0;
  rows: number = 5;
  paginationCustomer:ICustomerDto[]=[];

  constructor(private modalService : NgbModal,
    private confirmationService: ConfirmationService,
    private messageService : MessageService,
    private controlService:CssCustomerService) {}
  ngOnInit(): void {
    // throw new Error('Method not implemented.');
    this.getCustomers()

  }
  
  
  filterInterfaces() {
    if (this.searchText.trim() === '') {
      this.filterdInterface = this.Customer;
    } else {
      this.filterdInterface = this.Customer.filter((item) =>
        item.customerName.toLowerCase().includes(this.searchText.toLowerCase())
      );
    }
    this.first=0;
    this.onPageChange({first:this.first,rows:this.rows},this.filterdInterface)
  }
  

  getCustomers(){

    this.controlService.getCustomer().subscribe({
      next:(res)=>{
        this.Customer = res 
        this.paginatedCustomer(this.Customer)
       }
    })
  }

  addcustomer(){

    let modalRef = this.modalService.open(AddCssCustomerComponent,  {size:'lg',backdrop:'static', windowClass: 'custom-modal-width'})

    modalRef.result.then(()=>{
    })
  }


  showSelectOption() {
    this.displaySelectOption = true;
  }

  handleOptionSelection() {
    if (this.selectedOption === 'button') {
      this.addcustomer();
    } else {
    }
  }


  cssImport(){
    let modalRef = this.modalService.open(CssImportComponent,  {size:'lg',backdrop:'static',  })

    modalRef.result.then(()=>{
    })

  }
  onPageChange(event: any,gInterface?:ICustomerDto[] ) {
    this.first = event.first;
    this.rows = event.rows;
    if(gInterface){
      this.paginatedCustomer(gInterface)
    }else{
    this.paginatedCustomer(this.Customer);
    }
  }
  paginatedCustomer(ginterfces:ICustomerDto[]) {
    this.totlRecords =ginterfces.length
    this.paginationCustomer= ginterfces.slice(this.first, this.first + this.rows);
  }


}