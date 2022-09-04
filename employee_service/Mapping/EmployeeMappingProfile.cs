using AutoMapper;
using Employee.Data;
using Employee.Models;

namespace Employee.Service
{
  /// <summary>
  ///     Профиль маппинга для службы Сотрудники
  /// </summary>
  public class EmployeeMappingProfile : Profile
  {
    /// <summary>
    /// .ctor
    /// </summary>
    public EmployeeMappingProfile()
    {
      CreateMap<EmployeeEntity, EmployeeResp>();
      CreateMap<PagedList<EmployeeEntity>, PagedList<EmployeeResp>>();
      CreateMap<CreateEmployeeRequest, EmployeeEntity>();
      CreateMap<UpdateEmployeeRequest, EmployeeEntity>();
    }
  }
}
