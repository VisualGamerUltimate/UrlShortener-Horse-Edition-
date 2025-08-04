# Use the official ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj and restore as distinct layers
COPY ["UrlShortener.UserService/UrlShortener.UserService.csproj", "UrlShortener.UserService/"]
COPY ["UrlShortener.Shared.Models/UrlShortener.Shared.Models.csproj", "UrlShortener.Shared.Models/"]
COPY ["UrlShortener.Shared.Infrastructure/UrlShortener.Shared.Infrastructure.csproj", "UrlShortener.Shared.Infrastructure/"]
RUN dotnet restore "UrlShortener.UserService/UrlShortener.UserService.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/UrlShortener.UserService"
RUN dotnet publish -c Release -o /app/publish

# Final stage: run the app
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UrlShortener.UserService.dll"]
