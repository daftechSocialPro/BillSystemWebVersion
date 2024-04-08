import { Component } from '@angular/core';
import { CssCustomerService } from 'src/app/services/customer-service/css-customer.service';
import { ScsDataService } from 'src/app/services/system-control/scs-data.service';
import { ScsSetupService } from 'src/app/services/system-control/scs-setup.service';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
import { ICustomerGetDto } from 'src/models/customer-service/ICustomerGetDto';
import { ICustomerHomeData } from 'src/models/customer-service/ICustomerHomeDataDto';
import { IAccountPeriodDto } from 'src/models/system-control/IAccountPeriod';
import { ICustomerCategoryDto } from 'src/models/system-control/ICustomerCategoryDto';
import { IFiscalMonthDto } from 'src/models/system-control/IFiscalMonthDto';

@Component({
  selector: 'app-css-home',
  templateUrl: './css-home.component.html',
  styleUrls: ['./css-home.component.scss']
})
export class CssHomeComponent  {

  accountPeriod:IAccountPeriodDto
  months:IFiscalMonthDto[]
  monthName:string=""
  homeDatas : ICustomerHomeData[]

  customers: ICustomerDto[]=[]
  customerCategoriess:ICustomerCategoryDto[];


  constructor(
    private dataService :ScsDataService,
    private ControlService: ScsDataService,
    private controlService:ScsSetupService,
    private customerService: CssCustomerService,
    ) {

  }


  ngOnInit(): void {

    this.getAccountPeriod()
    this.getMonths()
    this.getCustomerCategoriess();
    this.fetchCustomerData();
    this.fetchCustomerHomeData()

  }

  fetchCustomerData() {
    this.customerService.getCustomer().subscribe({
      next:(res)=>{
        this.customers = res;
      },
    }
    );
  }

  fetchCustomerHomeData(){
    this.customerService.getCustomerHomeData().subscribe({
      next:(res)=>{

        this.homeDatas = res 
      }
    })
  }



  getAccountPeriod(){
    this.controlService.getAccountPeriod().subscribe({
      next:(res)=>{
        this.accountPeriod=res
      }
    })
  }
  getMonths(){
    this.dataService.getFiscalMonth().subscribe({
      next:(res)=>{
        this.months=res
        this.monthName=this.months.filter((item)=>item.monthIndex==this.accountPeriod.monthIndex)[0].monthnameEn
      }
    })
  }
  getCustomerCategoriess() {
    this.ControlService.getCustomerCategory().subscribe({
      next: (res) => {
        this.customerCategoriess = res;
      }
    });
  }

  getNumberOfCustomersByCategory(category: string, meterStatus: string): number {
    return this.customers.filter(customer => customer.custCategoryCode === category && customer.meterStatus === meterStatus).length;
  }
}
