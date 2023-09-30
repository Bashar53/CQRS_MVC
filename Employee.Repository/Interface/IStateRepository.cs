using Employee.Model;
using Employee.Service.Model;
using Employee.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Repository.Interface
{
    public interface IStateRepository :  IRepository<State, VMState, int>
    {

    }
}
