# Development Workflow

## Restore

```powershell
dotnet restore EgyptB2B.slnx
dotnet tool restore
```

## Build

```powershell
dotnet build EgyptB2B.slnx
```

## Test

```powershell
dotnet test EgyptB2B.slnx
```

## Format

```powershell
dotnet format EgyptB2B.slnx
```

To verify formatting without applying changes:

```powershell
dotnet format EgyptB2B.slnx --verify-no-changes
```

## Run API

```powershell
dotnet run --project src/EgyptB2B.Api --urls http://localhost:5088
```

## EF Core

This repo pins `dotnet-ef` through `dotnet-tools.json`.

Create a migration:

```powershell
dotnet tool run dotnet-ef migrations add MigrationName --project src/EgyptB2B.Infrastructure --startup-project src/EgyptB2B.Infrastructure --output-dir Persistence/Migrations
```

Apply migrations:

```powershell
dotnet tool run dotnet-ef database update --project src/EgyptB2B.Infrastructure --startup-project src/EgyptB2B.Infrastructure
```

Remove the latest migration:

```powershell
dotnet tool run dotnet-ef migrations remove --project src/EgyptB2B.Infrastructure --startup-project src/EgyptB2B.Infrastructure
```

## Documentation Rule

Update documentation in the same feature step when any of these change:

- Setup commands
- Solution structure
- Architecture decisions
- Database tables, columns, indexes, or migrations
- API endpoints, request bodies, responses, or authorization
- Business rules such as approval status transitions
- Environment variables or configuration keys

Recommended doc locations:

- `README.md` for project overview and common commands
- `docs/architecture.md` for design and project boundaries
- `docs/database-schema.md` for schema changes
- `docs/api.md` for endpoint contracts
- `docs/development.md` for local workflow
