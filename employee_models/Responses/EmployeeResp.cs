using Newtonsoft.Json;

namespace Employee.Models
{
  /// <summary>
  /// Сотрудники
  /// </summary>
  public sealed class EmployeeResp
  {
    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

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
