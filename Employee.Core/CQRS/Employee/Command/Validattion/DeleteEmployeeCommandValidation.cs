using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.CQRS.Employee.Command.Validattion;


public class DeleteEmployeeCommandValidation : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidation()
    {
        RuleFor(x => x.id).NotEmpty().WithMessage("Id is Required");
    }
}

