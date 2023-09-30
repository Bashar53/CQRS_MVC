
using Employee.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Shared
{
    public  interface IVM <T> where T : IEquatable<T>

    {
        T Id { get; set; }
    }


}
public interface IVM : IVM<int> { }

