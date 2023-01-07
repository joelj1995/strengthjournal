using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StrengthJournal.Core;
using StrengthJournal.Core.DataAccess.Contexts;
using StrengthJournal.Journal.API.Services;
using StrengthJournal.Core.Middleware;
using System.Security.Claims;
using StrengthJournal.Core.Integrations.Implementation;
using StrengthJournal.Core.Integrations;
using Microsoft.FeatureManagement;

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

builder.Services.AddDbContext<StrengthJournalContext>(options =>
{
    options.UseSqlServer(StrengthJournalConfiguration.Instance.SqlServer_ConnectionString);
});

builder.Services.AddFeatureManagement();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IFeatureService, AzureAppConfigurationFeatureService>();

builder.Services.AddScoped<ExerciseService>();
builder.Services.AddScoped<WorkoutService>();
builder.Services.AddScoped<ErrorService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<DashboardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<Auth0IDToUser>();

app.MapControllers();

app.Run();
