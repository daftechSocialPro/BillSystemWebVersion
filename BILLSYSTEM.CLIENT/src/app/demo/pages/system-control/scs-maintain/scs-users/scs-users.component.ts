import { Component, OnInit, Input } from '@angular/core';
import { AddUserComponent } from '../../../users/add-user/add-user.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { AddUsersComponent } from './add-users/add-users.component';

@Component({
  selector: 'app-scs-users',
  templateUrl: './scs-users.component.html',
  styleUrls: ['./scs-users.component.scss']
})
export class ScsUsersComponent implements OnInit {

  @Input() isFromDashboard:boolean=false
  @Input() isFromUser:boolean=false
  
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
 
  constructor(
    private modalService: NgbModal,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    // private controlService: ScsDataService
    ) { }
    

    getGeneralInterfaces(){}

  addUsers() {


    let modalRef = this.modalService.open(AddUsersComponent, { size: 'lg', backdrop: 'static' })

    modalRef.result.then(() => {
      this.getGeneralInterfaces()
    })

  }
}
