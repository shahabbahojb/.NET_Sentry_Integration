using Microsoft.EntityFrameworkCore;

namespace SampleApp_Review;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<WeatherForecastEntity> WeatherForecasts { get; set; } = null!;
}

public class EntityBase
{
    public Guid Id { get; set; }
}

public class WeatherForecastEntity : EntityBase
{
    public string Content { get; set; } = null!;
}