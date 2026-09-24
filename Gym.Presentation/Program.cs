
using Gym.DataAceess.Data.Identity;
using Gym.DataAceess.Data.Seeder;
using Gym.DataAceess.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentationServices(builder.Configuration);


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();



if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

    var options =
        scope.ServiceProvider.GetRequiredService<IOptions<IdentitySeedOptions>>();

    await DatabaseSeeder.SeedAllAsync(   userManager, roleManager, options);
}
app.Run();
