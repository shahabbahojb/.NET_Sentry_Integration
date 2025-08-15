# SampleApp_Review

A .NET 9.0 web application with integrated Sentry monitoring, OpenTelemetry tracing, and automated CI/CD pipeline.

## Features

- **.NET 9.0** web API with Entity Framework Core
- **Sentry** integration for error monitoring and performance tracking
- **OpenTelemetry** for distributed tracing
- **PostgreSQL** database support
- **Swagger** API documentation
- **Docker** containerization
- **GitHub Actions** CI/CD pipeline
- **Automated source mapping** for Sentry

## CI/CD Pipeline

The project includes a comprehensive GitHub Actions pipeline that:

1. **Builds** the application once with debug symbols
2. **Publishes** the application to `./build-output`
3. **Uploads source maps** to Sentry using the build output
4. **Creates Docker image** using the same pre-built artifacts
5. **Pushes image** to GitHub Container Registry
6. **Creates Sentry release** with deployment tracking

### Single Build Approach

The pipeline ensures that the exact same build artifacts are used for:

- Sentry source mapping (`./build-output`)
- Docker image creation (copies from `./build-output`)

This guarantees perfect consistency between your deployed application and Sentry debugging information.

### Required GitHub Secrets

Add these secrets to your GitHub repository:

- `SENTRY_AUTH_TOKEN`: Your Sentry authentication token

### Pipeline Triggers

The pipeline runs on:

- Push to `main`, `develop`, or `setup-open-telemetry` branches
- Pull requests to `main` branch

## Local Development

### Prerequisites

- .NET 9.0 SDK
- Docker and Docker Compose
- PostgreSQL (or use Docker Compose)

### Environment Variables

Create a `.env` file in the project root:

```env
SENTRY_DSN=your_sentry_dsn_here
CONNECTION_STRING=Host=localhost;Database=sampleapp_review;Username=postgres;Password=postgres
```

### Running with Docker Compose (Local Development)

```bash
# Start all services (app + PostgreSQL) - uses Dockerfile.local
docker-compose up -d

# View logs
docker-compose logs -f app

# Stop services
docker-compose down
```

### Testing CI/CD Process Locally

```bash
# Simulate the exact CI/CD build process
./scripts/simulate-ci.sh

# Test local development build
./scripts/test-docker.sh
```

### Running Locally

```bash
# Restore dependencies
dotnet restore SampleApp_Review/SampleApp_Review.csproj

# Run the application
dotnet run --project SampleApp_Review/SampleApp_Review.csproj
```

## Docker Image

The application is automatically built and pushed to GitHub Container Registry:

```bash
# Pull the latest image
docker pull ghcr.io/shahabbahojb/sentry_integration:latest

# Run the container
docker run -p 8080:8080 \
  -e SENTRY_DSN=your_dsn \
  -e ConnectionStrings__DefaultConnection=your_connection_string \
  ghcr.io/shahabbahojb/sentry_integration:latest
```

## Sentry Integration

### Source Mapping

The pipeline automatically uploads debug symbols and source code to Sentry using:

```bash
sentry-cli debug-files upload -o shahabcoding -p shahab_sample --include-sources ./build-output
```

This ensures that:

- Stack traces show original source code
- Error locations are accurately mapped
- Debugging is easier in production

### Release Tracking

Each deployment creates a Sentry release with:

- Git commit information
- Source code upload
- Deployment tracking

## API Endpoints

- **Health Check**: `GET /health`
- **Weather Forecast**: `GET /WeatherForecast`
- **Swagger UI**: `GET /swagger` (Development only)

## Database Migrations

```bash
# Add migration
dotnet ef migrations add MigrationName --project SampleApp_Review

# Update database
dotnet ef database update --project SampleApp_Review
```

## Monitoring

The application includes:

- **Sentry** for error tracking and performance monitoring
- **OpenTelemetry** for distributed tracing
- **Health checks** for container monitoring
- **Structured logging** with Sentry integration

## Architecture

```text
├── Controllers/          # API controllers
├── Discount/            # Business logic (Strategy pattern)
├── Migrations/          # EF Core migrations
├── Properties/          # Launch settings
├── Utility/             # Database utilities
├── Dockerfile           # Container definition
├── docker-compose.yml   # Local development setup
└── .github/workflows/   # CI/CD pipeline
```

## Contributing

1. Create a feature branch
2. Make your changes
3. Ensure tests pass (when implemented)
4. Create a pull request

The CI/CD pipeline will automatically:

- Build and test your changes
- Create a preview Docker image
- Upload source maps for debugging

## License

This project is licensed under the MIT License.
