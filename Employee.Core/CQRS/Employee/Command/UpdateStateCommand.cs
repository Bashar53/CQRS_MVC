using AutoMapper;
using Employee.Model;
using Employee.Repository.Concrete;
using Employee.Repository.Interface;
using Employee.Service.Model;
using Employee.Shared.Contracts;
using Employee.Shared.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.CQRS.Employee.Command;
public record UpdateStateCommand(int id, VMState state) : IRequest<CommandResult<VMState>>;


public class UpdateStateCommandHandler : IRequestHandler<UpdateStateCommand, CommandResult<VMState>>
{
    private readonly IStateRepository _staterepository;
    private readonly IValidator<UpdateStateCommand> _validator;
    private readonly IMapper _mapper;



    public UpdateStateCommandHandler(IStateRepository stateRepository, IValidator<UpdateStateCommand> validator, IMapper mapper)
    {
        _staterepository = stateRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<CommandResult<VMState>> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);
        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);
        var data = _mapper.Map<State>(request.state);
        var result = await _staterepository.UpdateAsync(request.id, data);
        return result switch
        {
            null => new CommandResult<VMState>(null, CommandResultTypeEnum.UnprocessableEntity),
            _ => new CommandResult<VMState>(result, CommandResultTypeEnum.Success)
        };
    }
}