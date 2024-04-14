using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities;

public class UserEntity : IdentityUser
{

    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;
    public DateTime? DeletionRequest { get; set; }
    public string? ProfileImage { get; set; } = "avatar1.jpg";
    public string? Bio {  get; set; } 
    public bool IsDeleted { get; set; }

    public int? AddressId { get; set; }
    public AddressEntity? Address { get; set; }

}
