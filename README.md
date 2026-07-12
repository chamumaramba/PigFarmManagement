# Pig Farm Management

Pig Farm Management is an ASP.NET Core solution structured into API, Application, Domain, Infrastructure, and Blazor client projects.

## Local setup

The API deliberately does not keep credentials in source control. Configure a local JWT signing key before running it:

```powershell
cd PigFarmManagement.Api
dotnet user-secrets set "Jwt:Key" "replace-with-a-random-secret-at-least-32-bytes-long"
```

The issuer and audience are non-secret settings in `appsettings.json`. For production, provide `Jwt__Key` through the deployment platform's secret store instead of user secrets.

Optional development-only admin seeding requires both values below. When they are absent, no default administrator is created.

```powershell
dotnet user-secrets set "DevelopmentSeed:AdminEmail" "admin@example.com"
dotnet user-secrets set "DevelopmentSeed:AdminPassword" "use-a-unique-strong-password"
```

Apply database migrations before starting the API:

```powershell
dotnet ef database update --project ..\PigFarmManagement.Infrastructure --startup-project .
```

## Authentication

- `POST /api/auth/register` creates an Identity account.
- `POST /api/auth/login` returns a short-lived JWT and a refresh token.
- `POST /api/auth/refresh` rotates a valid refresh token; the old token cannot be used again.
- `POST /api/auth/revoke` revokes a refresh token.
- Farm endpoints require the `Admin` role.

Refresh tokens are stored as SHA-256 hashes, so the database does not contain usable bearer credentials.

## Development checks

```powershell
dotnet build PigFarmManagement.slnx
```

Run this before committing. The project currently has a transitive SQLite vulnerability warning; update the EF Core/SQLite dependency chain before deployment.
