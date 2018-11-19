import { Component } from '@angular/core';
import { UserService } from '../services/user.service';
import {Router} from '@angular/router';

@Component({
  selector: 'signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent {
  newUser = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    phones: []
  };
  error = '';
  constructor(private service: UserService, private router: Router) { }

  cleanError(){
    this.error = '';
  }
  
  save() {
    this.service.signup(this.newUser).then((result) => {
      if(result.isSucess){
        this.router.navigateByUrl('/signin');
      }
      else{
        this.error = JSON.stringify(result);
      }
      console.log(result);
    }).catch((er) => {
      console.log(er);
    });
  }

}
