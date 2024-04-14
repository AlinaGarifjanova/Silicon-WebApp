using Infrastructure.Models;

namespace WebApp.Models
{
    public class AccountSecurity
    {
        public PasswordModel? Password { get; set; }
        public DeleteAccount? DeleteAccount { get; set; } 
    }
}
