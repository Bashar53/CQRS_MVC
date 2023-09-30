using Employee.Repository.Interface;
using Employee.Service.Model;
using Employee.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.CQRS.Employee.Query
{

    public record GetAllEmployeeQuery() : IRequest<QueryResult<IEnumerable<VMEmployee>>>;

    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, QueryResult<IEnumerable<VMEmployee>>>
    {

        private readonly IEmployeeRepository _employeeRepository;
        public GetAllEmployeeQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<QueryResult<IEnumerable<VMEmployee>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetAllAsync();

            return employee switch
            {
                null => new QueryResult<IEnumerable<VMEmployee>>(null, QueryResultTypeEnum.NotFound),
                _ => new QueryResult<IEnumerable<VMEmployee>>(employee, QueryResultTypeEnum.Success)
            };



        }
    }
}
