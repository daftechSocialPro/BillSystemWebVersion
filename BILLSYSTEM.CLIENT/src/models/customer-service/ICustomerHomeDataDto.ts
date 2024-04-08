export interface ICustomerHomeData {
    customerCategory:string,
    activeCustomers: number,
    disconnectedCustomers:  number
    terminatedCustomers:number
}

export interface CustomerBillOfficerDto {
    customerContractNos: string[];
    billOfficerId: string;
  }