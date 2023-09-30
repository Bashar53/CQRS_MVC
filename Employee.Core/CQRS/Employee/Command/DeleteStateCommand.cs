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

public record DeleteStateCommand(int id) : IRequest<CommandResult<VMState>>;

public class DeleteStateCommandHandler : IRequestHandler<DeleteStateCommand, CommandResult<VMState>>
{

    private readonly IStateRepository _staterepository;
    private readonly IValidator<DeleteStateCommand> _validator;
    private readonly IMapper _mapper;

    public DeleteStateCommandHandler(IStateRepository stateRepository, IValidator<DeleteStateCommand> validator, IMapper mapper)
    {
        _staterepository = stateRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<CommandResult<VMState>> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);
        if (!validator.IsValid) throw new ValidationException(validator.Errors);
        var result = await _staterepository.DeleteAsync(request.id);
        return result switch
        {
            null => new CommandResult<VMState>(null, CommandResultTypeEnum.NotFound),
            _ => new CommandResult<VMState>(result, CommandResultTypeEnum.Success)
        };
    }

}
