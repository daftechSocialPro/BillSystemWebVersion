export interface ICustomerMeterChangeGetDto {
  fiscalYear: number;
  monthIndex: number;
  typeOfAction?: string;
  reason?: string;
  enterDate?: Date;
}


export interface ICustomerMeterChangePostDto {

  fiscalYear: number;
  monthIndex: number;
  typeOfAction: string;
  reason: string;
  disDate: Date;
  custId : string
}
