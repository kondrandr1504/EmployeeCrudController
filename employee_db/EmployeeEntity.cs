using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Data
{
  /// <summary>
  /// Работники
  /// </summary>
  [PublicAPI]
  [Table("employee")]
  public class EmployeeEntity
  {
    /// <summary>
    ///     ID
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// ФИО работника
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Позиция
    /// </summary>
    public string Position { get; set; }

    /// <summary>
    /// Настройка
    /// </summary>
    /// <param name="builder"></param>
    internal static void Setup(EntityTypeBuilder<EmployeeEntity> builder)
    {
      // колонки обязательные
      builder.Property(_ => _.Id).IsRequired();
      builder.Property(_ => _.Name).IsRequired();
      builder.Property(_ => _.Position).IsRequired();
      builder.HasIndex(_ => _.Id).IsUnique();
      builder.HasIndex(_ => _.Name).IsUnique();
    }
  }
}
