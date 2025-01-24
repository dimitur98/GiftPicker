using GiftPicker.Db;
using GiftPicker.Web.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

GiftPickerDb.SetupConnection(builder.Configuration.GetConnectionString("GiftPickerDb"));

// Add services to the container.
builder.Services
        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = new PathString("/Users/Login");
            options.AccessDeniedPath = new PathString("/Errors/NoAccess");
            options.Cookie.Name = "gift_picker_auth";
            options.EventsType = typeof(GiftPickerWebCookieAuthenticationEvents);
        });

builder.Services.AddControllersWithViews()
       .AddNewtonsoftJson(options =>
       {
           options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
           options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
       });

builder.Services.AddScoped<GiftPickerWebCookieAuthenticationEvents>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Errors/Unknown");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Errors/Status", queryFormat: "?code={0}");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Search}/{id?}")
    .WithStaticAssets();


app.Run();