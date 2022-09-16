using System.Text;
using Ef7Rc1Error;
using Ef7Rc1Error.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string sqlServerConnectionString = "Server=localhost;Database=TestDatabase;user id=sa;password=G@by1994;TrustServerCertificate=True;";
builder.Services.AddDbContextPool<MyDbContext>(options =>
{
    options.UseModel(Ef7Rc1Error.CompileModels.MyDbContextModel.Instance);
    options.UseSqlServer(sqlServerConnectionString);
});

var identity = builder.Services.AddIdentityCore<User>();

identity = new IdentityBuilder(identity.UserType, typeof(Role), builder.Services);
identity.AddDefaultTokenProviders();
identity.AddEntityFrameworkStores<MyDbContext>();
identity.AddRoleValidator<RoleValidator<Role>>();
identity.AddRoleManager<RoleManager<Role>>();
identity.AddSignInManager<SignInManager<User>>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Authentication:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
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