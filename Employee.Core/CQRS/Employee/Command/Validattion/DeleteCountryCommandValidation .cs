using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.CQRS.Employee.Command.Validattion;

public class DeleteCountryCommandValidation : AbstractValidator<DeleteCountryCommand>
{
    public DeleteCountryCommandValidation()
    {
        RuleFor(x => x.id).NotEmpty().WithMessage("Id is Required");
    }
}
