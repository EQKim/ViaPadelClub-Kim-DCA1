# Assignment 7 - Persistence Status

## Persistence Choice

This project uses PostgreSQL hosted in Neon instead of SQLite.
This still follows the assignment direction because it uses Entity Framework Core, a `DbContext`, repository implementations, Unit of Work, and integration tests against a real database.

## Completed

* [x] Infrastructure persistence project
* [x] Entity Framework Core packages
* [x] PostgreSQL provider through `Npgsql.EntityFrameworkCore.PostgreSQL`
* [x] Domain project reference
* [x] `DmContext`
* [x] Design-time context factory
* [x] Entity configurations for:
  * [x] `Player`
  * [x] `DailySchedule`
  * [x] `DailyScheduleCourt`
  * [x] `Booking`
* [x] Generic repository interface in Domain
* [x] Generic repository base implementation
* [x] `PlayerRepository`
* [x] `DailyScheduleRepository`
* [x] Unit of Work implementation
* [x] Separate integration test project
* [x] Repository integration tests

## Implemented Files

* `src/Infrastructure/ViaPadelClub-Kim-DCA1.Infrastructure.PostgresDmPersistence/DmContext.cs`
* `src/Infrastructure/ViaPadelClub-Kim-DCA1.Infrastructure.PostgresDmPersistence/DmContextFactory.cs`
* `src/Infrastructure/ViaPadelClub-Kim-DCA1.Infrastructure.PostgresDmPersistence/Configurations`
* `src/Infrastructure/ViaPadelClub-Kim-DCA1.Infrastructure.PostgresDmPersistence/Repositories`
* `src/Infrastructure/ViaPadelClub-Kim-DCA1.Infrastructure.PostgresDmPersistence/UnitOfWork.cs`
* `tests/IntegrationTests/Repositories`

## Local Setup

Set the Neon connection string before running integration tests or EF design-time tools:

```powershell
$env:VIAPADELCLUB_POSTGRES_CONNECTION_STRING = "Host=...;Database=...;Username=...;Password=...;SSL Mode=Require"
```

The connection string is intentionally not committed to Git.

## Test Notes

The integration tests use `Database.EnsureCreatedAsync()` and unique aggregate IDs.
They are skipped automatically when `VIAPADELCLUB_POSTGRES_CONNECTION_STRING` is not set.

## Optional Work

* [ ] Add migrations if the course expects migration files
* [ ] Add dependency injection registration for persistence services
* [ ] Add soft delete challenge
