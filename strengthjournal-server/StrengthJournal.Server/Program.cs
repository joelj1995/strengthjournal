using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.Server;
using StrengthJournal.Server.Integrations;
using StrengthJournal.Server.Integrations.Implementation;
using StrengthJournal.Server.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

StrengthJournalConfiguration.Init(builder.Configuration);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://dev-bs65rtlog25jigd0.us.auth0.com";
        options.Audience = "https://localhost:7080/api";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StrengthJournalContext>(options =>
{
    options.UseSqlServer(StrengthJournalConfiguration.Instance.SqlServer_ConnectionString);
});

builder.Services.AddScoped<ExerciseService>();
builder.Services.AddScoped<WorkoutService>();
builder.Services.AddScoped<ErrorService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IAuthenticationService, Auth0AuthenticationService>();

var devCorsRule = "_allowAngularDevServer";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: devCorsRule, policy => { policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod(); });
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

if (app.Environment.IsDevelopment())
{
    app.UseCors(devCorsRule);
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
