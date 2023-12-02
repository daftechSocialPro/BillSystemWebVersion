// Angular import
import { Component, OnInit } from '@angular/core';
import { AuthGuard } from 'src/app/auth/auth.guard';
import { CommonService } from 'src/app/services/common.service';
import { UserService } from 'src/app/services/user.service';
import { UserView } from 'src/models/auth/userDto';

@Component({
  selector: 'app-nav-right',
  templateUrl: './nav-right.component.html',
  styleUrls: ['./nav-right.component.scss']
})
export class NavRightComponent implements OnInit {

  currentUser:UserView
  ngOnInit(): void {

    this.currentUser = this.userService.getCurrentUser()
   
    
  }

  constructor( 
    private commonService:CommonService,
    private authGuard: AuthGuard,private userService:UserService){

  }

  // getImage(){
  //   return this.commonService.createImgPath(this.currentUser.photo)
  // }

  
  logOut() {

    this.authGuard.logout();
  }
}
