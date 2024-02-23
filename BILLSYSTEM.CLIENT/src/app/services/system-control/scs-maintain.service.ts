import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { UserService } from "../user.service";
import { IGeneralOptionsDto } from "src/models/system-control/IGeneralOptionsDto";
import { IBillSectionDto, IDetailPermissionDto } from "src/models/system-control/IBillSectionDto";
import { IBillEmpDutiesDto } from "src/models/system-control/IBillEmpDutiesDto";
import { ResponseMessage, SelectList } from 'src/models/ResponseMessage.Model';
import { IUserPermissionDto, IUserSettingsDto, IuserSettingPostDto } from "src/models/system-control/IUserSettingsDto";

@Injectable({
    providedIn: 'root'
  })
  export class ScsMaintainService {
    constructor(private http: HttpClient, private userService: UserService) { }
    readonly baseUrl = environment.baseUrl;

    getBillSection() {
        return this.http.get<IBillSectionDto[]>(this.baseUrl + "/BillSection/GetBillSection")
      }

      addBillOfficer(addBillSection: IBillSectionDto) {

        return this.http.post<ResponseMessage>(this.baseUrl + "/BillSection/AddBillSection", addBillSection)
      }
      getBillOfficerWithNoUser(){
        return this.http.get<SelectList[]>(this.baseUrl+"/Employee/GetEmployeeNoUser")
      }

      updateBillOfficer(updateBillSection: IBillSectionDto) {

        return this.http.post<ResponseMessage>(this.baseUrl + "/BillSection/UpdateBillSection", updateBillSection)
      }
      deleteBillOfficer(empId: string) {
        return this.http.delete<ResponseMessage>(this.baseUrl + `/BillSection/DeleteBillSection?empId=${empId}`)
      }

      getBillEmpDuties() {
        return this.http.get<IBillEmpDutiesDto[]>(this.baseUrl + "/BillEmpDuties/GetBillDuties")
      }

      addBillEmpDuties(addBillEmpDuties: IBillEmpDutiesDto) {


        return this.http.post<ResponseMessage>(this.baseUrl + "/BillEmpDuties/AddBillEmpDuties", addBillEmpDuties)
      }

      getBillEmpDutyForUpdate(recordno: number) {
        return this.http.get<IBillEmpDutiesDto>(this.baseUrl + `/BillEmpDuties/GetBillEmpDutyForUpdate?recordno=${recordno}`)
      }

      updateBillEmpDuties(updateBillEmpDuties: IBillEmpDutiesDto) {

        return this.http.put<ResponseMessage>(this.baseUrl + "/BillEmpDuties/UpdateBillEmpDuties", updateBillEmpDuties)
      }
      deleteBillEmpDuties(recordno: number) {
        return this.http.delete<ResponseMessage>(this.baseUrl + `/BillEmpDuties/DeleteBillEmpDuties?recordno=${recordno}`)
      }
      getBillOfficerHavingNoDuty(){
        return this.http.get<IBillSectionDto[]>(this.baseUrl+"/BillSection/GetBillOfficerHavingNoDuty")
      }
      // Detail Permission

      getDetailPermmission(){

        return this.http.get<IDetailPermissionDto[]>(this.baseUrl+"/BillSection/GetAllDetailPermission")
      }

      getUserSettings(){
        return this.http.get<IUserSettingsDto[]> (this.baseUrl+"/UserSetting/GetAllUserSettings")
      }



      createSystemUser(userPost:IuserSettingPostDto){
        return this.http.post<ResponseMessage> (this.baseUrl+"/UserSetting/CreateSystemUser",userPost)
      }

      getEmployeesForUserSetting(){
        return this.http.get<SelectList[]>(this.baseUrl+"/UserSetting/GetEmployeeForUserSetting")
      }

      getEmployeesForUserSettingUpdate(){
        return this.http.get<SelectList[]>(this.baseUrl+"/UserSetting/GetEmployeesforUserSettingUpdate")
      }



      updateUserService(updateUsers: IuserSettingPostDto) {

        return this.http.put<ResponseMessage>(this.baseUrl + "/UserSetting/UpdateSystemUser", updateUsers)
      }

      deleteSystemUsers (userId : number ){

        return this.http.delete<ResponseMessage>(this.baseUrl + `/UserSetting/DeleteSystemUser?userId=${userId}`)
      }



      getAppModules(){
        return this.http.get<SelectList[]>(this.baseUrl+"/UserSetting/GetAppModules")
      }

      getAppTabsByModule(appModule:string){
        return this.http.get<SelectList[]>(this.baseUrl+`/UserSetting/GetAppTabsByModule?appModule=${appModule}` )
      }

      getUserPermissions(userId:number){
        return this.http.get<IUserPermissionDto[]>(this.baseUrl+`/UserSetting/GetUserPermissions?userId=${userId}` )
      }


      updateUserPermissions(userPermissions:IUserPermissionDto[]){

        return this.http.put<ResponseMessage>(this.baseUrl+`/UserSetting/UpdateUserPermission`,userPermissions)
      }


      getBanks(){
        return this.http.get<SelectList[]> (this.baseUrl+"/Bank/GetBanks")
      }


      getBillOffciersForTransfer(){
        return this.http.get<SelectList[]> (this.baseUrl+"/BillSection/GetBillOfficersForTransfer")
      }
      getBillOffciers(){
        return this.http.get<SelectList[]> (this.baseUrl+"/BillSection/GetBillOfficers")
      }





  }
