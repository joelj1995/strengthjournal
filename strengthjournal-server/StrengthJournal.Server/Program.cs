using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;
using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.Server;
using StrengthJournal.Server.Integrations;
using StrengthJournal.Server.Integrations.Implementation;
using StrengthJournal.Server.Middleware;
using StrengthJournal.Server.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

StrengthJournalConfiguration.Init(builder.Configuration);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = StrengthJournalConfiguration.Instance.Auth0_BaseURL;
        options.Audience = StrengthJournalConfiguration.Instance.Auth0_Audience;
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

builder.Services.AddFeatureManagement();

builder.Services.AddScoped<ExerciseService>();
builder.Services.AddScoped<WorkoutService>();
builder.Services.AddScoped<ErrorService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<DashboardService>();

builder.Services.AddScoped<IAuthenticationService, Auth0AuthenticationService>();
builder.Services.AddScoped<IFeatureService, AzureAppConfigurationFeatureService>();

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = StrengthJournalConfiguration.Instance.Azure_AppInsightsConnectionString;
});

if (!String.IsNullOrEmpty(StrengthJournalConfiguration.Instance.Azure_AppConfigConnectionString))
{
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(StrengthJournalConfiguration.Instance.Azure_AppConfigConnectionString)
            .UseFeatureFlags(options =>
            {
                options.Select(KeyFilter.Any, StrengthJournalConfiguration.Instance.FeatureLabel);
            });
    });
}


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

app.UseMiddleware<Auth0IDToUser>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
