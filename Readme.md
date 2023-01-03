# TASKY WEB API

This is a simple project to show in practice some examples of using .NET 6 with testing and SOLID principles.

The main idea for this project is to serve as a backend for a task manager application, where we have 3 main entities, Category, TaskList and Item. Every TaskList should have a name and a Category related to it, but may not have Items related.

## How to run the project
There are 2 ways to run this project. Using Docker Compose (recommended) or provisioning a local enviroment using appsettings.json file.

* Using Docker Compose:
    - Install docker and docker compose on your machine.
    - Using Visual Studio: Select "docker-compose" as the startup project and then click run.
    - Using command line: go to the application folder and type the command ```docker-compose up``` and hit enter.

* Using appsettings.json file:
    - First you have to get your SQL Server instance up and runnning, you can install it on your machine or even create a docker container for the database only.
    - Go to appsettings.json file and change ```DB_HOST``` value with your database IP Address, ```DB_NAME``` with your database name (usually "tasky") and ```DB_SA_PASSWORD``` with the password for SA user you specified on your database.
    - Set "Tasky" project as the statup project.
    - Click run or use the command ```dotnet run```.

Then you should be able to use the application endpoints. This app uses Swagger that provides us a nice interface to use and test our application endpoints. When your app is up and running, to reach swagger page, you have to add ```/swagger``` or ```/swagger/index.html``` path into your browser. The entire path should be something like this: ```https://localhost:8001/swagger/index.html```.

Alternatively you can use another tool to test the API endpoints like Postman.

## Some tools used

* Visual Studio 2022 - https://visualstudio.microsoft.com/vs/
* Visual Studio Code - https://code.visualstudio.com/
* SQL Server - https://www.microsoft.com/en-us/sql-server/sql-server-downloads
* Docker - https://www.docker.com/
* Docker Compose - https://docs.docker.com/compose/

## Packages used

* AutoMapper - For mapping dto and model objects in an easy way.
* Fluent Validation - For setting the validations for the requests.
* Entity Framework Core - To improve the database handling.