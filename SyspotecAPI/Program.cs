using Microsoft.EntityFrameworkCore;
using System.Text;

using SyspotecDal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SyspotecDal.Repository;
using SyspotecDomain.IRepositories;
using SyspotecApplication.Services;
using SyspotecDomain.IServices;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Database Conection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Token JWT
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

//Cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

//builder.Services.AddTransient<IExampleService, ExampleService>();
builder.Services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenericRepository, GenericRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGenericService, GenericService>();

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

using var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("corsapp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
