import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app.router';
import { AppComponent } from './app.component';
import { VehiclesComponent } from './vehiclelist/vehicles.component';
import { VehicleService} from './shared/services/vehicle.service';
import { DataService} from './shared/services/data.service';
import { DropdownDirective } from './shared/directive/dropdowndirective';
import { ErrorComponentComponent } from './error-component/error-component.component';
import { HomeComponent } from './home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AddComponent } from './vehiclelist/addvehicle/add.component';

@NgModule({
  declarations: [
    AppComponent,
    VehiclesComponent,
    DropdownDirective,
    ErrorComponentComponent,
    HomeComponent,
    AddComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [VehicleService, DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
