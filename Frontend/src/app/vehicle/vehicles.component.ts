import { Component, OnInit, OnDestroy } from '@angular/core';
import { VehicleService } from '../shared/services/vehicle.service';
import { Subscription, Observable } from 'rxjs';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.css']
})
export class VehiclesComponent implements OnInit {
  vehicles:any;

  errormessage$: Observable<any>;
  vehicles$: Observable<any>;
  private subscription: Subscription;
  public Loading = false;

  constructor(private vehicleService: VehicleService) {
   }

  ngOnInit() {
    this.Loading = true;
    this.vehicles$ = this.vehicleService.getAllVehicles();
   }
ngAfterViewInit(){
  this.Loading = false;
}
}
