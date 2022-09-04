using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.NameTranslation;

namespace Employee.Data
{
  /// <summary>
  ///     Контекст БД службы пользователей
  /// </summary>
  [PublicAPI]
  public sealed class EmployeeDatabaseContext : DbContext
  {
    /// <summary>
    ///     .ctor
    /// </summary>
    public EmployeeDatabaseContext(DbContextOptions<EmployeeDatabaseContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Таблица пользователей
    /// </summary>
    public DbSet<EmployeeEntity> Employee { get; set; }

    /// <summary>
    /// Настроить параметры контекста БД
    /// </summary>
    public static void Configure(DbContextOptionsBuilder options, string connectionString)
    {
      // NpgsqlDiagnostics.OnConfiguring(connectionString);
      options.UseNpgsql(
          connectionString,
          opts => opts.MigrationsHistoryTable("__schema_migrations")
      );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      var mapper = new NpgsqlSnakeCaseNameTranslator();
      foreach (var entity in modelBuilder.Model.GetEntityTypes())
      {
        var storeObjectId = StoreObjectIdentifier.Table(entity.GetTableName(), entity.GetSchema());
        foreach (var property in entity.GetProperties())
        {
          // Проставляем имя поля по умолчанию (snake_case)
          property.SetColumnName(mapper.TranslateMemberName(property.GetColumnName(storeObjectId)));
        }
      }

      EmployeeEntity.Setup(modelBuilder.Entity<EmployeeEntity>());
    }
  }
}
