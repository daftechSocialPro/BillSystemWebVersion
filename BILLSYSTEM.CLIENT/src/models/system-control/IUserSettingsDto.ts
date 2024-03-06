export interface IUserSettingsDto {
    userId: number;
    userName?: string;
    empId?: string;
    userLevel?: string;
    userStatus?: string;
    sysetmAdmin?: string;
    customerService?: string;
    billProduce?: string;
    technicalService?: string;
    stockControl?: string;
    hrm?: string;
    allowKetenas?:string;
    online? :string;
    selected:boolean

  }

  export interface IuserSettingPostDto{

    userId: number;
    userName?: string;
    empId?: string;
    userLevel?: string;
    userStatus?: string;
    sysetmAdmin?: string;
    customerService?: string;
    billProduce?: string;
    technicalService?: string;
    stockControl?: string;
    hrm?: string;
    password?:string;
    allowKetenas?:string;
    online :string;




  }

  export interface IUserPermissionDto{
    buttonId:string
    userId:string
    selected?:boolean

  }
