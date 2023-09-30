using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.CQRS.Employee.Query.Validator
{
    public class GetAllStateByIdQueryValidator : AbstractValidator<GetAllStateByIdQuery>
    {
        public GetAllStateByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");
        }
    }
}
