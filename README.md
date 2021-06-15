# Hotel.Backend system built on .Net Core 5.0


## Visual Studio 2019 and SQL Server Express 2019

#### Prerequisites

- SQL Server Express 2019 or later
- [Visual Studio 2019 version 16.8 with .NET Core SDK 5.0 ](https://dotnet.microsoft.com/download)

#### Steps to run

- Open project in Vusual Studio.
- Update the connection string in appsettings.json in Hotel.Backend.WebApi.
- Build whole solution.
- Open Package Manager Console Window and type "Update-Database" then press "Enter". This action will create database schema.
- Run staticdata.sql on the created database to insert seed data.
- In Visual Studio, press "Control + F5".

## Technologies and frameworks used:
- ASP.NET Core 5.0
- Entity Framework Core 5.0

## The architecture highlight

The Hotel.Backend.Api contains controllers, services
The Hotel.Backend.Infrastructure contains the connection to the database and repository
The Hotel.Backend.Domain contains domain models and business logic
