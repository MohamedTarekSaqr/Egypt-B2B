# API

Base URL during local development:

```text
http://localhost:5088
```

Swagger is available in development:

```text
http://localhost:5088/swagger
```

## Health

```http
GET /api/health
```

Example response:

```json
{
  "status": "Healthy",
  "timestampUtc": "2026-04-26T20:34:09.9165074Z"
}
```

## Authentication

JWT bearer authentication is wired, but register/login endpoints are not implemented yet.

Configured policies:

- `AdminOnly`
- `SupplierOnly`
- `BuyerOnly`

Configured roles:

- `Admin`
- `Supplier`
- `Buyer`

As endpoints are added, this document should list routes, authorization requirements, request bodies, and response shapes.
