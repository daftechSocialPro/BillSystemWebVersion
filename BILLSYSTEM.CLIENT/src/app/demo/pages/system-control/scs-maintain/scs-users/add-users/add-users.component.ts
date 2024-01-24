import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { IGeneralInterfceDto } from 'src/models/system-control/IGeneralInterfaceDto';
import { ScsDetailPermissionComponent } from './scs-detail-permission/scs-detail-permission.component';

@Component({
  selector: 'app-add-users',
  templateUrl: './add-users.component.html',
  styleUrls: ['./add-users.component.scss']
})
export class AddUsersComponent implements OnInit {

  @Input() GeneralInterface: IGeneralInterfceDto
  GeneralInterfaceForm!: FormGroup;
  constructor(
    private modalService : NgbModal,
    private activeModal: NgbActiveModal,
    private messageService: MessageService,
    private controlService: ScsDataService,
    private formBuilder: FormBuilder) { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  addDetailPermision(){
    let modalRef=this.modalService.open(ScsDetailPermissionComponent,{size:'lg', backdrop:'static'})
  }
  closeModal() {
    this.activeModal.close()
  }
}


