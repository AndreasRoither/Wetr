import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { LoginRequest } from './requests/login.request';
import { TokenResponse } from './responses/token.response';
import { Station } from './DTOs/station';

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
      response = await this.http.post("http://localhost:5000/v1/auth/",request,{observe: 'response'}).toPromise()
    } catch (error) {
        return false
    }

    let payload = <TokenResponse> response.body
    this.token = payload.Token
    localStorage.setItem("token", this.token)
    return true
    
  }

  /***
   * Auto Authorizing GET request
   */
  private async JwtGet(url:string){

    /* If there is no token, login */
    if(this.token == null){
      this.router.navigate(['/login'])
      throw new Error();
    }

    let headers = new HttpHeaders();
    headers = headers.append("Authorization", this.token);
    let response = await this.http.get(url,  {headers: headers, observe: 'response'}).toPromise();

    if(response.status == 401){
      this.router.navigate(['/login'])
      throw new Error();
    }

    return response.body

  }

  public async getStations(){

    let response;
    try {
      response = <Array<Station>>await this.JwtGet("http://localhost:5000/v1/stations")
    } catch (error) {
      this.router.navigate(['/login'])
      return []
    }
    return  <Array<Station>>response

  }

}
