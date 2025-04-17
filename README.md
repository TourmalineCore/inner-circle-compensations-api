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
docker-compose --profile ide-run up -d
```
To run the project use the keyboard shortcut `Ctrl+F5` (or `Cmd+F5` for Mac)

You can use Swagger to see all roots by following this link:
```
http://localhost:5273/index.html
```

## Karate tests

To run tests, you need to open the project in VS Code and Visual Studio.
Enter this command in Visual Studio
```
docker-compose --profile local-run up -d
```

Go to VS Code
1. Install the extension "Dev Containers" (extension id: ms-vscode-remote.remote-containers)
2. Click on the blue button in the lower left corner of your screen
3. Click "Rebuild Container" or something like this - the project will start in dev container
4. Enter this command to run the tests
```
java -jar /karate.jar .
```

## Configurations

- MockForPullRequest - used in PR pipeline to run the service in isolation (no external deps) and run its Karate tests against it
- MockForDevelopment - used locally when you run the service in Visual Studio e.g. in Debug and don't want to spin up any external deps
- LocalEnvForDevelopment - used locally when you run the service in Visual Studio and you want to connect to its external deps from Local Env (ToDo not there yet)
- ProdForDevelopment - used locally when you run the service in Visual Studio and want to connect to its external deps from Prod specially dedicated Local Development Tenant (ToDo, need to complete tenants, secrets need to be available in the developer PC env vars)
- ProdForDeployment - used when we run the service in Prod, it shouldn't contain any secrets, it should be a Release build, using real Prod external deps

