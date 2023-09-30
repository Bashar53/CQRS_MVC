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

public record GetAllCountryQuery() : IRequest<QueryResult<IEnumerable<VMCountry>>>;

public class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQuery, QueryResult<IEnumerable<VMCountry>>>
{
    private readonly ICountryRepository _countryRepository;
    public GetAllCountryQueryHandler(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }
    public async Task<QueryResult<IEnumerable<VMCountry>>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetAllAsync();
        return country switch
        {
            null => new QueryResult<IEnumerable<VMCountry>>(null, QueryResultTypeEnum.NotFound),
            _ => new QueryResult<IEnumerable<VMCountry>>(country, QueryResultTypeEnum.Success)
        };
    }
}
