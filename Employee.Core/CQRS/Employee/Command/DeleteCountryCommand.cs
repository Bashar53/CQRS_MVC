using AutoMapper;
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
public record DeleteCountryCommand(int id) : IRequest<CommandResult<VMCountry>>;

public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, CommandResult<VMCountry>>
{
    private readonly ICountryRepository _countryrepository;
    private readonly IValidator<DeleteCountryCommand> _validator;
    private readonly IMapper _mapper;
    public DeleteCountryCommandHandler(ICountryRepository countryRepository, IValidator<DeleteCountryCommand> validator, IMapper mapper)
    {
        _countryrepository = countryRepository;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<CommandResult<VMCountry>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);
        if (!validator.IsValid) throw new ValidationException(validator.Errors);
        var result = await _countryrepository.DeleteAsync(request.id);
        return result switch
        {
            null => new CommandResult<VMCountry>(null, CommandResultTypeEnum.NotFound),
            _ => new CommandResult<VMCountry>(result, CommandResultTypeEnum.Success)
        };
    }
}

