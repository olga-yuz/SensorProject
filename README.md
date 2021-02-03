# SensorProject
This project is our Alpha Groups take on a Web API acting as a Server Communcating with a Desktop GUI (Client).
This is project is built to display and monitor the performance of vehicles. It can store, read and track vehicle's data that has been input using 3 different sensors; Temperature, Humidity and GPS.
The data is read using sensor API endpoints and this is displayed in a dashboard to view this data.

## Getting Started

To Being using the Application, Download a clone of the git repository to your machine. Open to project folder with a Visual Studio Environment (Preferablly Studio 2019). Ensure all file are loaded correctly before the project is started within the environment.

## Prerequisites

### Installing
From this github repository click on the option to "clone"
On your local machine use git bash to move into a directory in which you would like your git repository to be located.
Use the command:
```
git clone https://github.com/olga-yuz/SensorProject.git
```
This will import this repository to your selected file directory
Then you can open the file in Visual Studio

### NuGet Packages
- Moq
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Authentication.OpenIdConnect
- Microsoft.AspNetCore.Identity
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.IdentityModel.Tokens
- System.IdentityModel.Tokens.Jwt

### Execution

Running the VehicleAPI and the SensorGUI projects at the same time will display the Swagger interface and the Sensor dashboard, where you can create, read, update and delete the data.
The data is connected to three databases (Vehicle, Location and Red Alert) which are updated upon any changes and reflected in the dashboard.

## Testing
We have tested the API's using Xunit
Xunit testing will test the contents of a single action, not the behavior of its dependencies or the framework itself.

To generate a coverage report, follow the steps below:

Firstly, you must have "coverlet" installed - You can do this by installing the NuGet Package: *coverlet.msbuild*.
This can be done by running the following command in your developer PowerShell:
```
dotnet add package coverlet.msbuild
```
In order to collect the coverage data, run this next command:
```
dotnet test /p:CollectCoverage=true /p:CoverletOutput=TestResults/ /p:CoverletOutputFormat=lcov
```

The following two commands will be used to generate the coverage report:

```
dotnet tool install -g dotnet-reportgenerator-globaltool
```

```
reportgenerator "-reports:Path\To\TestProject\TestResults\{guid}\coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html
```

## Built With

Visual Studio
.Net

## Authors

* **Olga Yuzenkova**
* **Winifred Dakora** 
* **Ben Irwin** 
* **Usama Mahmood**

## License

This project is licensed under the MIT license - see the [LICENSE.md](LICENSE.md) file for details 
*For help in [Choosing a license](https://choosealicense.com/)*

## Acknowledgments

Dara Oladapo for his help in this project

