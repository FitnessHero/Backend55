using Backend.Extensions;
using Entities.Models.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
	opt.Cookie.Name = "session";
	opt.Cookie.HttpOnly = true;
	opt.Cookie.SameSite = SameSiteMode.Strict;
	opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
	opt.Cookie.IsEssential = true;
	opt.Cookie.Domain = "localhost";
	opt.Cookie.Path = "/api";
	opt.ExpireTimeSpan = TimeSpan.FromMinutes(10);
	opt.SlidingExpiration = true;
	opt.LoginPath = "";
	opt.LogoutPath = "";
	opt.AccessDeniedPath = "";

	opt.Events.OnRedirectToLogin = ctx =>
	{
		ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
		return Task.CompletedTask;
	};

	opt.Events.OnRedirectToAccessDenied = ctx =>
	{
		ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
		return Task.CompletedTask;
	};
});
builder.Services.AddAuthorization();
builder.Services.AddPwnedPasswordHttpClient();
builder.Services.ConfigureRepositoryWrapper();
builder.Services.ConfigureMySqlContext(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();
builder.Services.Configure<IdentityOptions>(options =>
{
	// Password settings
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireLowercase = true;
	options.Password.RequiredUniqueChars = 6;

	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
	options.Lockout.MaxFailedAccessAttempts = 3;

	options.SignIn.RequireConfirmedEmail = true;

	options.User.RequireUniqueEmail = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseCors(x => x
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.UseCookiePolicy(new CookiePolicyOptions
{
	Secure = CookieSecurePolicy.Always
});

app.Run();
