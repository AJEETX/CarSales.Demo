# CarSales.Demo
[![.Net Framework](https://img.shields.io/badge/DotNet-2.1_Framework-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/2.1) |[![Node](https://img.shields.io/badge/Node-Js-blue.svg?style=plastic)](https://nodejs.org/en/download/) | [![Angular](https://img.shields.io/badge/angular-8-blue)](https://angular.io/) | ![GitHub language count](https://img.shields.io/github/languages/count/ajeetx/CarSales.Demo.svg) | ![GitHub top language](https://img.shields.io/github/languages/top/ajeetx/CarSales.Demo.svg) |![GitHub repo size in bytes](https://img.shields.io/github/repo-size/ajeetx/CarSales.Demo.svg) 
| --- | ---          | ---        | ---      | ---        |  --- |

---------------------------------------
# Demo

| <img width=“100%” alt=“list” src="https://github.com/AJEETX/CarSales.Demo/blob/master/demo.gif"> |
| --- |



## Repository codebase
 
The repository consists of projects as below:


| # |Project Name | Project detail | location| Environment |
| ---| ---  | ---           | ---          | --- |
| 1 | CarSale.Demo.Api | Asp.Net Core WebApi as backend  |  **Backend** folder | [![.Net Framework](https://img.shields.io/badge/DotNet-2.1_Framework-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/2.1)|
| 2 | CarSale.Demo.Api.Domain | Business logic  |  **Backend** folder | [![.Net Framework](https://img.shields.io/badge/DotNet-2.1_Framework-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/2.1)|
| 3 | CarSale.Demo.Api.Test | Unit Test for Api |  **Backend** folder | [![.Net Framework](https://img.shields.io/badge/DotNet-2.1_Framework-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/2.1)| 
| 4 | carsales | angular application   | **Frontend** folder | [![Node](https://img.shields.io/badge/Node-Js-blue.svg?style=plastic)](https://nodejs.org/en/download/)  [![Angular](https://img.shields.io/badge/angular-8-blue)](https://angular.io/) |

## Design highlight

> For future enhancements to add more type of vehicles for example **motor bike**:

| angular application | Remarks |
| --- | --- |
| No change required | none |


| CarSale.Demo.Api application   |  Remarks |
| --- | --- |
| No change in endpoints |  none |

| CarSale.Demo.Api.Domain class library   |  Remarks |
| --- | --- |
| Add enum and model for the new vehicle i.e. motorbike | Add new code |
| Hook up the enum/model to VehicleDetailService to add new VehicleType  | `Modify` existing code |
| Add/implement an interface i.e. *IMotobikeDbService* implementing *IVehicleDbServiceBase* |  Add new code |
| Inject VehicleDetailService  with 'IMotobikeDbService' | `Modify` existing code`  |
| Wire up 'IMotobikeDbService' in the Dependency injection graph  | `Modify` existing code  |


##### Environment Setup

> Download/install   	
>	1.	[![.Net Framework](https://img.shields.io/badge/DotNet-2.1_Framework-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/2.1) to run CarSale.Demo.Api and CarSale.Demo.Api.Test project
>   
>   2. [![Node](https://img.shields.io/badge/Node-Js-blue.svg?style=plastic)](https://nodejs.org/en/download/) to run the angular [front end] application
>   
>	3. [![VSCode](https://img.shields.io/badge/VS-Code-blue.svg?style=plastic)](https://code.visualstudio.com/) to run/debug the applications
>	
>   4. In Visual Studio Code, please install a [![c#](https://img.shields.io/badge/cSharp-extension-blue.svg?style=plastic)](https://github.com/OmniSharp/omnisharp-roslyn)
>   
>   5. Please install angular-cli version 8 [![Angular](https://img.shields.io/badge/angular-8-blue)](https://angular.io/)
>   

##### Project Setup

>   1. Please clone or download the repository from [![github](https://img.shields.io/badge/git-hub-blue.svg?style=plastic)](https://github.com/AJEETX/CarSales.Demo) 
>   
>   2. Create a folder and place the downloaded repository
>   3. Open **Visual Studio Code** and open the newly created folder where the repository is downloaded
>   
##### (a) To start the backend Api service
   
>   1. Within **Visual Studio Code** open a command terminal by pressing the computer keyboard buttons `Control` and `~`
>    
>   2. Within the terminal, browse to  root location of  class library project [**"Backend/CarSales.Demo.Api.Domain"**]
>  
>   3. Restore the dependencies, type `dotnet restore` on the terminal
>    
>   4. Within the terminal, browse to  root location of webapi project [**"Backend/CarSales.Demo.Api"**]
>  
>   5. Restore the dependencies, type `dotnet restore` on the terminal
>
>   6. Run the webapi project, type `dotnet run` on the terminal
>   
>   7. **CarSales.Demo.Api** shall start running on port **3000**
>
>   8. To verify the Webapi is running, open the url **http://localhost:3000** in chrome browser, the page shall be displayed as below
>
>   <img width=“1469” alt=“list” src="https://github.com/AJEETX/CarSales.Demo/blob/master/swagger.PNG">


##### (b) To run the unit test project

>   1. Within **Visual Studio Code** Open a new command terminal
>   
>   2. Within the new terminal, browse to the root location of webapi test project [**"Backend/CarSales.Demo.Api.Test"**]
>   
>   3. To run the tests, type `dotnet test` on the terminal

##### (c) To start the front end application

>   1. Within **Visual Studio Code** Open a new command terminal
>   
>   2. Within the new terminal, browse to the folder named as **"Frontend"**
>   
>   3. To restore the dependencies, type `npm install` on the terminal
>   
>   4. Now in order to run the front end application, type `npm start` on the terminal
>   
>   5. The default browser shall open with url as `localhost:5000`, and the page shall be displayed as below. For better experience please use chrome browser
>
>   <img width=“1469” alt=“list” src="https://github.com/AJEETX/CarSales.Demo/blob/master/client.PNG">

```
happy coding :)
```
