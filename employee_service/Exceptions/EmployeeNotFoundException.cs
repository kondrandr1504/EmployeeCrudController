using System;

namespace Employee.Service
{
  /// <summary>
  ///     Класс исключения в случае отсутствия сотрудника в бд
  /// </summary>
  public class EmployeeNotFoundException : ApplicationException
  {
    public EmployeeNotFoundException() { }

    public EmployeeNotFoundException(string message) : base(message) { }

    public EmployeeNotFoundException(string message, Exception inner) : base(message, inner) { }
  }
}
