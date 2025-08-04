# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY UrlShortener.ApiGateway/UrlShortener.ApiGateway.csproj UrlShortener.ApiGateway/
RUN dotnet restore UrlShortener.ApiGateway/UrlShortener.ApiGateway.csproj

# Copy the rest of the source
COPY . .
WORKDIR /src/UrlShortener.ApiGateway
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80

# Copy published app
COPY --from=build /app/publish . 

#  Copy ocelot.json into the same folder as the published DLL
COPY UrlShortener.ApiGateway/ocelot.json ./ocelot.json

# Install curl (for manual container testing)
RUN apt-get update && apt-get install -y curl && apt-get clean

# Run the app
ENTRYPOINT ["dotnet", "UrlShortener.ApiGateway.dll"]
