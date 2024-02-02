export interface ICustomerMeterStatusGetDto {
  fiscalYear: number;
  monthIndex: number;
  typeOfAction?: string;
  reason?: string;
  enterDate?: Date;
}
