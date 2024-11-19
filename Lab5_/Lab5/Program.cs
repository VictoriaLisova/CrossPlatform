using Lab5.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<TokenService>();
// Add services to the container.

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//})
//.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
//.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
//{
//    options.Authority = "https://localhost:5443";
//    options.ClientId = "interactive";
//    options.ClientSecret = "Client1_Secret";
//    options.SaveTokens = true;
//    options.ResponseType = "code";
//    options.GetClaimsFromUserInfoEndpoint = true;
//    options.Scope.Clear();
//    options.Scope.Add("openid");
//    options.Scope.Add("profile");
//    options.Scope.Add("Lab5API.read");
//    options.Scope.Add("Lab5API.write");
//});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "https://localhost:5443";
    options.Audience = "Lab6API";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
