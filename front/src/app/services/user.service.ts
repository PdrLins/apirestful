import { Injectable } from '@angular/core';
import { HttpApiServiceProvider } from '../providers/http-service/http-service';

@Injectable()
export class UserService {

  constructor(private http: HttpApiServiceProvider) { 
    
  }


  signup(user: any) {
    return this.http.postRequest('user/Signup', user);
  }
  signin(user: any) {
    return this.http.postRequest('user/Signin', user);
  }

  findUser(user:any) {
    return this.http.postRequest('user/FindUser', user);
  }

  me() {
    return this.http.postRequest('user/FindUser',{});
  }
}
