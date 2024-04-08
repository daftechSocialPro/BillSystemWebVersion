import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserService } from '../user.service';
import { ICustomerDto, ICustomerPostDto } from 'src/models/customer-service/ICustomerDto';
import { ResponseMessage } from 'src/models/ResponseMessage.Model';
import { ICustomerGetDto } from 'src/models/customer-service/ICustomerGetDto';
import { ICustomerMeterStatusGetDto, ICustomerMeterStatusPostDto } from 'src/models/customer-service/ICustomerMeterStatusDto';
import { ICustomerMeterChangeGetDto, ICustomerMeterChangePostDto } from 'src/models/customer-service/ICustomerChangeMeterDto';
import { CustomerBillOfficerDto, ICustomerHomeData } from 'src/models/customer-service/ICustomerHomeDataDto';
import { ICustomerBatchDto } from 'src/models/customer-service/ICustomerBatchDto';

@Injectable({
  providedIn: 'root'
})
export class CssCustomerService {
  constructor(
    private http: HttpClient,
    private userService: UserService
  ) {}
  readonly baseUrl = environment.baseUrl;

  //customer
  getCustomer() {
    return this.http.get<ICustomerDto[]>(this.baseUrl + '/Customer/GetCustomeres');
  }
  getCustomerForUpdate(contractNo: string) {
    return this.http.get<ICustomerDto>(this.baseUrl + `/Customer/GetCustomerForUpdate?ContractNo=${contractNo}`);
  }
  updateCustomer(updateCustomer: ICustomerDto) {
    return this.http.put<ResponseMessage>(this.baseUrl + '/Customer/UpdateCustomer', updateCustomer);
  }

  addCustomer(addcustomer: ICustomerDto) {
    return this.http.post<ResponseMessage>(this.baseUrl + '/Customer/AddCustomer', addcustomer);
    // /api/Customer/AddCustomer
  }
  getSingleCustomer(contractNo: string) {
    return this.http.get<ICustomerDto>(this.baseUrl + `/Customer/GetSingleCustomer?contractNo=${contractNo}`);
  }

  deleteCustomer(contractNo: string) {
    return this.http.delete<ResponseMessage>(this.baseUrl + `/Customer/DeleteCustomer?contractNo=${contractNo}`);
  }

  createCustomer(customerData: ICustomerDto) {
    return this.http.post<ResponseMessage>(this.baseUrl + '/Customer/CreateBasicData', customerData);
  }

  getContractNumber(kebele: string, ketena: string) {
    return this.http.get<number>(this.baseUrl + `/Customer/GetContractNumber?kebele=${kebele}&ketena=${ketena}`);
  }
  getCustomerMeterStatus(custId: string) {
    return this.http.get<ICustomerMeterStatusGetDto[]>(`${this.baseUrl}/CustomerMeterStatus/GetCustomerMeterStatus?custId=${custId}`);
  }
  updateCustomerMeterStatus(meterStatus: ICustomerMeterStatusPostDto) {
    return this.http.post<ResponseMessage>(`${this.baseUrl}/CustomerMeterStatus/ChangeCustomerStatus`, meterStatus);
  }

  getCustomerMeterChange(custId: string) {
    return this.http.get<ICustomerMeterChangeGetDto[]>(`${this.baseUrl}/CustomerMeterChange/GetCustomerMeterChange?custId=${custId}`);
  }
  updateCustomerMeterChange(meterChange: ICustomerMeterChangePostDto) {
    return this.http.post<ResponseMessage>(`${this.baseUrl}/CustomerMeterChange/ChangeMeter`, meterChange);
  }


  getCustomerHomeData (){
    return this.http.get <ICustomerHomeData[]> (this.baseUrl+"/Customer/GetCusotmerHomeData");
  }


  updateCustomerBillOfficerId( csutomerBillOfficerDto :CustomerBillOfficerDto){

    return this.http.put<ResponseMessage>(this.baseUrl+"/Customer/UpdateCustomerBillOfficerId",csutomerBillOfficerDto)

  }


  changeValueByBatch(customerBatchDto:ICustomerBatchDto){
    return this.http.post<ResponseMessage>(this.baseUrl+`/Customer/ChangeValueByBatch`,customerBatchDto)
  }
}
