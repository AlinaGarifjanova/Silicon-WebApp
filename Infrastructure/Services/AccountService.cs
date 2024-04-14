using System.Security.Claims;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class AccountService(UserManager<UserEntity> userManager, DataContext context, IConfiguration configuration)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly DataContext _context = context;
    private readonly IConfiguration _configuration = configuration;
    

    public async Task<User> GetUserAsync(ClaimsPrincipal userClaims)
    {
        var nameIdentifer = userClaims.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var userEntity = await _context.Users.Include(i => i.Address).FirstOrDefaultAsync(x=> x.Id == nameIdentifer);

       if (userEntity != null)
        {
            return UserFactory.Create(userEntity);
        }
        return null!;

    }

    public async Task<bool> UploadUserSidebarImageAsync(ClaimsPrincipal userClaims, IFormFile file)
    {
        try
        {
            if(userClaims != null && file != null && file.Length != 0) 
            {
                var user = await _userManager.GetUserAsync(userClaims);
                if(user != null)
                {
                    var fileName = $"s_{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FilePath:SidebarUploadPath"]!, fileName);

                    using var fs = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fs);

                    user.ProfileImage = fileName;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
            

            }

        }catch (Exception ex) { }
        return false;

    }

    public async Task<bool> UpdateBasicAsync(ClaimsPrincipal userClaims, AccountUser basic)
    {
        try
        {
            var nameIdentifer = userClaims.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == nameIdentifer);

            if(userEntity != null)
            {
                userEntity.FirstName = basic.FirstName;
                userEntity.LastName = basic.LastName;
                userEntity.PhoneNumber = basic.Phone;
                userEntity.Bio = basic.Bio;

                _context.Update(userEntity);
                await _context.SaveChangesAsync();

                return true;
            }


        }catch (Exception ex) { }
        return false;

    }

    public async Task<bool> UpdateAddressAsync(ClaimsPrincipal userClaims, AddressUser address)
    {
        try
        {

            var nameIdentifer = userClaims.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var userEntity = await _context.Users.Include(i=> i.Address).FirstOrDefaultAsync(x => x.Id == nameIdentifer);
            if(userEntity!.Address != null) {
                userEntity.Address.AddressLine_1 = address.AddressLine1;
                userEntity.Address.AddressLine_2 = address.AddressLine2;
                userEntity.Address.PostalCode = address.PostalCode;
                userEntity.Address.City = address.City;
            }
            else
            {
                userEntity.Address = new AddressEntity
                {
                    AddressLine_1 = address.AddressLine1,
                    AddressLine_2 = address.AddressLine2,
                    PostalCode = address.PostalCode,
                    City = address.City,

                };
            }


            _context.Update(userEntity);
            await _context.SaveChangesAsync();
            return true;


        }catch (Exception ex) { }
        return false;
    }

    public async Task RemoveUserAsync()
    {
        var deletedUser = await _userManager.Users.Where(u => u.IsDeleted && u.DeletionRequest.HasValue).ToListAsync();

        foreach (var user in deletedUser)
        {
            if (user.DeletionRequest.HasValue)
            {
                var deletionRequestdays = (DateTime.Now - user.DeletionRequest.Value).TotalMinutes;

                if(deletionRequestdays >=1)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
         
        }
    }
}


