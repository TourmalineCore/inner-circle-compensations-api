# inner-circle-compensations-api

____
## Prerequisites
- [.NET Core SDK 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) or later
- [Docker](https://www.docker.com/get-started/) (for running the application in a container)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
or later (also can be used on Mac)


## Getting started

You need to create an internal network for configuring interaction between different back-end services.  
You can do it using the following command in your terminal: 
```
docker network create ic-backend-deb
```  
Note: If you already has this network, skip this step

To start the service, you should go to the solution folder and enter this command in the terminal. This command starts the service in Docker and raises the database.
```
docker-compose up -d
```
To run the project use the keyboard shortcut `Ctrl+F5` (or `Cmd+F5` for Mac)

You can use Swagger to see all roots by following this link:
```
http://localhost:5273/index.html
```