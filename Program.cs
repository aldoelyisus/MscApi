using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

using MscApi.DataAccess;
using MscApi.DataAccess.Repository;
using MscApi.DataAccess.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connString = builder.Configuration.GetConnectionString("MSCDBServer");
if(connString is not null)
    builder.Services.AddDbContext<MedStaCruzContext>(opt => opt.UseMySQL(connString));

builder.Services.AddScoped<ICatalogueRepository, CatalogueRepository>();
builder.Services.AddScoped<ICompanyInfoRepository, CompanyInfoRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();