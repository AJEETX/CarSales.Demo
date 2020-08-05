import {Routes, RouterModule} from '@angular/router';
import {NgModule} from '@angular/core';
import {ErrorComponent} from './shared/error/error.component';
import {VehiclesComponent} from './vehicle/vehicles.component';
import {AddComponent} from './vehicle/addvehicle/add.component';

import {HomeComponent} from './home/home.component';

const appRoute: Routes = [
  {path: '', component: HomeComponent, children: [ {path: '', component: VehiclesComponent}]},
  {path: 'Add/:type', component: AddComponent},
  {path: '**', component: ErrorComponent, data: {message: 'Page not found'} }
];

@NgModule ({
  imports: [
    RouterModule.forRoot(appRoute)
  ],
  exports: [RouterModule]
})

export class AppRoutingModule {
}
