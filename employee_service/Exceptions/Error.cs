using JetBrains.Annotations;

namespace Employee.Service
{
  /// <summary>
  ///     Пользовательский контракт ошибки
  /// </summary>
  public class Error
  {
    public string Message { get; set; }
  }

  /// <summary>
  ///     Обертка для <see cref="Error"/>.
  ///     Содержит либо <see cref="Error{T}._value"/>, либо <see cref="Error{T}._error"/>
  /// </summary>
  [PublicAPI]
  public readonly struct ErrorTuple<T>
  {
    [CanBeNull]
    private readonly T _value;

    [CanBeNull]
    private readonly Error _error;

    private ErrorTuple([CanBeNull] T value, [CanBeNull] Error error)
    {
      _value = value;
      _error = error;
    }

    /// <summary>
    ///     Неявное преобразование из <typeparamref name="T"/>
    /// </summary>
    public static implicit operator ErrorTuple<T>(T value) => new(value, default);

    /// <summary>
    ///     Неявное преобразование из <see cref="Error"/>
    /// </summary>
    public static implicit operator ErrorTuple<T>(Error error) => new(default, error);

    /// <summary>
    ///     .dector
    /// </summary>
    public void Deconstruct([CanBeNull] out T value, [CanBeNull] out Error error)
    {
      value = _value;
      error = _error;
    }
  }
}