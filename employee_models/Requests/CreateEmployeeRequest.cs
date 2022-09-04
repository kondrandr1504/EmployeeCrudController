using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Employee.Models
{
  /// <summary>
  ///     Параметры  запроса для создания сотрудников
  /// </summary>
  public class CreateEmployeeRequest
  {
    /// <summary>
    /// ФИО сотрудника
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Позиция
    /// </summary>
    [JsonProperty("position")]
    public string Position { get; set; }
  }
}