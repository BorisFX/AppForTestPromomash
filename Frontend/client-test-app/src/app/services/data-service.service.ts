import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Country } from '../../models/country.model';
import { Province } from '../../models/province.model';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) {}

  private apiUrl = environment.apiUrl;
  getCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(`${this.apiUrl}/country`);
  }

  getProvinces(countryId: string): Observable<Province[]> {
    return this.http.get<Province[]>(`${this.apiUrl}/country/${countryId}/provinces/`);
  }
}
