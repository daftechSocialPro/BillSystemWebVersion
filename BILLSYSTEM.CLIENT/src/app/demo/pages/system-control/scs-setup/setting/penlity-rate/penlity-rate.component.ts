import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, MessageService, ConfirmEventType } from 'primeng/api';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { IPenalityRateDto } from 'src/models/system-control/IPenalityRateDto';
import { AddMeterRateComponent } from '../../../scs-maintain/scs-rate/meter-rate/add-meter-rate/add-meter-rate.component';
import { AddPenalityRateComponent } from './add-penality-rate/add-penality-rate.component';

@Component({
  selector: 'app-penlity-rate',
  templateUrl: './penlity-rate.component.html',
  styleUrls: ['./penlity-rate.component.scss']
})
export class PenlityRateComponent implements OnInit {

  MeterRates:IPenalityRateDto[]
  first: number = 0;
  rows: number = 5;
  paginationMeterRate:IPenalityRateDto[];
  ngOnInit(): void {

    this.getMeterRates()
    
  }

  constructor(
    private modalService : NgbModal,
    private confirmationService: ConfirmationService,
    private messageService : MessageService,
    private controlService:ScsDataService){}


  getMeterRates(){

    this.controlService.getPenalityRates().subscribe({
      next:(res)=>{
        console.log(res)
        this.MeterRates = res 
        this.paginatedMeterRate()
      }
    })
  }

  addMeterRate(){


    let modalRef = this.modalService.open(AddPenalityRateComponent,  {size:'lg',backdrop:'static'})

    modalRef.result.then(()=>{
      this.getMeterRates()
    })

  }

  removeMeterRate(PenalityRateId:number) {

    console.log(PenalityRateId)
    this.confirmationService.confirm({
      message: 'Are You sure you want to delete this Penality Rate?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.controlService.deletePenalityRate(PenalityRateId).subscribe({
          next: (res) => {

            if (res.success) {
              this.messageService.add({ severity: 'success', summary: 'Confirmed', detail: res.message });
              this.getMeterRates()
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

  
  updateMeterRate(PenalityRateId:number){


    let modalRef = this.modalService.open(AddMeterRateComponent,  {size:'lg',backdrop:'static'})

    modalRef.componentInstance.recordNo=PenalityRateId

    modalRef.result.then(()=>{
      this.getMeterRates()
    })  }


    onPageChange(event: any ) {
      this.first = event.first;
      this.rows = event.rows;
      this.paginatedMeterRate();
    }
    paginatedMeterRate() {
      this.paginationMeterRate= this.MeterRates.slice(this.first, this.first + this.rows);
    }
  

  

}
