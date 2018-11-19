import { Component } from '@angular/core';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent {
  user = {
    email: '',
    password: ''
  };
  error = '';
  constructor(private userService: UserService, private router: Router) { }

  cleanError(){
    this.error = '';
  }

  signin() {
    this.userService.signin(this.user).then((result)=>{
      if(result.isSucess){
        this.router.navigate(['/about-me', result.userId]);
        localStorage.setItem("token", result.token);
      }
      else{
        this.error = JSON.stringify(result);
      }
      console.log(result);
    }).catch((er)=>{
      this.error = JSON.stringify(er);
      console.log("Catch:", er);
    });
  }
}
