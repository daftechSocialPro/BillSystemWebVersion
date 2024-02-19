export interface ICustomerDto {
  fiscalYear?: number;
  monthIndex?: number;
  custId?: string;
  regFiscalYear?: number;
  regMonthIndex?: number;
  customerName: string;
  ketena: string;
  kebele: string;
  houseNo: string;
  village: string;
  telephone?: string;
  reason?: string;
  // mobile: string
  bookNo?: string;
  accountNo?: string;
  contractNo: string;
  ordinaryNo?: number;
  mapNumber?: string;
  custCategoryCode: string;
  meterno: string;
  meterSizeCode: string;
  meterType?: string;
  meterDigit?: number;
  meterCountryOrigin?: string;
  meterModel?: string;
  installationDate?: Date;
  meterStartReading?: number;
  // meterAltitude?: number
  // meterLongitude?: number
  sdPaid: string;
  billCycle?: string;
  meterClass?: string;
  waterSource?: string;
  meterStatus?: string;
  regDate?: Date;
  readerName?: string;
  paymentPlace?: string;
  // paymentDuration: string
  paymentMode?: string;
  // billSalesGroup: string
  // onlineGroup: string
  // bankCode: string
  bankAccount?: string;
  billOfficerId?: string;
  transferMI?: number;
  transferBy?: string;
  transferDT?: string;
  transferFy?: number;
  // field01: string
  // field02: string
  // field03: number
  // field04?: number
  // enterBy: string
  // enterDate?: Date
  // modifyBy: string
  // modifyDate: Date
  // remarks:string
  // dataSynched: string
  selected?: boolean;
}

export interface ICustomerPostDto {
  fullName: string;
  phoneNumber: string;
  ketena: string;
  kebele: string;
  readerName: string;
  village: string;
  mapNumber: string;
  houseNumber: string;
  billCycle: string;
  customerCategory: string;
  contractNo: string;
  ordinaryNo: string;
  meterNo: string;
  meterSize: string;
  installationDate: Date;
  updateInitial: string;
  startReading: number;
  sweragePaid: string;
  monthIndex: number;
  fiscalYear: number;
  billOfficerId: number;
  reason?: string;
}
