# ChronoTrack API

ChronoTrack is a Clockify-inspired time tracking API built with ASP.NET Core, Clean Architecture, CQRS, DDD, and PostgreSQL.

The goal of this project is to build a backend-first time tracking system that can later support a React and TypeScript frontend.

## Project Purpose

ChronoTrack is intended to help users track work time against workspaces, clients, projects, tasks, and tags.

The backend is being built first so that the business rules, persistence, API design, and reporting foundations are stable before the frontend is added.

The frontend will be created later using React and TypeScript.

## Planned Features

The planned feature set includes:

- Workspace management
- User and member management
- Client management
- Project management
- Project tasks
- Tags
- Manual time entries
- Start and stop timer tracking
- Summary reports
- Detailed reports
- Time approval workflow
- Audit trail for important changes

## Architecture

The solution follows a four-layer Clean Architecture structure:

```text
ChronoTrack.Api
ChronoTrack.Application
ChronoTrack.Domain
ChronoTrack.Infrastructure
```

### ChronoTrack.Api

The API layer is responsible for HTTP concerns only.

This layer contains:

- Controllers
- Request models
- API startup extensions
- Middleware
- HTTP response handling
- Swagger/OpenAPI configuration

Controllers should stay thin. They should receive HTTP requests, dispatch commands or queries, and return responses.

Business logic should not live in controllers.

### ChronoTrack.Application

The application layer contains the use cases of the system.

This layer contains:

- Commands
- Queries
- Handlers
- Validators
- Application interfaces
- Read models
- DTOs where needed

Commands are used for operations that change state.

Queries are used for read-only operations.

Application handlers coordinate use cases, but domain rules should stay inside the Domain layer.

### ChronoTrack.Domain

The domain layer contains the core business logic.

This layer contains:

- Entities
- Value objects
- Domain events
- Domain exceptions
- Business rules

The domain layer must not depend on any other project.

This layer should stay independent from frameworks such as ASP.NET Core and Entity Framework Core.

### ChronoTrack.Infrastructure

The infrastructure layer contains external technical concerns.

This layer contains:

- EF Core database context
- PostgreSQL persistence setup
- Entity configuration
- Repository implementations
- External service integrations later

Infrastructure depends on Application and Domain so it can implement interfaces defined by the application layer.

## Project Dependencies

The dependency flow is:

```text
ChronoTrack.Api -> ChronoTrack.Application
ChronoTrack.Api -> ChronoTrack.Infrastructure

ChronoTrack.Application -> ChronoTrack.Domain

ChronoTrack.Infrastructure -> ChronoTrack.Application
ChronoTrack.Infrastructure -> ChronoTrack.Domain

ChronoTrack.Domain -> no project dependencies
```

## Technology Stack

The backend currently uses:

- ASP.NET Core
- C#
- Entity Framework Core
- PostgreSQL
- pgAdmin
- MediatR
- FluentValidation
- Swagger/OpenAPI

The frontend is planned for later and will use:

- React
- TypeScript

Testing projects will be added later when the project is ready for them.

## Branching Strategy

The repository uses this branch structure:

```text
main
development
feature/*
docs/*
```

### main

`main` is the stable branch.

It should only contain code that is considered stable or release-ready.

### development

`development` is the main integration branch.

Feature branches are merged into `development` through pull requests.

### feature/*

`feature/*` branches are used for application setup work and feature development.

Examples:

```text
feature/project-setup
feature/database-setup
feature/workspaces
feature/clients
```

### docs/*

`docs/*` branches are used for documentation-only changes.

Examples:

```text
docs/local-database-setup
```

## Local Development Requirements

To run the project locally, install:

- Visual Studio
- .NET 10 SDK
- PostgreSQL
- pgAdmin
- Git

## Local Database Setup

ChronoTrack uses PostgreSQL for local development.

Create a local PostgreSQL database named:

```text
chronotrack_dev
```

The project uses a code-first database approach with Entity Framework Core.

This means tables should not be manually created in pgAdmin. Tables will be created through EF Core migrations once the domain entities are added.

## Local Configuration

Local database settings should be stored in:

```text
ChronoTrack.Api/appsettings.Development.json
```

This file is ignored by Git and should not be committed.

The API expects a connection string named:

```text
DefaultConnection
```

under the `ConnectionStrings` section.

The connection string should point to the local PostgreSQL database.

The expected database details are:

```text
Host: localhost
Port: 5432
Database: chronotrack_dev
Username: postgres
Password: your local PostgreSQL password
```

The JSON structure should contain a `ConnectionStrings` object with a `DefaultConnection` value.

## Running the API

From the solution folder, build the project with:

```bash
dotnet build
```

To run the API from the command line:

```bash
dotnet run --project ChronoTrack.Api
```

The API can also be run directly from Visual Studio by setting `ChronoTrack.Api` as the startup project.

## Current Setup Status

The following setup steps have been completed:

- Created the Clean Architecture solution structure
- Added project references between the layers
- Added base backend packages
- Added dependency injection setup
- Added EF Core `ApplicationDbContext`
- Added PostgreSQL configuration through Infrastructure dependency injection
- Added API startup extension methods to keep `Program.cs` light

## Current Solution Structure

```text
ChronoTrack
|
|-- ChronoTrack.Api
|   |-- Controllers
|   |-- Extensions
|   |-- Properties
|   |-- appsettings.json
|   |-- appsettings.Development.json
|   |-- ChronoTrack.Api.http
|   |-- ChronoTrack.Api.csproj
|   |-- Program.cs
|
|-- ChronoTrack.Application
|   |-- DependencyInjection.cs
|   |-- ChronoTrack.Application.csproj
|
|-- ChronoTrack.Domain
|   |-- ChronoTrack.Domain.csproj
|
|-- ChronoTrack.Infrastructure
|   |-- Persistence
|   |   |-- ApplicationDbContext.cs
|   |-- DependencyInjection.cs
|   |-- ChronoTrack.Infrastructure.csproj
|
|-- .gitignore
|-- ChronoTrack.slnx
|-- README.md
```

Note: `appsettings.Development.json` is shown in the structure because it is needed locally, but it is ignored by Git and should not appear in the public repository.

## Notes

This project is still in early setup.

The next backend work will focus on shared domain foundations, then the first real feature slice.

The frontend will be added later after the API foundation is stable.