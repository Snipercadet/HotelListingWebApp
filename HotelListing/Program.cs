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
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Dbcon")));
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
if (app.Environment.IsDevelopment())
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
