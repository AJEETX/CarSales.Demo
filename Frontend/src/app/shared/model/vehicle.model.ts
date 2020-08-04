export enum VehicleType {
  CAR,
}

export  class VehicleDetail {
  Name: string;
  Datatype: string;
  Regex: string;
  Required: boolean;
  Value: any;

  constructor(public _Name: string, public _Datatype: string,
    public _Order: number, public _Regex: string, public _Required: boolean,public _Value: any) {
    this.Name = _Name;
    this.Datatype = _Datatype;
    this.Regex = _Regex;
    this.Required = _Required;
    this.Value = _Value;
  }
}

export  abstract  class Vehicle {
  Id: number;
  Model: string;
  Make: string;
  abstract VehicleType: VehicleType;
}
export class Car extends Vehicle {
  Doors: number;
  Engine: string;
  Wheels: number;
  Bodytype: string;
  VehicleType: VehicleType = VehicleType.CAR;
  constructor(public _Model: string, public _Make: string, public _Doors: number, public _Engine: string,
    public _Wheels: number, public _Bodytype: string) { super();
    this.Model = _Model;
    this.Make = _Make;
    this.Doors = _Doors;
    this.Engine = _Engine;
    this.Wheels = _Wheels;
    this.Bodytype = _Bodytype;
    }
}
export class Boat extends Vehicle {
  Doors: number;
  Engine: string;
  Floors: number;
  VehicleType: VehicleType = VehicleType.CAR;
  constructor(public _Model: string, public _Make: string, public _Doors: number, public _Engine: string,
    public _Floors: number) { super();
    this.Model = _Model;
    this.Make = _Make;
    this.Doors = _Doors;
    this.Engine = _Engine;
    this.Floors = _Floors;
    }
}
export class CurrentVehicle {
  Id: number;
  VehicleType: VehicleType;
  constructor(public _id: number, public _vehicleType: VehicleType) {
    this.Id = _id;
    this.VehicleType = _vehicleType;
  }
}
