import { Component, OnInit } from '@angular/core';
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
  public Loading = false;
  constructor(private vehicleService: VehicleService) {
   }

  ngOnInit() {
    this.Loading = true;
    setTimeout(() => {
      this.vehicleService.getAllVehicles().subscribe(data => {
        this.vehicles = data;
        this.Loading = false;
        });
        this.errormessage$ = this.vehicleService.errorMessage;
    }, 1000);
   }
  }
