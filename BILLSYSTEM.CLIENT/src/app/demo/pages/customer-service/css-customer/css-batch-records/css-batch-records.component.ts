import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';

@Component({
  selector: 'app-css-batch-records',
  templateUrl: './css-batch-records.component.html',
  styleUrls: ['./css-batch-records.component.scss']
})
export class CssBatchRecordsComponent implements OnInit {
  constructor(private modalService : NgbModal,
    private confirmationService: ConfirmationService,
    private messageService : MessageService,
    private activeModal :NgbActiveModal,
    private customerService:CssCustomerService) {}
  ngOnInit(): void {
   
  }
 filter(){

 }
 closeModal() {

  this.activeModal.close()

}

}
