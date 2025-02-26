import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Application} from "../models/application";
import {Account} from "../models/account";

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  private baseUrl = "http://localhost:5260/api";
  private apiKey = "52726F4612414A76BA1A8110EE1FE145"

  private getHeaders() {
    return new HttpHeaders({
      "x-api-key": this.apiKey
    });
  }

  getApplications() {
    return this.http.get(`${this.baseUrl}/applications`, { headers: this.getHeaders() });
  }

  getPasswords(){
    return this.http.get(`${this.baseUrl}/passwords`, { headers: this.getHeaders() });
  }

  createApplication(application: Application){
    return this.http.post(`${this.baseUrl}/applications`, application, { headers: this.getHeaders() });
  }

  createPasswordAccount(account: Account){
    var body = {
      name: account.name,
      password: account.password,
      idApplication: account.application.idApplication
    }
    return this.http.post(`${this.baseUrl}/passwords`, body, { headers: this.getHeaders() });
  }

  deleteAccount(id: number){
    return this.http.delete(`${this.baseUrl}/passwords/${id}`, { headers: this.getHeaders() });
  }

}
