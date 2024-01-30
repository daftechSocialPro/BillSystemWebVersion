import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
// import { IGeneralInterfceDto } from 'src/models/system-control/IGeneralInterfaceDto';
import { ScsDetailPermissionComponent } from './scs-detail-permission/scs-detail-permission.component';
import { IuserSettingPostDto } from 'src/models/system-control/IUserSettingsDto';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
import { SelectList } from 'src/models/ResponseMessage.Model';

@Component({
  selector: 'app-add-users',
  templateUrl: './add-users.component.html',
  styleUrls: ['./add-users.component.scss']
})
export class AddUsersComponent implements OnInit {

  @Input() userSetting: IuserSettingPostDto
  userSettingForm!: FormGroup;
  emps: SelectList[]
  constructor(
    private modalService: NgbModal,
    private activeModal: NgbActiveModal,
    private messageService: MessageService,
    private maintainService: ScsMaintainService,
    private formBuilder: FormBuilder) { }
  ngOnInit(): void {

    console.log(this.userSetting)
    this.getEmpforUsers()
    if (this.userSetting) {
      this.userSettingForm = this.formBuilder.group({

        userId: [this.userSetting.userId, Validators.required],
        userName: [this.userSetting.userName, Validators.required],
        empId: [this.userSetting.empId, Validators.required],
        userLevel: [this.userSetting.userLevel, Validators.required],
        userStatus: [this.userSetting.userStatus, Validators.required],
        sysetmAdmin: [this.userSetting.sysetmAdmin, Validators.required],
        customerService: [this.userSetting.customerService, Validators.required],
        billProduce: [this.userSetting.billProduce, Validators.required],
        stockControl: [this.userSetting.stockControl, Validators.required],
        technicalService: [this.userSetting.technicalService, Validators.required],
        hrm: [this.userSetting.hrm, Validators.required],
        password: [''],
        confirmPassword: [''],
        allowKetena: [this.userSetting.allowKetenas, Validators.required],
        online: [this.userSetting.online, Validators.required],


      })
    }
    else {

      this.userSettingForm = this.formBuilder.group({

        // userId: ['', Validators.required],
        userName: ['', Validators.required],
        empId: ['', Validators.required],
        userLevel: ['', Validators.required],
        userStatus: ['', Validators.required],
        sysetmAdmin: ['', Validators.required],
        customerService: ['', Validators.required],
        billProduce: ['', Validators.required],
        stockControl: ['', Validators.required],
        technicalService: ['', Validators.required],
        hrm: ['', Validators.required],
        password: ['', Validators.required],
        confirmPassword: ['', Validators.required],
        allowKetena: ['', Validators.required],
        online: ['', Validators.required],


      })
    }





  }


  getEmpforUsers() {

    this.maintainService.getEmployeesForUserSetting().subscribe({
      next: (res) => {
        this.emps = res
      }
    })
  }
  submit() {
    console.log

    if (this.userSettingForm.valid) {

      if (this.userSettingForm.value.password != this.userSettingForm.value.confirmPassword) {

        this.messageService.add({ severity: 'error', summary: 'Password mismatch', detail: 'Please check ' })
        return
      }

      let addSystemUser: IuserSettingPostDto = {
        userId: this.userSettingForm.value.user,
        userName: this.userSettingForm.value.userName,
        userLevel: this.userSettingForm.value.userLevel,
        empId: this.userSettingForm.value.empId,
        userStatus: this.userSettingForm.value.userStatus,
        sysetmAdmin: this.userSettingForm.value.sysetmAdmin,
        customerService: this.userSettingForm.value.customerService,
        billProduce: this.userSettingForm.value.billProduce,
        stockControl: this.userSettingForm.value.stockControl,
        online: this.userSettingForm.value.online,
        password: this.userSettingForm.value.password,
        hrm: this.userSettingForm.value.hrm,
        allowKetenas: this.userSettingForm.value.allowKetena,
        technicalService: this.userSettingForm.value.technicalService


      }

      this.maintainService.createSystemUser(addSystemUser).subscribe({
        next: (res) => {

          if (res.success) {
            this.messageService.add({ severity: 'success', summary: 'Successfull', detail: res.message });

            this.closeModal()

          } else {
            this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: res.message });

          }

        }, error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: err.message });

        }
      })


    }
    else {

      this.messageService.add({ severity: 'error', summary: 'Form Validation', detail: 'form not valid!!!' });


    }
  }


  addDetailPermision() {
    let modalRef = this.modalService.open(ScsDetailPermissionComponent, { size: 'xl', backdrop: 'static' })

    modalRef.componentInstance.user= this.userSetting
  }
  update() {
    if (this.userSettingForm.valid) {

      let addUsers: IuserSettingPostDto = {
        userId: this.userSettingForm.value.userId,
        userName: this.userSettingForm.value.userName,
        empId: this.userSettingForm.value.empId,
        userLevel: this.userSettingForm.value.userLevel,
        userStatus: this.userSettingForm.value.userStatus,
        sysetmAdmin: this.userSettingForm.value.sysetmAdmin,
        customerService: this.userSettingForm.value.customerService,
        billProduce: this.userSettingForm.value.billProduce,
        stockControl: this.userSettingForm.value.stockControl,
        online: this.userSettingForm.value.online,
        password: this.userSettingForm.value.password,
        allowKetenas: this.userSettingForm.value.allowKetena,
        hrm: this.userSettingForm.value.hrm,
        technicalService: this.userSettingForm.value.technicalService,
        // recordno:this.MeterRate.recordno
      }

      this.maintainService.updateUserService(addUsers).subscribe({
        next: (res) => {

          if (res.success) {
            this.messageService.add({ severity: 'success', summary: 'Successfull', detail: res.message });

            this.closeModal()

          } else {
            this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: res.message });

          }

        }, error: (err) => {
          this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: err.message });

        }
      })


    }
    else {


    }

  }

  closeModal() {
    this.activeModal.close()
  }
}


