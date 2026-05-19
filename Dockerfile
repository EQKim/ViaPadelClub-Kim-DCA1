FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore ViaPadelClub-Kim.sln
RUN dotnet publish src/Presentation/ViaPadelClub-Kim-DCA1.Presentation.WebApi/ViaPadelClub-Kim-DCA1.Presentation.WebApi.csproj \
    --configuration Release \
    --output /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["sh", "-c", "dotnet ViaPadelClub-Kim-DCA1.Presentation.WebApi.dll --urls http://0.0.0.0:${PORT:-8080}"]
