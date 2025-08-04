# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY UrlShortener.UrlService/UrlShortener.UrlService.csproj UrlShortener.UrlService/
COPY UrlShortener.Shared.Models/UrlShortener.Shared.Models.csproj UrlShortener.Shared.Models/
COPY UrlShortener.Shared.Infrastructure/UrlShortener.Shared.Infrastructure.csproj UrlShortener.Shared.Infrastructure/
RUN dotnet restore UrlShortener.UrlService/UrlShortener.UrlService.csproj

# Copy entire source tree
COPY . .

# Publish the application
WORKDIR /src/UrlShortener.UrlService
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./

ENTRYPOINT ["dotnet", "UrlShortener.UrlService.dll"]
