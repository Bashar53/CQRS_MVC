using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.CQRS.Employee.Command.Validattion
{
    public class CreateEmployeeCommandValidation : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidation()
        {
            RuleFor(x => x.employee).NotEmpty();
        }
    }
}
