import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserService } from '../user.service';
import { ICustomerDto } from 'src/models/customer-service/ICustomerDto';

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
}
