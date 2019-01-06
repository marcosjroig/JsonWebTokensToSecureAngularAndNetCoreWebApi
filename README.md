# JWT Security with Angular 7 and .NET Core Web API

Adding security to Angular 7 application using JSON Web Tokens, claims, and the .NET Core Web API.

## Installation
This repo has 3 folders:
- Databases: that has 2 scripts, one to create the DB and the second to populate the tables
- Web: is the web project made in angular 7 
- NetCoreWebApi: is the .Net Core Web API made with C# and .Net Core 2.2

After downloaded this repo and unzip it:

### Database installation
1) Execute in order the scripts of the "Database" folder
2) Verify in your SQL Server that the DB has been created and tables are populated with some data.

### Web solution installation
1) Go to the command line and navigate until the "Web" folder, in the root of this folder execute these commands:
npm install
ng serve

### Web Api
1) Open the solution of the folder "NetCoreWebApi" with VS2017 or newer 
2) Change the property "PtcDbContext" of the file "appsettings.json" to set your SQL Server name and your credentials as well, in my case I am using Windows authentication
3) Compile the solution and press F5 to run the API
