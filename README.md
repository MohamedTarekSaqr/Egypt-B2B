# Egypt B2B Marketplace

Production-oriented B2B marketplace platform for Egyptian manufacturers, wholesalers, suppliers, and buyers.

## Current Status

Backend foundation is in place:

- ASP.NET Core Web API targeting .NET 8
- Clean Architecture projects: Domain, Application, Infrastructure, API
- SQL Server persistence with EF Core
- ASP.NET Core Identity with `Admin`, `Supplier`, and `Buyer` roles
- JWT configuration and policy wiring
- Initial database migration
- Health endpoint
- Smoke tests for API and Application layers

Frontend has not been scaffolded yet.

## Tech Stack

- Backend: ASP.NET Core Web API (.NET 8)
- Architecture: Clean Architecture
- Database: SQL Server
- ORM: Entity Framework Core
- Auth: ASP.NET Core Identity + JWT bearer tokens
- Tests: xUnit
- Frontend: React + TypeScript, planned

## Solution Structure

```text
src/
  EgyptB2B.Domain/
    Common/
    Entities/
    Enums/
    ValueObjects/

  EgyptB2B.Application/
    Common/
      Interfaces/
      Models/
      Security/

  EgyptB2B.Infrastructure/
    Auth/
    Identity/
    Persistence/
      Configurations/
      Migrations/
    Services/

  EgyptB2B.Api/
    Controllers/
    Extensions/
    Middleware/
    Services/

tests/
  EgyptB2B.Application.Tests/
  EgyptB2B.Api.Tests/
```

## Documentation

- [Architecture](docs/architecture.md)
- [Database Schema](docs/database-schema.md)
- [API](docs/api.md)
- [Development Workflow](docs/development.md)

Documentation should be updated in the same step as code when changes affect setup, architecture, schema, commands, endpoints, auth behavior, or business rules.

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server or SQL Server LocalDB
- EF Core local tool restored from `dotnet-tools.json`

### Restore

```powershell
dotnet restore EgyptB2B.slnx
dotnet tool restore
```

### Build

```powershell
dotnet build EgyptB2B.slnx
```

### Run Tests

```powershell
dotnet test EgyptB2B.slnx
```

### Apply Database Migrations

The default development connection string uses SQL Server LocalDB:

```json
"Server=(localdb)\\MSSQLLocalDB;Database=EgyptB2B;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

Apply migrations:

```powershell
dotnet tool run dotnet-ef database update --project src/EgyptB2B.Infrastructure --startup-project src/EgyptB2B.Infrastructure
```

### Run API

```powershell
dotnet run --project src/EgyptB2B.Api --urls http://localhost:5088
```

Health check:

```text
GET http://localhost:5088/api/health
```

Swagger is available in development:

```text
http://localhost:5088/swagger
```

## MVP Scope

Planned backend features:

- User registration and login
- Role-based access for Admin, Supplier, and Buyer
- Supplier profile management
- Product categories
- Product CRUD
- Product search and filtering
- Inquiry system
- Admin approval flow for suppliers and products

## Engineering Notes

- Domain entities own business state and invariants.
- Application owns use-case contracts and cross-cutting models.
- Infrastructure owns EF Core, Identity, SQL Server, JWT generation, and external implementations.
- API owns HTTP concerns only: controllers, auth wiring, middleware, request context.
- New feature work should include focused tests and documentation updates.
