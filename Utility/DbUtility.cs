using System;

namespace SampleApp_Review.Utility;

public static class DbUtility
{
    private const string DATABASE_HOST = "DATABASE_HOST";
    private const string DATABASE_PORT = "DATABASE_PORT";
    private const string DATABASE_NAME = "DATABASE_NAME";
    private const string DATABASE_USERNAME = "DATABASE_USERNAME";
    private const string DATABASE_PASSWORD = "DATABASE_PASSWORD";

    public static string GetConnectionString()
    {
        string host = GetEnvironmentVariable(DATABASE_HOST);
        string port = GetEnvironmentVariable(DATABASE_PORT);
        string database = GetEnvironmentVariable(DATABASE_NAME);
        string username = GetEnvironmentVariable(DATABASE_USERNAME);
        string password = GetEnvironmentVariable(DATABASE_PASSWORD);

        return $"Host={host};Port={port};Database={database};Username={username};Password={password}";
    }

    private static string GetEnvironmentVariable(string variableName)
    {
        string? value = Environment.GetEnvironmentVariable(variableName);
        
        if (value == null)
        {
            throw new InvalidOperationException($"Required environment variable '{variableName}' is not set.");
        }
        
        return value;
    }
}