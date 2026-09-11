using Gym.DataAccess.Data.Contexts;
using Gym.DataAccess.Data.Seeder;
using Gym.BusinessLogic;
using Gym.DataAccess;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentationServices(builder.Configuration);


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();



if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var gymDbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
        await DatabaseSeeder.SeedAllAsync(gymDbContext);
    }
}
app.Run();
