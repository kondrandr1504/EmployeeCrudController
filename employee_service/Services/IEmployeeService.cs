using System.Threading;
using System.Threading.Tasks;
using Employee.Data;
using Employee.Models;

namespace Employee.Service
{
  /// <summary>
  ///     Сервис Сотрудники
  /// </summary>
  public interface IEmployeeService
  {
    /// <summary>
    /// Получение списка сотрудников
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public PagedList<EmployeeEntity> GetEmployee(ListEmployeeRequest query, CancellationToken cancellationToken);

    /// <summary>
    /// Получить сотрудника по id
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="cancellationToken">ct</param>
    /// <returns></returns>
    public Task<EmployeeEntity> GetEmployeeByIdAsync(long employeeId, CancellationToken cancellationToken);

    /// <summary>
    /// Создание сотрудника
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public Task<ErrorTuple<EmployeeEntity>> PostEmployeeAsync(CreateEmployeeRequest query, CancellationToken cancellationToken);

    /// <summary>
    /// Изменение сотрудника
    /// </summary>
    /// <param name="Name">ФИО сотрудника</param>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public Task<ErrorTuple<EmployeeEntity>> PutEmployeeAsync(string Name, UpdateEmployeeRequest query, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить сотрудника
    /// </summary>
    /// <param name="name">ФИО сотрудника</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public Task<ErrorTuple<bool>> DeleteEmployeeAsync(string name, CancellationToken cancellationToken);
  }
}