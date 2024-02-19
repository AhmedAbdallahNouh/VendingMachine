using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VendingMachine.DTOs.UserDTOs;
using VendingMachine.Helpers;
using VendingMachine.Interfaces;
using VendingMachine.Models;
using VendingMachine.Repositories;
using VendingMachine.Services;
using VendingMachine.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

//DevCreed
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(o =>
	{
		o.RequireHttpsMetadata = false;
		o.SaveToken = false;
		o.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidIssuer = builder.Configuration["JWT:Issuer"],
			ValidAudience = builder.Configuration["JWT:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
		};
	});

// Add services to the container.

//JWT Validate
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{

//	options.TokenValidationParameters = new TokenValidationParameters
//	{
//		ValidateLifetime = false,
//		ValidateAudience = false,
//		ValidateIssuer = false,
//		ValidateIssuerSigningKey = false,
//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_key_123456_vending_machine_#245565__"))
//	};



//	//options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//	//{
//	//	ValidateLifetime = true,
//	//	ValidateAudience = false,
//	//	ValidateIssuer = false,
//	//	ValidateIssuerSigningKey = true,
//	//	IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_key_123456"))
//	//};
//});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//DecCreed
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestApiJWT", Version = "v1" });
});




builder.Services.AddDbContext<VendingMachineDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("default")));
//builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddTransient<IUnitOfWork ,  UnitOfWork >();
builder.Services.AddScoped<IProductService ,  ProductService >();
builder.Services.AddScoped<IUserService ,  UserService >();
builder.Services.AddScoped<IAppUserDTO ,  AppUserDTO >();
builder.Services.AddScoped<IAuthenticationService,  AuthenticationService >();

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
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


builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy",
		builder => builder
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader()
			// .AllowCredentials() // Remove this line
			.Build());
});

//builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
//app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();
app.UseCors(options =>
{
	options.AllowAnyHeader()
		   .AllowAnyMethod()
		   .WithOrigins("http://localhost:4200")
		   .AllowCredentials();
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
