using Employee.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Service.Model;

public class VMCountry : IVM
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Country Name
    /// </summary>

    public string? CountryName { get; set; }
    /// <summary>
    /// Currency
    /// </summary>
    public string? Currency { get; set; }
}
