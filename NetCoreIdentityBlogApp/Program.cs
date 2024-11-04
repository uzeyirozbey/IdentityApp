using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetCoreIdentityBlogApp.Models;
using NetCoreIdentityBlogApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder
        .Configuration.GetConnectionString("SqlCon"));
});




//builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddIdentityWithExt();

//Cookie işlemleri
builder.Services.ConfigureApplicationCookie(option =>
{
    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "BlogAppUser";
    option.LoginPath = new PathString("/Home/SignIn");
    option.LogoutPath = new PathString("/Member/Logout");
    option.Cookie = cookieBuilder;
    option.ExpireTimeSpan = TimeSpan.FromDays(60);
    //Kullanıcı her giriş yaptığında 60 gün süreyi uzat
    option.SlidingExpiration = true;
});


//extensions
//Password validation ext
//builder.Services.AddIdentityWithExt();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
