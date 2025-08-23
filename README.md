# SampleApp Review

A modern .NET 9.0 Web API project demonstrating best practices for error monitoring, observability, and database management with Sentry integration and OpenTelemetry support.

## 🚀 Features

- **.NET 9.0 Web API** with modern C# features
- **Sentry Integration** for comprehensive error monitoring and performance tracking
- **OpenTelemetry** support for distributed tracing and metrics
- **PostgreSQL Database** with Entity Framework Core
- **Swagger/OpenAPI** documentation
- **Source Link** support for better debugging experience
- **Environment-based Configuration** using .env files
- **Weather Forecast API** as a sample endpoint

## 🛠️ Prerequisites

- .NET 9.0 SDK
- PostgreSQL database
- Sentry account and project
- Sentry CLI (for source mapping)

## 📦 Installation & Setup

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd SampleApp_Review
```

### 2. Environment Configuration

Copy the template environment file and configure your settings:

```bash
cp SampleApp_Review/template.env SampleApp_Review/.env
```

Edit `.env` file with your actual values:

```env
DATABASE_HOST=localhost
DATABASE_PORT=5432
DATABASE_NAME=sampleapp_review
DATABASE_USERNAME=your_username
DATABASE_PASSWORD=your_password
SENTRY_DSN=https://your-sentry-dsn@sentry.io/your-project-id
```

### 3. Database Setup

Run the Entity Framework migrations:

```bash
cd SampleApp_Review
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run
```

The API will be available at:
- **API**: https://localhost:7001
- **Swagger UI**: https://localhost:7001/swagger

## 🔍 Sentry Integration

This project includes comprehensive Sentry integration for error monitoring, performance tracking, and debugging.

### Configuration

Sentry is configured in `Program.cs` with the following features:
- Error monitoring and crash reporting
- Performance monitoring and profiling
- Request tracking with PII data
- Entity Framework integration
- OpenTelemetry integration

### Source Mapping

To enable proper stack trace mapping in Sentry (showing actual source code instead of compiled assembly names), you need to upload source maps using Sentry CLI.

#### Prerequisites

1. Install Sentry CLI:
   ```bash
   # macOS
   brew install getsentry/tools/sentry-cli
   
   # Windows
   choco install sentry-cli
   
   # Linux
   curl -sL https://sentry.io/get-cli/ | bash
   ```

2. Authenticate with Sentry:
   ```bash
   sentry-cli login
   ```

#### Upload Source Maps

After building your project in Debug mode, upload the source maps:

```bash
sentry-cli debug-files upload -o shahabcoding -p shahab_sample --include-sources ./bin/Debug/net9.0
```

**Command Breakdown:**
- `-o shahabcoding`: Your Sentry organization name
- `-p shahab_sample`: Your Sentry project name
- `--include-sources`: Includes source code for better debugging
- `./bin/Debug/net9.0`: Path to your debug build output

#### Build Configuration

The project is configured to generate proper debug symbols and source information:

```xml
<PropertyGroup>
    <DebugSymbols>true</DebugSymbols>
    <DebugType>portable</DebugType>
    <PublishRepositoryUrl>true</PublishRepositoryUrl>
    <EmbedUntrackedSources>true</EmbedUntrackedSources>
    <EmbedAllSources>true</EmbedAllSources>
</PropertyGroup>
```

### Benefits of Source Mapping

With source maps uploaded to Sentry:
- Stack traces show actual source code file names and line numbers
- Better debugging experience for production issues
- Ability to view source code context directly in Sentry
- Improved error resolution workflow

## 🏗️ Project Structure

```
SampleApp_Review/
├── Controllers/
│   └── WeathersController.cs          # Sample API endpoint
├── Discount/                          # Discount strategy pattern implementation
│   ├── CheckoutService.cs
│   ├── ChristmasDiscount.cs
│   ├── IDiscountStrategy.cs
│   └── LoyaltyDiscount.cs
├── Migrations/                        # Entity Framework migrations
├── Utility/
│   └── DbUtility.cs                  # Database connection utilities
├── ApplicationDbContext.cs            # Entity Framework context
├── Program.cs                         # Application entry point
├── WeatherForecast.cs                 # Data model
└── appsettings.json                   # Configuration files
```

## 🧪 Testing

### Run Tests
```bash
dotnet test
```

### Test Docker Environment
```bash
./scripts/test-docker.sh
```

### Simulate CI Pipeline
```bash
./scripts/simulate-ci.sh
```

## 📊 API Endpoints

### Weather Forecast
- **GET** `/get-weather-forecast` - Returns a 5-day weather forecast

## 🔧 Development

### Adding New Controllers
1. Create a new controller in the `Controllers/` directory
2. Inherit from `ControllerBase`
3. Add appropriate routing attributes
4. Register in dependency injection if needed

### Database Changes
1. Modify your entity models
2. Create a new migration: `dotnet ef migrations add MigrationName`
3. Update the database: `dotnet ef database update`

### Environment Variables
- Use `.env` file for local development
- Set environment variables in production
- Never commit sensitive information to source control

## 🚀 Deployment

### Build for Production
```bash
dotnet publish -c Release
```

### Docker Support
The project includes Docker scripts for containerized deployment:
- `scripts/test-docker.sh` - Test Docker environment
- `scripts/simulate-ci.sh` - Simulate CI/CD pipeline

## 📚 Dependencies

### Core Packages
- **Microsoft.AspNetCore.OpenApi** - OpenAPI support
- **Microsoft.EntityFrameworkCore** - ORM framework
- **Npgsql.EntityFrameworkCore.PostgreSQL** - PostgreSQL provider
- **Swashbuckle.AspNetCore** - Swagger documentation

### Monitoring & Observability
- **Sentry.AspNetCore** - Error monitoring
- **Sentry.OpenTelemetry** - OpenTelemetry integration
- **OpenTelemetry.Exporter.OpenTelemetryProtocol** - OTLP exporter

### Development Tools
- **DotNetEnv** - Environment variable management
- **Microsoft.SourceLink.GitHub** - Source linking support

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For issues and questions:
- Check the [Sentry documentation](https://docs.sentry.io/)
- Review OpenTelemetry [best practices](https://opentelemetry.io/docs/)
- Open an issue in this repository

## 🔄 Version History

- **v1.0.0** - Initial release with Sentry integration and OpenTelemetry support
