import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {FormsModule} from '@angular/forms';


//providers and services
import { HttpApiServiceProvider } from './providers/http-service/http-service';
import { UserService } from './services/user.service';

//external-components
import { SignupComponent } from './signup/signup.component';
import { SigninComponent } from './signin/signin.component';
import { AboutMeComponent } from './about-me/about-me.component';

@NgModule({
  declarations: [
    AppComponent,
    SignupComponent,
    SigninComponent,
    AboutMeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule   
  ],
  providers: [HttpApiServiceProvider, UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
