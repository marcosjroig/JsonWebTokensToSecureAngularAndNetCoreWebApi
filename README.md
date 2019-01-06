# JWT Security with Angular 7 and .NET Core Web API

Adding security to Angular 7 application using JSON Web Tokens, claims, and the .NET Core Web API.

## Requirements for Installation
- Visual Studio 2017 or VS Code
- .NET Core 2.2 framework
- SQL Server 2017
- Check you have NODE installed in your PC

## Installation
This repo has 3 folders:
- __Databases__: that has 2 scripts, one to create the DB and the second one to populate the tables
- __Web__: is the web project made in angular 7 
- __NetCoreWebApi__: is the .Net Core Web API made with C# and .Net Core 2.2

After downloading this repo and unzip it, please follow the next instructions to configure everything.

### Database installation
1) Execute in order the scripts of the __"Database"__ folder
2) Verify in your SQL Server that the DB has been created and tables are populated with some data.

### Web solution installation
1) Go to the command line and navigate until the __"Web"__ folder, in the root of this folder execute these commands:

  #### npm install 
  > To install the Node packages
  
  #### ng serve
  > To run the web server

### Web Api
1) Open the solution of the folder __"NetCoreWebApi"__ with VS2017 or newer 
2) Change the property __"PtcDbContext"__ of the file __"appsettings.json"__ to set your SQL Server name and your credentials as well, in my case I am using Windows authentication
3) Ensure the file __launchSettings.json__ is pointing to this URL http://localhost:5000/ as the Web project is looking at this URL to access to the DB.
4) Compile the solution and press F5 to run the API
5) Do not worry if you see an empty page when you run the API

## Execution
Once you have configured everything like in the previous steps, you should have running:
- the web solution in the URL http://localhost:4200/ 
- the Web API in the URL http://localhost:5000/

Now you can try to log in. You can check the credentials in the Security.User table. To make the life easier the password is not encrypted.

Once you login you will see a big string, do not worry, it is a print of the Json web token. And now you should be able to navigate in the other pages as well.

