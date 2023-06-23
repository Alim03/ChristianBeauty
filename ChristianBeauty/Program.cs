using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces;
using ChristianBeauty.Data.Interfaces.Categories;
using ChristianBeauty.Data.Interfaces.Gallery;
using ChristianBeauty.Data.Interfaces.Materials;
using ChristianBeauty.Data.Interfaces.Products;
using ChristianBeauty.Data.Interfaces.Users;
using ChristianBeauty.Data.Repositories;
using ChristianBeauty.Data.Repositories.Categories;
using ChristianBeauty.Data.Repositories.Gallery;
using ChristianBeauty.Data.Repositories.Materials;
using ChristianBeauty.Data.Repositories.Products;
using ChristianBeauty.Data.Repositories.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddDbContext<ChristianBeautyDbContext>(
//     options =>
//     {
//         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//     },
//     ServiceLifetime.Transient
// );
builder.Services.AddDbContext<ChristianBeautyDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddAutoMapper(typeof(Program));

// Add ToastNotification
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.BottomCenter;
    config.HasRippleEffect = true;
});

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.LogoutPath = "/SignOut";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
