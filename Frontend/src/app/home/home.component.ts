import { Component, OnInit } from '@angular/core';
import {VehicleService} from '../shared/services/vehicle.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  errormessage$: Observable<any>;
  vehicletypes$: Observable<any>;
  customErrorMessage=' Server is down !!!'
  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicletypes$= this.vehicleService.getVehicleTypes();
    this.errormessage$ = this.vehicleService.errorMessage;
  }
}
