
# Clean Architecture Sample

This sample solution uses **Clean Architecture** and **CQRS** to expose a REST Web API.

Swagger documentation is available at: **/swagger/index.html**

## Technologies

 - C# language using Microsoft .NET 8.0 Framework
 - SqLite with EntityFramework Core (In-Memory for tests)
 - MediatR for the CQRS pattern
 - Automapper
 - xUnit as testing framework (using Moq & FluentAssertions packages)

## Installation
Clone the repository and restore packages:

```sh
$ git clone https://github.com/marcosjimenez/cleanarchitecturesample.git
$ cd cleanarchitecturesample
$ dotnet restore
```

## Run
Use --launch-profile "https" or "http"
```sh
$ cd src\cleanarchitecturesample.API
$ dotnet run --launch-profile "https"
```
