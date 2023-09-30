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

public record GetAllStateByIdQuery : IRequest<QueryResult<VMState>>
{
    [JsonConstructor]
    public GetAllStateByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }

    public class GetAllStateByIdQueryHandler : IRequestHandler<GetAllStateByIdQuery, QueryResult<VMState>>
    {
        private readonly IStateRepository _stateRepository;
        private readonly IValidator<GetAllStateByIdQuery> _validator;
        public GetAllStateByIdQueryHandler(IStateRepository stateRepository, IValidator<GetAllStateByIdQuery> validator)
        {
            _stateRepository = stateRepository;
            _validator = validator;
        }
        public async Task<QueryResult<VMState>> Handle(GetAllStateByIdQuery request, CancellationToken cancellationToken)
        {
            var validate = await _validator.ValidateAsync(request, cancellationToken);
            if (!validate.IsValid)
                throw new ValidationException(validate.Errors);
            var state = await _stateRepository.GetByIdAsync(request.Id);
            return state switch
            {
                null => new QueryResult<VMState>(null, QueryResultTypeEnum.NotFound),
                _ => new QueryResult<VMState>(state, QueryResultTypeEnum.Success)
            };


        }
    }



}
