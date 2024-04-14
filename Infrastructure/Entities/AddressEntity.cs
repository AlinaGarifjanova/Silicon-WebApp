using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities;

public class AddressEntity
{
    public int Id { get; set; }
    public string? AddressLine_1 { get; set; } 
    public string? AddressLine_2 { get; set;}
    public string? PostalCode { get; set; }
    public string? City { get; set; } 


}
