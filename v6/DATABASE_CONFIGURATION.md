# Database Configuration Guide

The OEAPI .NET implementation supports two database providers, chosen at deployment time:

- **Microsoft SQL Server** (default)
- **PostgreSQL**

See the [README](README.md#configuration) for the overall configuration structure
(`Database`/`Authentication`/`Service`); this guide covers the database provider in more depth.

## Configuration

### 1. appsettings.json Configuration

Configure your database provider and connection strings in the `appsettings.json` file:

```json
{
  "Database": {
    "Provider": "SqlServer",
    "ConnectionStrings": {
      "SqlServer": "Server=(localdb)\\mssqllocaldb;Database=OEAPIDatabase;Trusted_Connection=True;MultipleActiveResultSets=true",
      "PostgreSQL": "Host=localhost;Port=5432;Database=OEAPIDatabase;Username=postgres;Password=yourpassword"
    }
  }
}
```

### 2. Supported Providers

#### SQL Server Configuration

**Provider**: `"SqlServer"`

**Connection String Examples**:

- LocalDB:
  `"Server=(localdb)\\mssqllocaldb;Database=OEAPIDatabase;Trusted_Connection=True;MultipleActiveResultSets=true"`
- Full SQL Server:
  `"Server=your-server-name;Database=OEAPIDatabase;User Id=your-username;Password=your-password;MultipleActiveResultSets=true"`
- SQL Express: `"Server=.\\SQLEXPRESS;Database=OEAPIDatabase;Trusted_Connection=True;MultipleActiveResultSets=true"`

**Required NuGet Packages**:

- Microsoft.EntityFrameworkCore.SqlServer

#### PostgreSQL Configuration

**Provider**: `"PostgreSQL"`

**Connection String Examples**:

- Local: `"Host=localhost;Port=5432;Database=OEAPIDatabase;Username=postgres;Password=yourpassword"`
- Remote: `"Host=your-db-server;Port=5432;Database=OEAPIDatabase;Username=your-username;Password=your-password"`
- With SSL:
  `"Host=your-db-server;Port=5432;Database=OEAPIDatabase;Username=your-username;Password=your-password;SSL Mode=Require;Trust Server Certificate=true"`

**Required NuGet Packages**:

- Npgsql.EntityFrameworkCore.PostgreSQL

### 3. Switching Database Providers

To switch between database providers:

1. **Update appsettings.json**:
   ```json
   {
     "Database": {
       "Provider": "PostgreSQL",
       "ConnectionStrings": {
         "SqlServer": "...",
         "PostgreSQL": "Host=localhost;Port=5432;Database=OEAPIDatabase;Username=postgres;Password=yourpassword"
       }
     }
   }
   ```

2. **Ensure the required packages are installed** (they are included in the Infrastructure project)

3. **Run database migrations** for the new provider:
   ```bash
   dotnet ef database update --project src\OEAPI.Infrastructure
   ```

## Environment-Specific Configuration

### Development Configuration

Edit `appsettings.Development.json` to override the production settings:

```json
{
  "Database": {
    "Provider": "SqlServer",
    "ConnectionStrings": {
      "SqlServer": "Server=(localdb)\\mssqllocaldb;Database=OEAPIDatabase_Dev;Trusted_Connection=True",
      "PostgreSQL": "Host=localhost;Port=5432;Database=OEAPIDatabase_Dev;Username=postgres;Password=devpassword"
    }
  }
}
```

### Production Configuration

For production environments:

1. **Set environment variables** (recommended):
   ```bash
   # Windows
   set Database__Provider=SqlServer
   set Database__ConnectionStrings__SqlServer=Server=prod-server;Database=OEAPI;User Id=appuser;Password=apppassword;
   
   # Linux/macOS
   export Database__Provider=SqlServer
   export Database__ConnectionStrings__SqlServer=Server=prod-server;Database=OEAPI;User Id=appuser;Password=apppassword;
   ```

2. **Or use appsettings.Production.json**:
   ```json
   {
     "Database": {
       "Provider": "SqlServer",
       "ConnectionStrings": {
         "SqlServer": "Server=prod-server;Database=OEAPI;User Id=appuser;Password=apppassword;"
       }
     }
   }
   ```

## Database Migrations

### Creating Migrations

```bash
# Install EF Core tools globally (if not already installed)
dotnet tool install --global dotnet-ef

# Create a new migration
dotnet ef migrations add AddLearningOutcomes --project src/OEAPI.Infrastructure --startup-project src/OEAPI.API
```

### Applying Migrations

Migrations are automatically applied in development mode when the application starts. For production:

```bash
# Apply migrations manually
dotnet ef database update --project src/OEAPI.Infrastructure --startup-project src/OEAPI.API
```

## Troubleshooting

### Common Issues

1. **"Unsupported database provider"**: Ensure the Provider value in appsettings.json is exactly "SqlServer" or "
   PostgreSQL"
2. **"Connection string not configured"**: Verify the connection string for your selected provider is not empty
3. **Missing packages**: Ensure all required NuGet packages are restored (`dotnet restore`)
4. **Migration errors**: Re-run `dotnet ef database update` (see [Applying Migrations](#applying-migrations)) after
   fixing the underlying schema/model mismatch. **Avoid `dotnet ef database drop`** as a quick fix, especially with
   connection strings supplied via environment variables: `dotnet ef`'s design-time tooling only reads
   `appsettings.json` directly and ignores environment-variable overrides (`Database__ConnectionStrings__...`), so it's
   easy to point it at a different database than the one your running application actually uses - including, in a
   shared/production setup, a real database instead of the intended throwaway one.

### Connection String Validation

You can test your connection string using:

```csharp
// For SQL Server
using var connection = new SqlConnection(yourConnectionString);
connection.Open();

// For PostgreSQL
using var connection = new NpgsqlConnection(yourConnectionString);
connection.Open();
```

## Performance Considerations

### Connection Pooling

Both SQL Server and PostgreSQL providers use connection pooling by default. For high-traffic applications, you may want
to configure pool settings:

```json
{
  "Database": {
    "Provider": "SqlServer",
    "ConnectionStrings": {
      "SqlServer": "Server=your-server;Database=OEAPI;...;Max Pool Size=200;Min Pool Size=10;"
    }
  }
}
```

### Retry logic

Not enabled by default. Add it yourself in `Program.cs`'s `ConfigureDatabaseServices` if transient
connection failures are a concern in your environment:

```csharp
options.UseSqlServer(connectionString, sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null);
});
```

## Security Best Practices

1. **Never commit production connection strings** to source control
2. **Use environment variables** for sensitive information
3. **Rotate passwords** regularly
4. **Use integrated authentication** where possible (Trusted_Connection for SQL Server)
5. **Enable SSL** for PostgreSQL connections in production

## Extending Database Support

To add support for additional database providers:

1. Add the required NuGet package to `OEAPI.Infrastructure.csproj`
2. Add a new case in the `ConfigureDatabaseServices` method in `Program.cs`
3. Update the configuration in `DatabaseConfiguration.cs`
4. Add connection string validation

Example for adding MySQL support:

```csharp
// In OEAPI.Infrastructure.csproj
<PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="10.0.0" />

// In DatabaseConfiguration.cs
public string MySQL { get; set; } = string.Empty;

// In Program.cs ConfigureDatabaseServices method
case "MySQL":
    builder.Services.AddDbContext<OEAPIDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    break;
```