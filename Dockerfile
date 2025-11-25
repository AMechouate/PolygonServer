# Use the official .NET 8.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj file and restore dependencies
COPY PolygonApi/PolygonApi.csproj PolygonApi/
RUN dotnet restore PolygonApi/PolygonApi.csproj

# Copy everything else and build
COPY PolygonApi/ PolygonApi/
WORKDIR /src/PolygonApi
RUN dotnet publish -c Release -o /app/publish

# Use the .NET 8.0 runtime image for running
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published app from build stage
COPY --from=build /app/publish .

# Expose port (Railway will set PORT env variable)
EXPOSE 5104

# Set environment variable for port
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-5104}

# Run the app
ENTRYPOINT ["dotnet", "PolygonApi.dll"]

