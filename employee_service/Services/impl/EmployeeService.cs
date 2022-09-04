using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Employee.Data;
using Employee.Models;

namespace Employee.Service
{
  /// <summary>
  ///     Сервис Сотрудники
  /// </summary>
  public class EmployeeService : IEmployeeService
  {
    private readonly EmployeeDatabaseContext _db;
    private readonly ILogger<EmployeeService> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    ///     .ctor
    /// </summary>
    public EmployeeService(
      EmployeeDatabaseContext db,
      ILogger<EmployeeService> logger,
      IMapper mapper
    )
    {
      _db = db;
      _logger = logger;
      _mapper = mapper;
    }

    #region public Methods

    /// <summary>
    /// Получение списка сотрудников
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public PagedList<EmployeeEntity> GetEmployee(ListEmployeeRequest query, CancellationToken cancellationToken)
    {
      var employee = _db.Employee.AsQueryable();

      var skip = query.Skip ?? 0;
      var max = query.Max ?? employee.Count();
      var pagedResult = employee.Skip(skip).Take(max);

      var result = new PagedList<EmployeeEntity>
      {
        Items = pagedResult.ToArray(),
        TotalCount = employee.Count(),
        Pager = new PagerInfo
        {
          Skip = skip,
          Max = max
        }
      };

      return result;
    }

    /// <summary>
    ///      Получить сотрудника по id
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="cancellationToken">ct</param>
    /// <returns></returns>
    public async Task<EmployeeEntity> GetEmployeeByIdAsync(long employeeId, CancellationToken cancellationToken)
    {
      var employee = await _db.Employee.FirstOrDefaultAsync(_ => _.Id == employeeId, cancellationToken);
      return employee;
    }

    /// <summary>
    /// Создание сотрудника
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public async Task<ErrorTuple<EmployeeEntity>> PostEmployeeAsync(CreateEmployeeRequest query, CancellationToken cancellationToken)
    {
      try
      {
        var newEmployee = _mapper.Map<EmployeeEntity>(query);

        using var t = await _db.Database.BeginTransactionAsync(cancellationToken);
        var employeeExist = await _db.Employee.FirstOrDefaultAsync(_ => _.Name == query.Name, cancellationToken);
        if (employeeExist != null)
        {
          return new Error { Message = "Сотрудник с таким именем уже существует" };
        }

        var employee = await _db.Employee.AddAsync(newEmployee, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        await t.CommitAsync(cancellationToken);
        return employee.Entity;
      }
      catch (Exception e)
      {
        _logger.LogError(e, $"PostEmployeeAsync ended with error for {JsonConvert.SerializeObject(query)}");
        throw new Exception("", e);
      }
    }

    /// <summary>
    /// Изменение сотрудника
    /// </summary>
    /// <param name="Name">ФИО сотрудника</param>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public async Task<ErrorTuple<EmployeeEntity>> PutEmployeeAsync(string Name, UpdateEmployeeRequest query, CancellationToken cancellationToken)
    {
      try
      {
        using var t = await _db.Database.BeginTransactionAsync(cancellationToken);

        var employeeExist = await _db.Employee.FirstOrDefaultAsync(e => e.Name == Name, cancellationToken);
        if (employeeExist == null)
        {
          return new Error { Message = "Сотрудника с таким именем не существует" };
        }

        var newEmployee = _mapper.Map<EmployeeEntity>(query);

        employeeExist.Name = newEmployee.Name ?? Name;
        employeeExist.Position = newEmployee.Position ?? "";

        var result = _db.Update(employeeExist);
        await _db.SaveChangesAsync(cancellationToken);

        await t.CommitAsync(cancellationToken);
        return result.Entity;
      }
      catch (Exception e)
      {
        _logger.LogError(e, $"PutEmployeeAsync ended with error for {JsonConvert.SerializeObject(query)}");
        throw new Exception("", e);
      }
    }

    /// <summary>
    ///     Удалить сотрудника
    /// </summary>
    /// <param name="name">ФИО сотрудника</param>
    /// <param name="cancellationToken">CancellationToken</param>
    public async Task<ErrorTuple<bool>> DeleteEmployeeAsync(string name, CancellationToken cancellationToken)
    {
      try
      {
        using var t = await _db.Database.BeginTransactionAsync(cancellationToken);
        var employeeForDelete = await _db.Employee.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        if (employeeForDelete == null)
        {
          return new Error { Message = "Сотрудника с таким именем не существует" };
        }

        _db.Employee.Remove(employeeForDelete);
        await _db.SaveChangesAsync(cancellationToken);
        await t.CommitAsync(cancellationToken);
      }
      catch (Exception e)
      {
        _logger.LogInformation($"DeleteEmployeeAsync ended with error");
        throw new Exception($"DeleteEmployeeAsync ended with error", e);
      }

      return true;
    }

    #endregion
  }
}