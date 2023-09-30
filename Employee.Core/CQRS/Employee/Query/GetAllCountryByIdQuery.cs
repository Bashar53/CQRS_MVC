using Employee.Model;
using Employee.Repository.Concrete;
using Employee.Repository.Interface;
using Employee.Service.Model;
using Employee.Shared.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Employee.Core.CQRS.Employee.Query;

public record GetAllCountryByIdQuery : IRequest<QueryResult<VMCountry>>
{
    [JsonConstructor]
    public GetAllCountryByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }

    public class GetAllCountryByIdQueryHandler : IRequestHandler<GetAllCountryByIdQuery, QueryResult<VMCountry>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IValidator<GetAllCountryByIdQuery> _validator;
        public GetAllCountryByIdQueryHandler(ICountryRepository countryRepository, IValidator<GetAllCountryByIdQuery> validator)
        {
            _countryRepository = countryRepository;
            _validator = validator;
        }
        public async Task<QueryResult<VMCountry>> Handle(GetAllCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var validate = await _validator.ValidateAsync(request, cancellationToken);
            if (!validate.IsValid)
                throw new ValidationException(validate.Errors);
            var country = await _countryRepository.GetByIdAsync(request.Id);
            return country switch
            {
                null => new QueryResult<VMCountry>(null, QueryResultTypeEnum.NotFound),
                _ => new QueryResult<VMCountry>(country, QueryResultTypeEnum.Success)
            };


        }
    }



}
