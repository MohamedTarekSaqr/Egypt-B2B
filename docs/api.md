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

JWT bearer authentication is implemented for registration, login, and current-user lookup.

Configured policies:

- `AdminOnly`
- `SupplierOnly`
- `BuyerOnly`

Configured roles:

- `Admin`
- `Supplier`
- `Buyer`

### Register

```http
POST /api/auth/register
```

Authorization: anonymous

Self-registration supports only:

- `Supplier`
- `Buyer`

`Admin` accounts must not be created through public registration.

Request:

```json
{
  "fullName": "Ahmed Hassan",
  "email": "ahmed@example.com",
  "phoneNumber": "+201001234567",
  "password": "Password123",
  "role": "Supplier"
}
```

Successful response: `201 Created`

```json
{
  "userId": "00000000-0000-0000-0000-000000000000",
  "fullName": "Ahmed Hassan",
  "email": "ahmed@example.com",
  "phoneNumber": "+201001234567",
  "roles": ["Supplier"],
  "accessToken": "jwt-token",
  "expiresAtUtc": "2026-04-28T12:00:00Z"
}
```

Common errors:

- `400 Bad Request` for invalid input, weak password, disallowed role, or missing configured role
- `409 Conflict` when the email address is already registered

### Login

```http
POST /api/auth/login
```

Authorization: anonymous

Request:

```json
{
  "email": "ahmed@example.com",
  "password": "Password123"
}
```

Successful response: `200 OK`

```json
{
  "userId": "00000000-0000-0000-0000-000000000000",
  "fullName": "Ahmed Hassan",
  "email": "ahmed@example.com",
  "phoneNumber": "+201001234567",
  "roles": ["Supplier"],
  "accessToken": "jwt-token",
  "expiresAtUtc": "2026-04-28T12:00:00Z"
}
```

Common errors:

- `401 Unauthorized` for invalid credentials
- `403 Forbidden` for inactive accounts

### Current User

```http
GET /api/auth/me
```

Authorization: JWT bearer token

Successful response: `200 OK`

```json
{
  "userId": "00000000-0000-0000-0000-000000000000",
  "fullName": "Ahmed Hassan",
  "email": "ahmed@example.com",
  "phoneNumber": "+201001234567",
  "roles": ["Supplier"]
}
```

Common errors:

- `401 Unauthorized` when no valid bearer token is supplied
- `403 Forbidden` for inactive accounts
- `404 Not Found` when the token references a user that no longer exists
