namespace Employee.Frontend.Models;

public class Employees

{

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// FirstName
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    /// <summary>
    /// LastName
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
    /// PhoneNumber
    /// </summary>

    public string? PhoneNumber { get; set; }
    /// <summary>
    /// Country Id
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// State Id
    /// </summary>

    public int StateId { get; set; }




}
