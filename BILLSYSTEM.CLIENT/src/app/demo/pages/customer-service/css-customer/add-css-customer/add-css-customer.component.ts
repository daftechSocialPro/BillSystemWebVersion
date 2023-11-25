import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-css-customer',
  templateUrl: './add-css-customer.component.html',
  styleUrls: ['./add-css-customer.component.scss']
})
export class AddCssCustomerComponent {

  constructor(
    private activeModal : NgbActiveModal){}

    closeModal(){

      this.activeModal.close()
    
    }
}
