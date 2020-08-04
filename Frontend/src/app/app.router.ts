import {Routes, RouterModule} from '@angular/router';
import { NgModule } from '@angular/core';
import { ErrorComponentComponent } from './error-component/error-component.component';
import {VehiclesComponent} from './vehiclelist/vehicles.component';
import {AddComponent} from './vehiclelist/addvehicle/add.component';

import {HomeComponent} from './home/home.component';

const appRoute: Routes = [
  {path: '', component: HomeComponent, children: [ {path: '', component: VehiclesComponent}]},
  {path: 'Add/:type', component: AddComponent},
  {path: '**', component: ErrorComponentComponent, data: {message: 'Page not found'} }
];

@NgModule ({
  imports: [
    RouterModule.forRoot(appRoute)
  ],
  exports: [RouterModule]
})

export class AppRoutingModule {
}
