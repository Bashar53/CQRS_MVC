using Employee.Shared;
using Employee.Shared.Common;
using System.Text.Json.Serialization;

namespace Employee.Model;

public class Employees : BaseAuditableEntity, IEntity
{
    /// <summary>
    /// id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// First Name
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    /// <summary>
    /// Last Name
    /// </summary>
    public string? LastName { get; set; }
    /// <summary>
    /// Address
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// Age
    /// </summary>
    public int Age { get; set; }
    /// <summary>
    /// Phone Number
    /// </summary>
    public string? PhoneNumber { get; set; }
    public int CountryId { get; set; }
    public Country? Country { get; set; }

    public int StateId { get; set; }

    public State? State { get; set; }
}
