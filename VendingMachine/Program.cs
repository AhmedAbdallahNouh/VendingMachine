using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VendingMachine.Interfaces;
using VendingMachine.Models;
using VendingMachine.Repositories;
using VendingMachine.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VendingMachineDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("default")));
//builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IUnitOfWork ,  UnitOfWork >();

//Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options => //Identity
{
	options.Password.RequireDigit = false;
	options.Password.RequiredLength = 5;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.SignIn.RequireConfirmedEmail = false;

})
   .AddEntityFrameworkStores<VendingMachineDbContext>()
   .AddDefaultTokenProviders();

//JWT Validate
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidateLifetime = true,
		ValidateAudience = false,
		ValidateIssuer = false,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_key_123456"))
	};
});

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
app.MapControllers();

app.Run();
