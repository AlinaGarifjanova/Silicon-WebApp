using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Factories;

public class UserFactory(DataContext context)
{
   private readonly DataContext _context = context;

    public static UserEntity Create(string firstName, string lastName, string email)
    {
        try
        {
            return new UserEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email

            };
        }
        catch { }
        return null!;
    }

    public static User Create(UserEntity userEntity)
    {
        try
        {
            return new User
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email!,
                Bio = userEntity.Bio,   
                UserName = userEntity.Email!,
                Phone = userEntity.PhoneNumber,
                AddressLine1 = userEntity.Address?.AddressLine_1,
                AddressLine2 = userEntity.Address?.AddressLine_2,
                PostalCode = userEntity.Address?.PostalCode,
                City = userEntity.Address?.City,
                IsDeleted = userEntity.IsDeleted,
                DeletionRequest = userEntity.DeletionRequest,
                
            };
        }
        catch { }
        return null!;
    }

    public static IEnumerable<User> Create(List<UserEntity> list)
    {
        var users = new List<User>();

        try
        {
            foreach (var user in list)
                users.Add(Create(user));
        }
        catch { }
        return users;
    }

}
