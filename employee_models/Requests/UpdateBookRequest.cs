using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Employee.Models
{
  /// <summary>
  ///     Параметры  запроса для обновления полей сотрудника
  /// </summary>
  public class UpdateEmployeeRequest
  {
    /// <summary>
    /// Позиция
    /// </summary>
    [JsonProperty("position")]
    public string Position { get; set; }

  }
}