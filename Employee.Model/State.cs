using Employee.Shared.Common;
using Employee.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Model;

public class State : BaseAuditableEntity, IEntity
{
    public int Id { get; set; }
    public string? StateName { get; set; }
    public int CountryId { get; set; }
    public Country? Country { get; set; }

    public ICollection<Employees> Employee { get; set; } = new HashSet<Employees>();
}


