using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.CQRS.Employee.Query.Validator
{
    public  class GetAllEmployeeByIdQueryValidator:AbstractValidator<GetAllEmployeeByIdQuery>
    {
        public GetAllEmployeeByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");
        }
    }
}
