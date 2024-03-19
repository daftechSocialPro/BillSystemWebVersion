export interface ICustomerMeterChangeGetDto {
  fiscalYear: number;
  monthIndex: number;
  reason?: string;
  enterDate?: Date;
}


export interface ICustomerMeterChangePostDto {

  fiscalYear?: number;
  monthIndex?: number;
  curMeterNo?: string;
  curMeterSizeCode?: string;
  curMeterType?: string;
  curMeterDigit?: number;
  curMeterOrigin?: string;
  curMeterModel?: string;
  curInstallationDate?: Date;
  curStartReading?: number;
  unpaidCons?: number;
  reason?: string;
  custID?:string
}
