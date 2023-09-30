using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.CQRS.Employee.Command.Validattion;

public class UpdateCountryCommandValidation : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidation()
    {
        RuleFor(x => x.id).NotEmpty().WithMessage("Id is Requered");
    }
}
