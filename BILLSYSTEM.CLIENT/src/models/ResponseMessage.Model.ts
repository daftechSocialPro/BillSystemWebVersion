export interface ResponseMessage{
    success : boolean;
    message: string;
    data: any;
}

export interface SelectList {

    id?: string;
    name: string;
    empId:string;
    // employeeId ?: string 
    // reason?:string
    // photo ?:string
    // commiteeStatus?:string

}

export interface BillOfficers {

    empID:string
    name:string
    gender?: string
    position?:string
    // employeeId ?: string 
    // reason?:string
    // photo ?:string
    // commiteeStatus?:string

}

