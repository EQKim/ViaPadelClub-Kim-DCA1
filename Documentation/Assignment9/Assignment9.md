# Assignment 9 - Presentation

## Status

Implemented.

## What Was Added

- `ViaPadelClub-Kim-DCA1.Presentation.WebApi`
  - Controller based ASP.NET Core Web API.
  - REPR-style endpoint base class in `Endpoints/Common/ApiEndpoint.cs`.
  - `POST /api/players/register` command endpoint.
  - `GET /api/daily-schedules/upcoming` query endpoint.

- `ViaPadelClub-Kim-DCA1.Core.Tools.ObjectMapper`
  - `IObjectMapper`
  - `IMapping<TSource, TDestination>`
  - `ObjectMapper`
  - DI registration extension.

- Dependency injection extension methods
  - Application command handlers and command dispatcher.
  - Postgres domain model persistence.
  - Postgres query handlers and query dispatcher.

- Presentation tests
  - Object mapper unit tests.
  - Endpoint integration tests for success, validation failure, and exception handling.

## Neon Alignment

This assignment aligns with the Neon setup from Assignment 7 and Assignment 8.
The Web API reads the Postgres connection string from:

```text
VIAPADELCLUB_POSTGRES_CONNECTION_STRING
```

No database password is stored in source code. When the environment variable is present, the API uses the real Neon-backed write model and query model. When it is missing, the API starts but the database-backed dispatchers report that the connection string is not configured.

## Remaining Deployment Step

For hosting, the backend should be deployed to a service that supports ASP.NET Core and environment variables. GitHub Pages can host a static frontend, but it cannot host this Web API backend.
