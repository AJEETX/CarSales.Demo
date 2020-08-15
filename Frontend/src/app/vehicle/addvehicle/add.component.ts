import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { VehicleService } from '../../shared/services/vehicle.service';
import { Subscription } from 'rxjs';
import { DataService } from '../../shared/services/data.service';
import { FormGroup, FormControl} from '@angular/forms';
@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  requestedvehicletype: string;
  vehicleProps;
  form: FormGroup;
  IdProp = ['Id', 'VehicleType'];

  constructor(private route: ActivatedRoute, private router: Router, private vehicleService: VehicleService, private dataService: DataService) {
      this.route.params.subscribe((params: Params) => {
          this.requestedvehicletype = params['type'];
        });
      this.vehicleService.getVehicleProperties(this.requestedvehicletype);
      }

  ngOnInit() {
      this.dataService.vehiclePropsChanged.subscribe((vehicles) => {
        this.vehicleProps = vehicles.filter(d => 
          !this.IdProp.includes(d.Name)
          );
          const formGroup = {};
          for (const prop of this.vehicleProps) {
            formGroup[prop['Name']] = new FormControl(null);
          }
          formGroup['VehicleType'] = new FormControl(this.requestedvehicletype);
          this.form = new FormGroup(formGroup);
          });
    }

    onSubmit() {
      if(!this.form.invalid){
        this.vehicleService.addVehicle(this.form.value).subscribe(data => {
        });
        this.router.navigate(['/']);
      }
    }

    cancel() {
      this.router.navigate(['']);
    }
}
