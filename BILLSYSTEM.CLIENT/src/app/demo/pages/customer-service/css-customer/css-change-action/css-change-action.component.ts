import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { DWMService } from 'src/app/services/dwm/dwm.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
import { ICustomerGetDto } from 'src/models/customer-service/ICustomerGetDto';


@Component({
  selector: 'app-css-change-action',
  templateUrl: './css-change-action.component.html',
  styleUrls: ['./css-change-action.component.scss']
})
export class CssChangeActionComponent  implements OnInit{
  Customer:ICustomerGetDto[]
  customer:ICustomerDto[]

  totalRecords: number = 0;
  searchText: string = '';
  first: number = 0;
  rows: number = 5;
  paginationCustomer: ICustomerGetDto[] = [];



  constructor(
   private activeModal: NgbActiveModal,
    private modalService: NgbModal,
    private formBuilder: FormBuilder,
    private controlService: ScsDataService,
    private dwmService: DWMService,
    private dataService: ScsDataService,
    private maintainService: ScsMaintainService,
    private customerService: CssCustomerService,
    private messageService: MessageService
  ){}
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }



  onReasonchange() {
    throw new Error('Method not implemented.');
    }
    reasons: any;

  paginatedCustomer(ginterfces: ICustomerGetDto[]) {
      this.totalRecords = ginterfces.length;
      this.paginationCustomer = ginterfces.slice(this.first, this.first + this.rows);
    }


  closeModal() {

    this.activeModal.close()

  }
}
