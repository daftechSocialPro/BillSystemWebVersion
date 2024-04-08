export interface ICustomerMeterStatusGetDto {
  fiscalYear: number;
  monthIndex: number;
  typeOfAction?: string;
  reason?: string;
  enterDate?: Date;
}


export interface ICustomerMeterStatusPostDto {

  fiscalYear: number;
  monthIndex: number;
  typeOfAction: string;
  reason: string;
  disDate: Date;
  enterBy:string 
  custId : string
}
