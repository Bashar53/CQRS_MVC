using AutoMapper;
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
public record DeleteEmployeeCommand(int id) : IRequest<CommandResult<VMEmployee>>;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, CommandResult<VMEmployee>>
{
    private readonly IEmployeeRepository _employeerepository;
    private readonly IValidator<DeleteEmployeeCommand> _validator;
    private readonly IMapper _mapper;
    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IValidator<DeleteEmployeeCommand> validator, IMapper mapper)
    {
        _employeerepository = employeeRepository;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<CommandResult<VMEmployee>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);
        if (!validator.IsValid) throw new ValidationException(validator.Errors);
        var result = await _employeerepository.DeleteAsync(request.id);
        return result switch
        {
            null => new CommandResult<VMEmployee>(null, CommandResultTypeEnum.NotFound),
            _ => new CommandResult<VMEmployee>(result, CommandResultTypeEnum.Success)
        };
    }
}

