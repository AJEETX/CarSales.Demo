import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../shared/services/vehicle.service';
import { Observable } from 'rxjs';
import {  tap, delay } from "rxjs/operators";

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.css']
})
export class VehiclesComponent implements OnInit {
  vehicles:any;
  errormessage$: Observable<string>;
  public Loading = false;
  constructor(private vehicleService: VehicleService) {
   }

  ngOnInit() {
    this.Loading = true;
      this.vehicles=this.vehicleService.getAllVehicles().pipe(
      tap(_ => (this.Loading = false)));
   }
  }
