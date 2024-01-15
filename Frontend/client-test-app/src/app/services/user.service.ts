import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../models/user.model';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { SelectUserCountry } from '../../models/selectUserCountry.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  register(user: User): Observable<any> {
    return this.http.post(`${this.apiUrl}/user/register`, user);
  }

  selectUserCountry(data: SelectUserCountry): Observable<any> {
    return this.http.post(`${this.apiUrl}/user/selectCountry`, data);
    }

  }
