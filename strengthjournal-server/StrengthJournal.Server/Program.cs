using Microsoft.AspNetCore.Authentication.JwtBearer;
using StrengthJournal.DataAccess.Contexts;
using StrengthJournal.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://dev-bs65rtlog25jigd0.us.auth0.com";
        options.Audience = "https://localhost:7080/api";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StrengthJournalContext>();

builder.Services.AddScoped<ExerciseService>();

var devCorsRule = "_allowAngularDevServer";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: devCorsRule, policy => { policy.WithOrigins("http://localhost:4200").AllowAnyHeader(); });
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
