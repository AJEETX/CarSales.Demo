import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { throwError,Observable, Subject } from 'rxjs';
import {environment} from '../../../environments/environment';
import { catchError, shareReplay,map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  relativeUrl='/api/vehicle';
  private alertMessage$ = new Subject<string>();
  private vehicleTypes$:Observable<string[]>;

  constructor(private http: HttpClient) { }
    
  getVehicleTypes() {
      if(!this.vehicleTypes$){
        this.vehicleTypes$=this.http.get<string[]>(environment.baseUrl + this.relativeUrl+ '/types').
          pipe(shareReplay(1)) as Observable<string[]>;
      }
      return this.vehicleTypes$;
    }

  get errorMessage() {
      return this.alertMessage$;
    }

  getAllVehicles()  {
      return this.http.get(environment.baseUrl+ this.relativeUrl).
        pipe(map(res=>{
          return res
        }, catchError(error => {
          this.alertMessage$.next(error.message);
          return throwError(error.message);
        } )));
    }

  getVehicleProperties(type: string) {
      return this.http.get(environment.baseUrl + this.relativeUrl+ '/' + type).
        pipe(shareReplay(1),catchError(error => {
          this.alertMessage$.next(error.message);
          return throwError(error.message);
        } ));
    }

  addVehicle(newVehicle) {
    return this.http.post<any>(environment.baseUrl+ this.relativeUrl, newVehicle).
      pipe(catchError(error => {
        this.alertMessage$.next(error.message);
        return throwError(error.message);
      } ));
  }
}
