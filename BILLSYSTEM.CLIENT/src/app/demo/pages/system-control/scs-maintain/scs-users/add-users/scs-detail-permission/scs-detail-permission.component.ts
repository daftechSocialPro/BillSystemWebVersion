import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsMaintainService } from 'src/app/services/system-control/scs-maintain.service';
import { SelectList } from 'src/models/ResponseMessage.Model';
import { IDetailPermissionDto } from 'src/models/system-control/IBillSectionDto';
import { IUserPermissionDto, IUserSettingsDto } from 'src/models/system-control/IUserSettingsDto';

@Component({
  selector: 'app-scs-detail-permission',
  templateUrl: './scs-detail-permission.component.html',
  styleUrls: ['./scs-detail-permission.component.scss']
})
export class ScsDetailPermissionComponent implements OnInit {
  @Input() user: IUserSettingsDto;

  userPermissions: IUserPermissionDto[] = [];
  detailPermissions: IDetailPermissionDto[];
  filterdPermissions: IDetailPermissionDto[];
  appModules: SelectList[];
  appTabModules: SelectList[];
  selected: boolean = false;

  ngOnInit(): void {
    this.getDetailPermission();
    this.getAppModules();
    this.getUserPermission();
    //  this.toggleSelectAll()
    //  this.getExistingUserPermissions();
  }
  constructor(
    private modalService: NgbModal,
    private messageService: MessageService,
    private activeModal: NgbActiveModal,
    private maintainService: ScsMaintainService
  ) {}

  getDetailPermission() {
    this.maintainService.getDetailPermmission().subscribe({
      next: (res) => {
        console.log('detail permission', res);
        this.detailPermissions = res;
        this.filterdPermissions = res;
      }
    });
  }

  getAppModules() {
    this.maintainService.getAppModules().subscribe({
      next: (res) => {
        this.appModules = res;
      }
    });
  }

  getAppModulesTabs(appModule: string) {
    if (appModule == 'all') {
      this.filterdPermissions = this.detailPermissions;
    } else {
      this.filterdPermissions = this.detailPermissions.filter((x) => x.appModule == appModule);
      this.maintainService.getAppTabsByModule(appModule).subscribe({
        next: (res) => {
          this.appTabModules = res;
        }
      });
    }
  }

  onAppModuleTabsChanged(tabs: string) {
    if (tabs == 'all') {
    } else {
      this.filterdPermissions = this.filterdPermissions.filter((x) => x.appTabs == tabs);
    }
  }

  getUserPermission() {
    this.maintainService.getUserPermissions(this.user.userId).subscribe({
      next: (res) => {
        this.userPermissions = res;

        console.log('user Permssins', this.userPermissions);
      },
      error: (error) => {
        console.error('Error', error);
      }
    });
  }

  isPermissionSelected(permissionId: string): boolean {
    console.log(this.userPermissions.some((permission) => permission.buttonId === permissionId));
    return this.userPermissions.some((permission) => permission.buttonId === permissionId);
  }

  toggleSelectAll() {
    // this.selected = !this.selected;
    // this.filterdPermissions.forEach(permission => (permission.selected = this.selected));
  }

  togglePermission(recId: string, permissionId: string, value: any): void {
    if (value.checked) {
      var permission: IUserPermissionDto = {
        userId: recId,
        buttonId: permissionId
      };

      // Add the permission to userPermissions
      this.userPermissions.push(permission);

      console.log(this.userPermissions);
    } else {
      // Remove the permission from userPermissions
      this.userPermissions = this.userPermissions.filter((permission) => permission.buttonId !== permissionId);

      console.log(this.userPermissions);
    }
  }

  closeModal() {
    this.activeModal.close();
  }

  updatePermissions() {
    this.maintainService.updateUserPermissions(this.userPermissions).subscribe({
      next: (res) => {
        if (res.success) {
          this.messageService.add({ severity: 'success', summary: 'User Permission', detail: res.message });
        } else {
          this.messageService.add({ severity: 'error', summary: 'Something went wrong!!!', detail: res.message });
        }
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'User Permission', detail: err.message });
      }
    });
  }
}
