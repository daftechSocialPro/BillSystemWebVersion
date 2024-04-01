import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { IFiscalMonthDto } from 'src/models/system-control/IFiscalMonthDto';

@Component({
  selector: 'app-css-general',
  templateUrl: './css-general.component.html',
  styleUrls: ['./css-general.component.scss']
})
export class CssGeneralComponent implements OnInit {
  FiscalMonths:IFiscalMonthDto[]
  first: number = 0;
  rows: number = 5;
  paginationFiscalMonth:IFiscalMonthDto[];


  ngOnInit(): void {

    this.getFiscalMonths()
  }

  constructor(
    private modalService : NgbModal,
    private confirmationService: ConfirmationService,
    private messageService : MessageService,
    private controlService:ScsDataService){}


  getFiscalMonths(){

    this.controlService.getFiscalMonth().subscribe({
      next:(res)=>{
        this.FiscalMonths = res
        this.paginatedFiscalMonth()
      }
    })
  }
  onPageChange(event: any ) {
    this.first = event.first;
    this.rows = event.rows;
    this.paginatedFiscalMonth();
  }
  paginatedFiscalMonth() {
    this.paginationFiscalMonth= this.FiscalMonths.slice(this.first, this.first + this.rows);
  }
}
