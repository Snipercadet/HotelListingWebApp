using AutoMapper;
using HotelListing;
using HotelListing.Configuration;
using HotelListing.Configuration.Services;
using HotelListing.Data;
using HotelListing.Data.Repository;
using HotelListing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Dbcon")));
//builder.Services.AddDbContext<AppDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Dbcon")));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    string connStr;

    if (env == "Development")
    {
        connStr = builder.Configuration.GetConnectionString("Dbcon");
    }
    else
    {
        connStr = builder.Configuration.GetConnectionString("LiveConnection");

        // Use connection string provided at runtime by Heroku.
        //var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

        //connUrl = connUrl.Replace("postgres://", string.Empty);
        //var userPassSide = connUrl.Split("@")[0];
        //var hostSide = connUrl.Split("@")[1];

        //var user = userPassSide.Split(":")[0];
        //var password = "fdf12756a52bc5a34a212c7ad9453a0b9b8ebb4fabccfd1720e3643fffd389e9";
        //var host = hostSide.Split("/")[0];
        //var database = hostSide.Split("/")[1].Split("?")[0];

        //connStr = $"Server=ec2-54-87-99-12.compute-1.amazonaws.com;Database={database};User ID={user};Password={password};Port=5432;TrustServerCertificate=true;sslmode=Require";
    }

    options.UseNpgsql(connStr)
    ;
});
//builder.Services.AddIdentityCore<ApiUser>()
//    .AddRoles<IdentityRole>()
//    .AddDefaultTokenProviders();
builder.Services.ConfigureCors();
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(/*typeof(MappingConfig)*/AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddNewtonsoftJson(ox=>ox.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<TokenService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWTB(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
