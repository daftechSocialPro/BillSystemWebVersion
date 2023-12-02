
export interface ICustomerDto {

    recordno: number
    userID: string
    fiscalYear: number
    monthIndex: number
    custId: string
    regFiscalYear?: number
    regMonthIndex?: number
    customerName: string
    Ketena: string
    Kebele: string
    HouseNo: string
    Village: string
    Telephone: string
    Mobile: string
    BookNo: string
    AccountNo: string
    contractNo: string
    OrdinaryNo?: number
    custCategoryCode: string
    meterno: string
    meterSizeCode: string
    MeterType: string
    MeterDigit: number
    MeterCountryOrigin: string
    MeterModel: string
    InstallationDate?: Date
    MeterStartReading: number
    MeterAltitude?: number
    MeterLongitude?: number
    sdPaid: string
    MeterClass: string
    WaterSource: string
    MeterStatus: string
    RegDate: Date
    ReaderName: string
    PaymentPlace: string
    PaymentDuration: string
    PaymentMode: string
    BillSalesGroup: string
    OnlineGroup: string
    BankCode: string
    BankAccount: string
    TrasnferBY: string
    BillOfficerId: string
    TransferMI?: number
    TransferDT: string
    Field01: string
    Field02: string
    Field03: number
    Field04?: number
    enterBy: string
    enterDate?: Date
    modifyBy: string
    modifyDate: Date
    DataSynched: string

    selected ?:boolean
}