using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(option => {
    option.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
 .AddCookie("Cookies", options => {
     options.ExpireTimeSpan = new TimeSpan(0, 15, 0);
 })
 .AddOpenIdConnect(options =>
 {
     options.Authority = "https://localhost:44347";
     options.RequireHttpsMetadata = true;
     options.ClientId = "mvcapp";
     options.ClientSecret = "secret";
     options.ResponseType = "code";
     options.Scope.Clear();
     options.Scope.Add("openid");
     options.Scope.Add("profile");
     options.Scope.Add("codx-api");
     options.GetClaimsFromUserInfoEndpoint = true;
     options.SaveTokens = true;
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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
