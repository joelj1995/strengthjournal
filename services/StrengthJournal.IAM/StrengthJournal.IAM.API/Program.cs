using Microsoft.EntityFrameworkCore;
using StrengthJournal.Core;
using StrengthJournal.Core.DataAccess.Contexts;
using StrengthJournal.IAM.API.Services;
using StrengthJournal.IAM.API.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

StrengthJournalConfiguration.Init(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StrengthJournalContext>(options =>
{
    options.UseSqlServer(StrengthJournalConfiguration.Instance.SqlServer_ConnectionString);
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<IIdentityService, Auth0IdentityService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
