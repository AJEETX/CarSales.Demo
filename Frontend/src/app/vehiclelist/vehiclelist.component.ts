import { Component, OnInit, OnDestroy } from '@angular/core';
import {Vehicle} from '../shared/model/vehicle.model';
import { VehicleService } from '../shared/services/vehicle.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-vehiclelist',
  templateUrl: './vehiclelist.component.html',
  styleUrls: ['./vehiclelist.component.css']
})
export class VehiclelistComponent implements OnInit, OnDestroy {
  vehicles: Vehicle[];
  private subscription: Subscription;
  public Loading = false;
  constructor(private vehicleService: VehicleService) {
   }

  ngOnInit() {
    this.Loading = true;
    setTimeout(() => {
      this.subscription = this.vehicleService.getAllVehicles().subscribe(data => {
        this.vehicles = data;
        this.Loading = false;
        });
    }, 1000);
   }

   ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
