import { Component} from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { VehicleService } from '../../shared/services/vehicle.service';
import { FormGroup, FormControl} from '@angular/forms';
@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent {
  vehicleProps;
  form: FormGroup;
  IdProp = ['Id', 'VehicleType'];
  requestedvehicletype:string
  constructor(private route: ActivatedRoute, private router: Router, private vehicleService: VehicleService) {
      this.route.params.subscribe((params: Params) => {
        this.requestedvehicletype= params['type'];
          this.vehicleService.getVehicleProperties(params['type']).subscribe((vehicles: any) =>{
            this.vehicleProps = vehicles.filter(d => 
              !this.IdProp.includes(d.Name)
              );
              const formGroup = {};
              for (const prop of this.vehicleProps) {
                formGroup[prop['Name']] = new FormControl(null);
              }
              formGroup['VehicleType'] = new FormControl(params['type']);
              this.form = new FormGroup(formGroup);
          });
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
