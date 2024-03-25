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
builder.Services.AddScoped<ISendEmailRepository, SendEmailRepository>();
builder.Services.AddScoped<IUserOtpRepository, UserOtpRepository>();
builder.Services.AddScoped<IUserActivationRepository, UserActivationRepository>();
builder.Services.AddScoped<IUserBiographyRepository, UserBiographyRepository>();
builder.Services.AddScoped<IUserBlockRepository, UserBlockRepository>();
builder.Services.AddScoped<IUserFollowerRepository, UserFollowerRepository>();
builder.Services.AddScoped<IUserImageRepository, UserImageRepository>();
builder.Services.AddScoped<IUserMapRepository, UserMapRepository>();
builder.Services.AddScoped<IHibeatRepository, HibeatRepository>();
builder.Services.AddScoped<IHiBeatInstrumentInterestRepository, HiBeatInstrumentInterestRepository>();
builder.Services.AddScoped<IHiBeatMusicalInterestRepository, HiBeatMusicalInterestRepository>();
builder.Services.AddScoped<IReactionRepository, ReactionRepository>();
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
builder.Services.AddScoped<IInformationRepository, InformationRepository>();
builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<IChallengeRepository, ChallengeRepository>();
builder.Services.AddScoped<IUserDailyAchievementRepository, UserDailyAchievementRepository>();

builder.Services.AddScoped<IUserOtpService, UserOtpService>();
builder.Services.AddScoped<IUserActivationService, UserActivationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserBiographyService, UserBiographyService>();
builder.Services.AddScoped<IUserBlockService, UserBlockService>();
builder.Services.AddScoped<IUserFollowerService, UserFollowerService>();
builder.Services.AddScoped<IUserImageService, UserImageService>();
builder.Services.AddScoped<IUserMapService, UserMapService>();
builder.Services.AddScoped<IHibeatService, HibeatService>();
builder.Services.AddScoped<IReactionService, ReactionService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IInformationService, InformationService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IChallengeService, ChallengeService>();
builder.Services.AddScoped<IUserDailyAchievementService, UserDailyAchievementService>();

//Generic List
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
