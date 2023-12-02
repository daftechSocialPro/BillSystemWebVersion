// Angular import
import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsSetupService } from 'src/app/services/system-control/scs-setup.service';
import { IAccountPeriodDto } from 'src/models/system-control/IAccountPeriod';
import { IFiscalMonthDto } from 'src/models/system-control/IFiscalMonthDto';

@Component({
  selector: 'app-nav-left',
  templateUrl: './nav-left.component.html',
  styleUrls: ['./nav-left.component.scss']
})
export class NavLeftComponent implements OnInit {
  // public props
  @Output() NavCollapsedMob = new EventEmitter();

  accountPeriod: IAccountPeriodDto
  months: IFiscalMonthDto[]
  monthName: string = ""


  ngOnInit(): void {
    this.getAccountPeriod()
  }

  constructor(
    private dataService: ScsDataService,
    private controlService: ScsSetupService) {

  }

  getAccountPeriod() {
    this.controlService.getAccountPeriod().subscribe({
      next: (res) => {
        this.accountPeriod = res
        this.getMonths()

      }
    })
  }

  getMonths() {
    this.dataService.getFiscalMonth().subscribe({
      next: (res) => {
        this.months = res
        this.monthName = this.months.filter((item) => item.monthIndex == this.accountPeriod.monthIndex)[0].monthnamelocal
      }
    })
  }
}
