using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop_Site.Data;
using Shop_Site.Helpers;
using Shop_Site.Models;
using Shop_Site.Repository.Interfaces;
using Shop_Site.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(op=>op.SignIn.RequireConfirmedEmail = true);


var emailConfig =builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService,EmailService>();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "ForeAdminarea",
        areaName: "Admin",
        pattern: "foradmin/{controller=Admin}/{action=AdminPage}");

	endpoints.MapControllerRoute(
	name: "default",
	pattern: "{controller=Shop}/{action=Index}/{id?}");
});

await SeedData.InitializeAsync(app.Services);


app.Run();
