using AutoMapper;
using Employee.Model;
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

public record CreateEmployeeCommand(VMEmployee employee) : IRequest<CommandResult<VMEmployee>>;



   
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CommandResult<VMEmployee>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<CreateEmployeeCommand> _validator;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IValidator<CreateEmployeeCommand> validator, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CommandResult<VMEmployee>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var validate = await _validator.ValidateAsync(request, cancellationToken);
            if (!validate.IsValid)

                throw new ValidationException(validate.Errors);
            var result = _mapper.Map<Employees>(request.employee);

            var employee = await _employeeRepository.InsertAsync(result);

            return employee switch
            {
                null => new CommandResult<VMEmployee>(null, CommandResultTypeEnum.NotFound),

                _ => new CommandResult<VMEmployee>(employee, CommandResultTypeEnum.Success)

            };
        }
    }

