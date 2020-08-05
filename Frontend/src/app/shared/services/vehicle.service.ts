import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { throwError,Observable, Subject } from 'rxjs';
import { DataService } from './data.service';
import {environment} from '../../../environments/environment';
import { catchError } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class VehicleService {
relativeUrl='/api/vehicle';
private alertMessage$ = new Subject<string>();
constructor(private http: HttpClient, private dataservice: DataService) { }

  getVehicleTypes() {
    return this.http.get<string[]>(environment.baseUrl + this.relativeUrl+ '/types').
    pipe(catchError(error => {
      this.alertMessage$.next(error.message);
      return throwError(error.message);
    } ));
  }
  get errorMessage() {
    return this.alertMessage$;
  }
  getAllVehicles()  {
    return this.http.get(environment.baseUrl+ this.relativeUrl).
    pipe(catchError(error => {
      this.alertMessage$.next(error.message);
      return throwError(error.message);
    } ));
  }
  getVehicleProperties(type: string) {
    return this.http.get(environment.baseUrl + this.relativeUrl+ '/' + type).subscribe((response: any) =>{
      this.dataservice.setVehicleProps(response);
    });
  }

  addVehicle(newVehicle): Observable<any> {
    return this.http.post<any>(environment.baseUrl+ this.relativeUrl, newVehicle).
    pipe(catchError(error => {
      this.alertMessage$.next(error.message);
      return throwError(error.message);
    } ));
  }
}
