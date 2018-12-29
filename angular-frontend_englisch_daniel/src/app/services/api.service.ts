import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { LoginRequest } from './requests/login.request';
import { TokenResponse } from './responses/token.response';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private token : string = null


  constructor(private http: HttpClient,  private router: Router){

    /* Load token from localStorage */
    this.token = localStorage.getItem("token");

    if(this.token == null){
      console.log("No token found! Requesting login.")
      this.router.navigate(['/login'])
    }

  }


  public async login(request: LoginRequest ){
  
    let response : HttpResponse<object>

    try {
      response = await this.http.post("/api/v1/auth/",request,{observe: 'response'}).toPromise()
    } catch (error) {
        return false
    }

    let payload = <TokenResponse> response.body
    this.token = payload.token
    localStorage.setItem("token", this.token)
    return true
    
  }

}
