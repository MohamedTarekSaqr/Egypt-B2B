# Architecture

The backend uses Clean Architecture to keep business logic independent from ASP.NET Core, EF Core, SQL Server, and JWT implementation details.

## Project Responsibilities

### EgyptB2B.Domain

Owns business concepts and rules:

- Entities such as `SupplierProfile`, `Product`, `Category`, and `Inquiry`
- Enums such as `ApprovalStatus`, `ProductStatus`, and `InquiryStatus`
- Value objects such as `Address` and `Money`
- Base entity types for audit and soft-delete behavior

Domain should not reference Application, Infrastructure, or API.

### EgyptB2B.Application

Owns use-case contracts and shared application models:

- `IApplicationDbContext`
- `ICurrentUserService`
- `IJwtTokenGenerator`
- `IDateTimeProvider`
- `Result`
- `PaginatedList`
- role and policy constants

Feature commands, queries, DTOs, validators, and mappings will be added here as the MVP grows.

### EgyptB2B.Infrastructure

Owns implementation details:

- EF Core `ApplicationDbContext`
- Entity configurations and migrations
- ASP.NET Core Identity user, role, and refresh token models
- JWT token generation
- System clock implementation

Infrastructure references Application and Domain.

### EgyptB2B.Api

Owns HTTP and hosting concerns:

- Controllers
- Authentication and authorization setup
- Swagger setup
- Exception handling middleware
- Current user service backed by `HttpContext`

API should delegate business work to Application features as those are added.

## Current Authentication Shape

Roles are seeded through EF Core:

- `Admin`
- `Supplier`
- `Buyer`

JWT bearer authentication and these policies are configured:

- `AdminOnly`
- `SupplierOnly`
- `BuyerOnly`

Actual register/login endpoints are planned for the next feature slice.

## Design Decisions

- Use `Guid` primary keys across domain and identity tables.
- Use ASP.NET Core Identity for password handling and role membership.
- Keep supplier and product approval statuses explicit in domain enums.
- Use soft delete for products so product history and inquiry references can remain stable.
- Start with SQL Server indexes and simple filters; full-text search can be added when search requirements mature.
