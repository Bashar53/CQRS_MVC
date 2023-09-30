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
public record UpdateCountryCommand(int id, VMCountry country) : IRequest<CommandResult<VMCountry>>;

public  class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CommandResult<VMCountry>>
{
    private readonly ICountryRepository _countryrepository;
    private readonly IValidator<UpdateCountryCommand> _validator;
    private readonly IMapper _mapper;


  
    public UpdateCountryCommandHandler(ICountryRepository countryRepository, IValidator<UpdateCountryCommand> validator, IMapper mapper)
    {
        _countryrepository = countryRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<CommandResult<VMCountry>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);
        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);
        var data = _mapper.Map<Country>(request.country);
        var result = await _countryrepository.UpdateAsync(request.id, data);
        return result switch
        {
            null => new CommandResult<VMCountry>(null, CommandResultTypeEnum.UnprocessableEntity),
            _ => new CommandResult<VMCountry>(result, CommandResultTypeEnum.Success)
        };
    }
}
