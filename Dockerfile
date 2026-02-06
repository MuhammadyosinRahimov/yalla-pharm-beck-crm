# Backend Dockerfile for YallaPharm CRM API
# .NET 7.0 application

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src

# Copy solution and project files
COPY YallaPharm.CRM.sln .
COPY YallaPharm.API/YallaPharm.API.csproj YallaPharm.API/
COPY YallaPharm.Application/YallaPharm.Application.csproj YallaPharm.Application/
COPY YallaPharm.Domain/YallaPharm.Domain.csproj YallaPharm.Domain/
COPY YallaPharm.Infrastructure/YallaPharm.Infrastructure.csproj YallaPharm.Infrastructure/

# Restore dependencies
RUN dotnet restore

# Copy all source code
COPY . .

# Build the application
RUN dotnet build YallaPharm.API/YallaPharm.API.csproj -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish YallaPharm.API/YallaPharm.API.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS runtime
WORKDIR /app

# Install curl for health checks
RUN apk add --no-cache curl

# Create non-root user for security
RUN adduser -D -u 1001 appuser

# Copy published files
COPY --from=publish /app/publish .

# Set ownership
RUN chown -R appuser:appuser /app

# Switch to non-root user
USER appuser

# Expose port
EXPOSE 5071

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5071
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=10s --retries=3 \
    CMD curl -f http://localhost:5071/health || exit 1

# Entry point
ENTRYPOINT ["dotnet", "YallaPharm.API.dll"]
