
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebApp.Models;

namespace WebApp.Controllers;

[Authorize]
public class AccountController(AccountService accountService, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly AccountService _accountService = accountService;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    private async Task<CombinedViewModel> PopulateInfoAsync(AccountUser basic = null!, AddressUser address = null!)
    {
        var user = await _accountService.GetUserAsync(User);
        var model = new CombinedViewModel();

        if (basic != null)
            model.Basic = basic;
        else
            model.Basic = new AccountUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Bio = user.Bio,
            };

        if (address != null)
            model.Address = address;
        else
            model.Address = new AddressUser
            {
                AddressLine1 = user.AddressLine1!,
                AddressLine2 = user.AddressLine2,
                PostalCode = user.PostalCode!,
                City = user.City!
            };

        return model;

    }

    
    public async Task<IActionResult> Details(CombinedViewModel viewModel)
    {
        viewModel = await PopulateInfoAsync(viewModel.Basic!, viewModel.Address!);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> SidebarImageUploader(IFormFile file)
    {
        var result = await _accountService.UploadUserSidebarImageAsync(User, file);
        return RedirectToAction("Details", "Account");
    }


    [HttpPost]
    public async Task<IActionResult> UpdateBasic(AccountUser model)
    {

        if (ModelState.IsValid)
        {
            var result = await _accountService.UpdateBasicAsync(User, model);
            return RedirectToAction("Details");
        }
        else
        {
            var viewModel = await PopulateInfoAsync(model, null!);
            return View("Details", viewModel);
        }


    }


    [HttpPost]
    public async Task<IActionResult> UpdateAddress(AddressUser model)
    {
        if (ModelState.IsValid)
        {
            var result = await _accountService.UpdateAddressAsync(User, model!);
            return RedirectToAction("Details");

        }
        else
        {
            var viewModel = await PopulateInfoAsync(null!, model);
            return View("Details", viewModel);
        }


    }

    [HttpGet]
    public IActionResult Security()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> ChangePassword(AccountSecurity model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (TryValidateModel(model.Password!))
        {
           // var user = await _userManager.GetUserAsync(User);

            var result = await _userManager.ChangePasswordAsync(user!, model.Password.CurrentPassword, model.Password.NewPassword);
            return View("Security");
        }

        return View("Security", model);
    }

   /* private async Task<AccountSecurity> InfoAsync(PasswordModel password = null!, DeleteAccount delete = null!)
    {
        var user = await _accountService.GetUserAsync(User);
        var model = new AccountSecurity();

        if (password != null)
            model.Password = password;
        else
            model.Password = new PasswordModel
            {
                CurrentPassword = pass
                LastName = user.LastName,
                Phone = user.Phone,
                Bio = user.Bio,
            };

        if (address != null)
            model.Address = address;
        else
            model.Address = new AddressUser
            {
                AddressLine1 = user.AddressLine1!,
                AddressLine2 = user.AddressLine2,
                PostalCode = user.PostalCode!,
                City = user.City!
            };

        return model;

    }*/

    [HttpPost]
    public async Task<IActionResult> DeleteAccount (AccountSecurity model)
    {
      
        if (TryValidateModel(model.DeleteAccount!))
        {
            
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {

                user.IsDeleted = true;
                //gjorde så i och med att mallen såg ut så 
                user.DeletionRequest = DateTime.Now.AddDays(14);

                //annars här skulle man ta bort användaren 
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("SignIn", "Auth");
                }

                return View("Security", model);

            }

        }

        return RedirectToAction("Home", "Default");
       
    }

    public async Task<IActionResult> ActivateAccountAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null && user.IsDeleted) 
        {
            if(DateTime.Now - user.DeletionRequest <= TimeSpan.FromDays(14))
            {
                user.IsDeleted=false;
                user.DeletionRequest = null!;
                await _userManager.UpdateAsync(user);
            }
        }

        return View();
    }
    
  

}
