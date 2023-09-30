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
public record CreateStateCommand(VMState state) : IRequest<CommandResult<VMState>>;


public class CreateStateCommandHandler : IRequestHandler<CreateStateCommand, CommandResult<VMState>>
{
    private readonly IStateRepository _stateRepository;
    private readonly IValidator<CreateStateCommand> _validator;
    private readonly IMapper _mapper;

    public CreateStateCommandHandler(IStateRepository stateRepository, IValidator<CreateStateCommand> validator, IMapper mapper)
    {
        _stateRepository = stateRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<CommandResult<VMState>> Handle(CreateStateCommand request, CancellationToken cancellationToken)
    {
        var validate = await _validator.ValidateAsync(request, cancellationToken);
        if (!validate.IsValid)
            throw new ValidationException(validate.Errors);
        var result = _mapper.Map<State>(request.state);
        var state = await _stateRepository.InsertAsync(result);
        return state switch
        {
            null => new CommandResult<VMState>(null, CommandResultTypeEnum.NotFound),
            _ => new CommandResult<VMState>(state, CommandResultTypeEnum.Success)
        };

    }

}
