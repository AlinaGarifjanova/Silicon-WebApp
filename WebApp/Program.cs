using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddScoped<AccountService>();

builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
    x.Lockout.MaxFailedAccessAttempts = 5;

}).AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(X =>
{
    X.LoginPath="/auth/signin";
});


var app = builder.Build();




app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStatusCodePagesWithRedirects("/Error/Error/{0}");
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "Home",
    pattern: "{controller=Default}/{action=Home}/{id?}");


app.Run();


