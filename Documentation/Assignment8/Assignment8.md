# Assignment 8 - CQRS Status

## Completed

* [x] Core Query Contracts project
* [x] Infrastructure Query project
* [x] Query dispatcher
* [x] Query handler interface
* [x] Query-side EF Core read model
* [x] Query-side DbContext mapped to the PostgreSQL tables
* [x] `GetUpcomingDailySchedulesQuery`
* [x] `GetPlayerDirectoryQuery`
* [x] Query dispatcher unit tests
* [x] Query handler integration tests
* [x] Self-designed view sketch

## Implemented Projects

* `src/Core/ViaPadelClub-Kim-DCA1.Core.QueryContracts`
* `src/Infrastructure/ViaPadelClub-Kim-DCA1.Infrastructure.PostgresQueries`

## Implemented Queries

### Upcoming Daily Schedules

Used for the book-court view. It returns the upcoming daily schedules, including:

* schedule id
* schedule status
* schedule window
* courts
* VIP-only flag
* active booking count
* booking details

The default count is three schedules.

### Player Directory

Self-designed view. It returns players with optional filtering by VIP and banned status.
This view could be used by club staff to review player access and membership state.

## Test Notes

Query integration tests run against Neon PostgreSQL when this environment variable is set:

```powershell
$env:VIAPADELCLUB_POSTGRES_CONNECTION_STRING = "Host=...;Database=...;Username=...;Password=...;SSL Mode=Require"
```

Without the connection string, the integration tests are skipped.

## Optional Work

* [ ] Scaffold read models directly with `dotnet ef dbcontext scaffold`
* [ ] Add query caching for the upcoming schedules query
* [ ] Add dependency injection registration for query handlers
