using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Shared.Models
{
    public  class CommandResult<T>
    {
        public CommandResult()
        {

        }
        public CommandResult(T? result, CommandResultTypeEnum type)
        {
            Result = result;
            Type = type;
        }
        public T? Result { get; set; }
        public CommandResultTypeEnum Type { get; set; }
    }
}
