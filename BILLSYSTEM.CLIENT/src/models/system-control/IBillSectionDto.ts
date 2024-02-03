export interface IBillSectionDto {
    empID:string
    name:string
    gender:string
    position:string

}


export interface IDetailPermissionDto {
    recId: string;
    appModule?: string;
    appTabs?: string;
    buttonName?: string;
    buttonCaption?: string;
    selected:boolean
  }
