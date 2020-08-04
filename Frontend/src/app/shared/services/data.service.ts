import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { VehicleDetail} from '../model/vehicle.model';

@Injectable()
export class DataService {
  vehiclePropsChanged = new Subject<VehicleDetail[]>();
  vehicleDetail: VehicleDetail[];
  constructor() {}
  setVehicleProps(_vehicleProps: any) {
    this.vehicleDetail = _vehicleProps;
    this.vehiclePropsChanged.next(this.vehicleDetail.slice());
  }
}
