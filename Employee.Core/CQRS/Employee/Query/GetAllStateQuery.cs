using Employee.Repository.Concrete;
using Employee.Repository.Interface;
using Employee.Service.Model;
using Employee.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.CQRS.Employee.Query;
public record GetAllStateQuery() : IRequest<QueryResult<IEnumerable<VMState>>>;

public class GetAllStateQueryHandler : IRequestHandler<GetAllStateQuery, QueryResult<IEnumerable<VMState>>>
{
    private readonly IStateRepository _stateRepository;
    public GetAllStateQueryHandler(IStateRepository stateRepository)
    {
        _stateRepository = stateRepository;
    }

    public async Task<QueryResult<IEnumerable<VMState>>> Handle(GetAllStateQuery request, CancellationToken cancellationToken)
    {
        var state = await _stateRepository.GetAllAsync();
        return state switch
        {
            null => new QueryResult<IEnumerable<VMState>>(null, QueryResultTypeEnum.NotFound),
            _ => new QueryResult<IEnumerable<VMState>>(state, QueryResultTypeEnum.Success)
        };
    }

}
