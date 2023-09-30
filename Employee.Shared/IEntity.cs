using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Shared;

public  interface IEntity<T> where T : IEquatable<T>
{
    T Id { get; set; }

}

public interface IEntity : IEntity<int> { }
