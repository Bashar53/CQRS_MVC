using AutoMapper;
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
using System.Threading.Tasks;

namespace Employee.Core.CQRS.Employee.Command;

public record CreateCountryCommand(VMCountry country) : IRequest<CommandResult<VMCountry>>;

public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CommandResult<VMCountry>>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IValidator<CreateCountryCommand> _validator;
    private readonly IMapper _mapper;

    public CreateCountryCommandHandler(ICountryRepository countryRepository, IValidator<CreateCountryCommand> validator, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<CommandResult<VMCountry>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var validate = await _validator.ValidateAsync(request, cancellationToken);
        if (!validate.IsValid)
            throw new ValidationException(validate.Errors);
        var result = _mapper.Map<Country>(request.country);
        var country = await _countryRepository.InsertAsync(result);
        return country switch
        {
            null => new CommandResult<VMCountry>(null, CommandResultTypeEnum.NotFound),
            _ => new CommandResult<VMCountry>(country, CommandResultTypeEnum.Success)
        };

    }
}

