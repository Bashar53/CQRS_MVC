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

public record UpdateEmployeeCommand(int id, VMEmployee employee) : IRequest<CommandResult<VMEmployee>>;

public  class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, CommandResult<VMEmployee>>

{

    private readonly IEmployeeRepository _employeerepository;
    private readonly IValidator<UpdateEmployeeCommand> _validator;
    private readonly IMapper _mapper;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IValidator<UpdateEmployeeCommand> validator, IMapper mapper)
    {
        _employeerepository = employeeRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<CommandResult<VMEmployee>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var validator = await _validator.ValidateAsync(request, cancellationToken);
        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);
        var data = _mapper.Map<Employees>(request.employee);
        var result = await _employeerepository.UpdateAsync(request.id, data);
        return result switch
        {
            null => new CommandResult<VMEmployee>(null, CommandResultTypeEnum.UnprocessableEntity),
            _ => new CommandResult<VMEmployee>(result, CommandResultTypeEnum.Success)
        };
    }




}

