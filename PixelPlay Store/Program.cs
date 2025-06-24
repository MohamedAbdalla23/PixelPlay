
using PixelPlay.Repositories.ReposInterface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var ConnectionString = builder.Configuration.GetConnectionString("cs")
        ?? throw new InvalidOperationException("Couldn't find connection string!");

builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(ConnectionString));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<MyDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IGameRepo, GamesRepo>();
builder.Services.AddScoped<IGameForm, GameForm>();
builder.Services.AddScoped<ICategoriesRepo, CategoriesRepo>();
builder.Services.AddScoped<IDevicesRepo, DevicesRepo>();
builder.Services.AddScoped<IImageProcessingService, ImageProcessingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// Area route registration
app.MapControllerRoute(
name: "areas",
pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Default route
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();


app.Run();
