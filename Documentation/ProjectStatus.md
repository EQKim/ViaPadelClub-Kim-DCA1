# Project Status

## Current Backend Setup

- Backend: ASP.NET Core Web API
- Backend host: Render Web Service
- Live API URL: `https://viapadelclub-kim-dca1.onrender.com`
- Database: Neon PostgreSQL
- Database connection env var: `VIAPADELCLUB_POSTGRES_CONNECTION_STRING`
- Deployment: Docker-based Render deployment using the root `Dockerfile`

## Completed Assignments

- Assignment 5: Commands and handlers
- Assignment 6: Command dispatcher
- Assignment 7: PostgreSQL persistence with Neon-compatible setup
- Assignment 8: CQRS query contracts and Postgres read model queries
- Assignment 9: Presentation layer with ASP.NET Core Web API, REPR-style endpoints, object mapper, and presentation tests

## Current API Endpoints

- `GET /openapi/v1.json`
- `GET /api/daily-schedules/upcoming`
- `POST /api/players/register`

## Local Development Notes

Set the Neon connection string before running the API locally:

```powershell
$env:VIAPADELCLUB_POSTGRES_CONNECTION_STRING = "<neon-postgres-connection-string>"
dotnet run --project src\Presentation\ViaPadelClub-Kim-DCA1.Presentation.WebApi\ViaPadelClub-Kim-DCA1.Presentation.WebApi.csproj
```

Run tests:

```powershell
dotnet test --no-restore
```

Integration tests are skipped when `VIAPADELCLUB_POSTGRES_CONNECTION_STRING` is not set.

## Next Work

- Build a frontend application.
- Add CORS support to the Web API before calling it from a browser-hosted frontend.
- Decide where to host the frontend, for example GitHub Pages, Render Static Site, Netlify, or Vercel.
