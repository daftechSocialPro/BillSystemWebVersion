import { Component, OnInit, Input } from '@angular/core';
import { AddUserComponent } from '../../../users/add-user/add-user.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { AddUsersComponent } from './add-users/add-users.component';
import { IUserSettingsDto } from 'src/models/system-control/IUserSettingsDto';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';

@Component({
  selector: 'app-scs-users',
  templateUrl: './scs-users.component.html',
  styleUrls: ['./scs-users.component.scss']
})
export class ScsUsersComponent implements OnInit {
  userSettings:IUserSettingsDto[]
  searchText: string = '';
  totlRecords: number = 0;
  first: number = 0;
  rows: number = 5;
  filterdInterface: IUserSettingsDto[] = []
  paginationInterfce: IUserSettingsDto[];

  @Input() isFromDashboard:boolean=false
  @Input() isFromUser:boolean=false
  
  ngOnInit(): void {
    this.getUserSettings();
  }
 
  constructor(
    private modalService: NgbModal,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private maintainService:ScsMaintainService,
    ) { }

    filterInterfaces() {
      if (this.searchText.trim() === '') {
        this.filterdInterface = this.userSettings;
      } else {
        this.filterdInterface = this.userSettings.filter((user) =>
          user.userName.toLowerCase().includes(this.searchText.toLowerCase()) ||
          user.empId.toLowerCase().includes(this.searchText.toLowerCase()) 
         
        );
      }
      this.first = 0;
      this.onPageChange({ first: this.first, rows: this.rows }, this.filterdInterface)
      // Update pagination when the search text changes
    }


    getUserSettings(){
      this.maintainService.getUserSettings().subscribe({
        next:(res)=>{

          this.userSettings = res 
          this.paginatedInterface(this.userSettings)

        }
      })
    }

  

  addUsers() {


    let modalRef = this.modalService.open(AddUsersComponent, { size: 'lg', backdrop: 'static' })

    modalRef.result.then(() => {
      this.getUserSettings()
    })

  }

 


  updateUserSetting(userSetting:IUserSettingsDto){
    let modalRef =this.modalService.open(AddUsersComponent, {size:'lg',backdrop:'static'})
    modalRef.componentInstance.userSetting=userSetting
    modalRef.result.then(()=>{
      this.getUserSettings()
    })

  }


  onPageChange(event: any, gInterface?: IUserSettingsDto[]) {
    this.first = event.first;
    this.rows = event.rows;
    if (gInterface) {
      this.paginatedInterface(gInterface)
    } else {
      this.paginatedInterface(this.userSettings);
    }
  }
  paginatedInterface(ginterfces: IUserSettingsDto[]) {
    this.totlRecords = ginterfces.length
    this.paginationInterfce = ginterfces.slice(this.first, this.first + this.rows);
  }


  removeBillOfficer(userId:number) {

  
    this.confirmationService.confirm({
      message: 'Are You sure you want to delete this User?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.maintainService.deleteSystemUsers(userId).subscribe({
          next: (res) => {

            if (res.success) {
              this.messageService.add({ severity: 'success', summary: 'Confirmed', detail: res.message });
              this.getUserSettings()
            }
            else {
              this.messageService.add({ severity: 'error', summary: 'Rejected', detail: res.message });
            }
          }, error: (err) => {

            this.messageService.add({ severity: 'error', summary: 'Rejected', detail: err });


          }
        })

      },
      reject: (type: ConfirmEventType) => {
        switch (type) {
          case ConfirmEventType.REJECT:
            this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
            break;
          case ConfirmEventType.CANCEL:
            this.messageService.add({ severity: 'warn', summary: 'Cancelled', detail: 'You have cancelled' });
            break;
        }
      },
      key: 'positionDialog'
    });

  }

}
