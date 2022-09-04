using Newtonsoft.Json;

namespace Employee.Models
{
  /// <summary>
  ///     Информация о пагинации
  /// </summary>
  public class PagerInfo
  {
    /// <summary>
    ///     Сколько записей пропускать
    /// </summary>
    [JsonProperty("skip")]
    public int? Skip { get; set; }

    /// <summary>
    ///     Сколько записей выдавать
    /// </summary>
    [JsonProperty("max")]
    public int? Max { get; set; }
  }
}
