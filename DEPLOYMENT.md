# Deployment Guide

This guide helps you set up the complete CI/CD pipeline with Docker and Sentry integration.

## Prerequisites

### 1. GitHub Repository Setup

Ensure your repository is configured with the following:

- Repository name: `Sentry_Integration` (or update the workflow files accordingly)
- Owner: `shahabbahojb` (or update the workflow files accordingly)

### 2. Sentry Setup

1. Create a Sentry project
2. Get your Sentry DSN
3. Create a Sentry Auth Token with the following permissions:
   - `project:read`
   - `project:write` 
   - `project:releases`
   - `org:read`

### 3. GitHub Secrets

Add the following secret to your GitHub repository (`Settings > Secrets and variables > Actions`):

- `SENTRY_AUTH_TOKEN`: Your Sentry authentication token

## Deployment Process

### Automatic Deployment

The pipeline automatically triggers on:

- **Push to main branch**: Full build, Docker image, and production deployment
- **Push to develop branch**: Full build and Docker image with develop tag
- **Push to setup-open-telemetry branch**: Full build and Docker image with branch tag
- **Pull Request to main**: Validation only (no deployment)

### Manual Deployment

To manually trigger a deployment:

1. **Local Build Test**:
   ```bash
   ./scripts/test-docker.sh
   ```

2. **Push to trigger deployment**:
   ```bash
   git add .
   git commit -m "Deploy: your changes"
   git push origin main
   ```

## Pipeline Steps

### 1. Build Phase
- Restores .NET dependencies
- Builds application with debug symbols
- Creates build output in `./build-output`

### 2. Sentry Source Mapping
- Creates Sentry release using git commit SHA
- Uploads debug symbols and source code
- Associates commits with the release
- Finalizes the release

### 3. Docker Build & Push
- Builds Docker image using the same build output
- Tags with branch name and commit SHA
- Pushes to GitHub Container Registry (`ghcr.io`)

### 4. Deployment Tracking
- Marks the release as deployed in Sentry
- Links deployment to production environment

## Docker Images

Images are available at:
```
ghcr.io/shahabbahojb/sentry_integration:latest          # Latest main branch
ghcr.io/shahabbahojb/sentry_integration:main-<sha>      # Specific main commit
ghcr.io/shahabbahojb/sentry_integration:develop-<sha>   # Develop branch
```

## Environment Variables

### Required for Production
- `SENTRY_DSN`: Your Sentry Data Source Name
- `ConnectionStrings__DefaultConnection`: Database connection string

### Optional
- `SENTRY_RELEASE`: Release version (auto-set in CI/CD)
- `ASPNETCORE_ENVIRONMENT`: Environment name (Production/Development)

## Monitoring

### Sentry Features Enabled
- **Error Tracking**: Automatic error capture with source mapping
- **Performance Monitoring**: Request tracing and profiling
- **Release Tracking**: Deployment and commit association
- **Source Maps**: Original source code in stack traces

### Health Checks
- Container health: `GET /health`
- Application metrics via OpenTelemetry
- Database connectivity via Entity Framework

## Troubleshooting

### Source Mapping Issues
1. Verify `SENTRY_AUTH_TOKEN` secret is set
2. Check Sentry organization and project names in workflow
3. Ensure build includes debug symbols

### Docker Build Issues
1. Check Dockerfile syntax
2. Verify .dockerignore excludes unnecessary files
3. Test local build with `./scripts/test-docker.sh`

### Pipeline Failures
1. Check GitHub Actions logs
2. Verify secrets are properly set
3. Ensure branch protection rules allow pushes

## Security Considerations

- Secrets are only accessible to the pipeline
- Docker images are stored in private GitHub registry
- Source maps are uploaded to secure Sentry project
- Database connections use secure connection strings

## Scaling

To scale the deployment:

1. **Multiple Environments**: Create separate Sentry projects for staging/production
2. **Database Scaling**: Use connection pooling and read replicas
3. **Container Orchestration**: Deploy to Kubernetes or Docker Swarm
4. **Load Balancing**: Place container behind load balancer

## Maintenance

### Regular Tasks
- Update .NET dependencies monthly
- Review Sentry error trends weekly
- Clean up old Docker images quarterly
- Update GitHub Actions workflows as needed

### Monitoring
- Set up Sentry alerts for error spikes
- Monitor container resource usage
- Track deployment frequency and success rates
