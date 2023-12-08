import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserService } from '../user.service';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';
import { ICustomerGetDto } from 'src/models/customer-service/ICustomerGetDto';
import { ResponseMessage } from 'src/models/ResponseMessage.Model';

@Injectable({
  providedIn: 'root'
})
export class CssCustomerService {

  constructor(private http: HttpClient, private userService: UserService) { }
  readonly baseUrl = environment.baseUrl;


  //customer
  getCustomer() {
    return this.http.get<ICustomerDto[]>(this.baseUrl + "/Customer/GetCustomeres")
  }
  getCustomerForUpdate(contractNo: string) {
    return this.http.get<ICustomerDto>(this.baseUrl + `/Customer/GetCustomerForUpdate?ContractNo=${contractNo}`)
  }
updateCustomer(updateCustomer:ICustomerDto){
  return this.http.put<ResponseMessage>(this.baseUrl+'/Customer/UpdateCustomer', updateCustomer)
  
}

  addCustomer(addcustomer: ICustomerDto) {

    return this.http.post<ResponseMessage>(this.baseUrl + "/Customer/AddCustomer", addcustomer)
    // /api/Customer/AddCustomer
  }
  deleteMetersizeRent(customerId: number) {
    return this.http.delete<ResponseMessage>(this.baseUrl + `/Customer/DeleteCustomer?CustomerId=${customerId}`)
    // /api/Customer/DeleteCustomer
  }
}
