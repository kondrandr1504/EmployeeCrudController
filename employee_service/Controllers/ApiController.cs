using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Employee.Data;
using Employee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Employee.Service
{
  [ApiController]
  [Route("[controller]")]
  public class ApiController : ControllerBase
  {
    private readonly EmployeeDatabaseContext _db;
    private readonly IMapper _mapper;
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<ApiController> _logger;

    /// <summary>
    ///     .ctor
    /// </summary>
    public ApiController(
        EmployeeDatabaseContext db,
        IMapper mapper,
        IEmployeeService employeeService,
        ILogger<ApiController> logger)
    {
      _mapper = mapper;
      _db = db;
      _employeeService = employeeService;
      _logger = logger;
    }

    #region GET /employee/

    /// <summary>
    /// Получение списка сотрудников
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    [HttpGet("~/employee/")]
    [ProducesResponseType(typeof(PagedList<EmployeeResp>), 200)]
    [ProducesResponseType(400)]
    public IActionResult GetEmployee([FromQuery] ListEmployeeRequest query, CancellationToken cancellationToken)
    {
      var employee = _employeeService.GetEmployee(query, cancellationToken);
      var result = _mapper.Map<PagedList<EmployeeEntity>, PagedList<EmployeeResp>>(employee);
      return Ok(result);
    }

    #endregion

    #region GET /employee/{id}
    /// <summary>
    /// Получение сотрудника по id
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="cancellationToken">CancellationToken</param>
    [HttpGet("~/employee/{id}")]
    [ProducesResponseType(typeof(EmployeeResp), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetEmployeeById([FromRoute(Name = "id")] long employeeId, CancellationToken cancellationToken)
    {
      var employeeById = await _employeeService.GetEmployeeByIdAsync(employeeId, cancellationToken);
      var result = _mapper.Map<EmployeeEntity, EmployeeResp>(employeeById);
      return Ok(result);
    }

    #endregion

    #region POST /employee/

    /// <summary>
    /// Создание сотрудника
    /// </summary>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    [HttpPost("~/employee")]
    [ProducesResponseType(typeof(EmployeeResp), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostEmployee([FromBody] CreateEmployeeRequest query, CancellationToken cancellationToken)
    {
      var (employee, error) = await _employeeService.PostEmployeeAsync(query, cancellationToken);
      if (error != null)
      {
        return Conflict(error.Message);
      }
      var result = _mapper.Map<EmployeeResp>(employee);

      return Ok(result);
    }

    #endregion

    #region PUT /employee/{id}

    /// <summary>
    /// Изменение сотрудника
    /// </summary>
    /// <param name="name"></param>
    /// <param name="query">Параметры запроса</param>
    /// <param name="cancellationToken">CancellationToken</param>
    [HttpPut("~/employee/{name}")]
    [ProducesResponseType(typeof(EmployeeResp), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PutEmployee([FromRoute(Name = "name")] string name, [FromBody] UpdateEmployeeRequest query, CancellationToken cancellationToken)
    {
      var (employee, error) = await _employeeService.PutEmployeeAsync(name, query, cancellationToken);
      if (error != null)
      {
        return Conflict(error.Message);
      }
      var result = _mapper.Map<EmployeeResp>(employee);
      return Ok(result);
    }

    #endregion

    #region DELETE /employee/{name}

    /// <summary>
    ///     Удалить сотрудника
    /// </summary>
    /// <param name="name">ФИО сотрудника</param>
    /// <param name="cancellationToken">CancellationToken</param>
    [HttpDelete("~/employee/{name}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> EmployeeDelete([FromRoute(Name = "name")] string name, CancellationToken cancellationToken)
    {
      var (result, error) = await _employeeService.DeleteEmployeeAsync(name, cancellationToken);
      if (error != null)
      {
        return Conflict(error.Message);
      }

      return Ok(result);
    }

    #endregion
  }
}
