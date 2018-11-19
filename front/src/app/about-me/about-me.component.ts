import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {UserService} from '../services/user.service';

@Component({
  selector: 'about-me',
  templateUrl: './about-me.component.html',
  styleUrls: ['./about-me.component.scss']
})
export class AboutMeComponent implements OnInit {
  user:any;
  constructor(private route: ActivatedRoute, private router: Router, private userService: UserService) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    if(id){
      let user= {id:id}
     this.userService.findUser(user).then((result)=>{
        if(result!= null){
          localStorage.setItem('user', result.user);
          this.user = result;
          console.log("Find User", result);
        }
      }).catch(er=>{
      this.router.navigateByUrl('/signin');
        console.log("Find User", er);
      });
    }
      
  }

  logout(){
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    this.router.navigateByUrl('/signin');
  }

}
