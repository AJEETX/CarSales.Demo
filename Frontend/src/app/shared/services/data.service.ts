import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class DataService {
  vehiclePropsChanged = new Subject<any>();
  constructor() {}
  setVehicleProps(_vehicleProps: any) {
    this.vehiclePropsChanged.next(_vehicleProps.slice());
  }
}
