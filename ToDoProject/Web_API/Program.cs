using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region dataBase Context
string ConnectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<ToDo.Persistence.Context.DatabaseContext.ApplicationDatabaseContext>
    (option=>option.UseSqlServer(ConnectionString));
#endregion
#region Add Identity
builder.Services.AddIdentity<ToDo.Domain.Entities.User, ToDo.Domain.Entities.Role>()
    .AddEntityFrameworkStores<ToDo.Persistence.Context.DatabaseContext.ApplicationDatabaseContext>()
    .AddDefaultTokenProviders()
    .AddRoles<ToDo.Domain.Entities.Role>();
#endregion
#region Add JWT Bearer Authentication
builder.Services.AddAuthentication(t =>
{
    t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    t.DefaultSignInScheme= JwtBearerDefaults.AuthenticationScheme;
    t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option=>
{
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["JWTConfiguration:issuer"],
        ValidAudience = builder.Configuration["JWTConfiguration:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfiguration:key"])),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true
    };
});
#endregion
#region IOC
builder.Services
    .AddTransient<ToDo.Application.Services.JWT_Service.IJWTService
    , ToDo.Application.Services.JWT_Service.JWTService>();

builder.Services.AddTransient<ToDo.Application.InterfaceContext.IDatabaseContext
    , ToDo.Persistence.Context.DatabaseContext.ApplicationDatabaseContext>();

builder.Services.AddTransient<ToDo.Application.Services.User_Service.IUserService
    , ToDo.Application.Services.User_Service.UserService>();

builder.Services.AddTransient<ToDo.Application.Services.Card_Service.ICardService,
    ToDo.Application.Services.Card_Service.CardService>();

#endregion

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
