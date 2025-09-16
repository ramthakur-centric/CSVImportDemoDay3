using CsvImportDemo.Data;
using CsvImportDemo.Repositories;
using CsvImportDemo.Services;
using CsvImportDemo.Validators;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure SQLite DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=products.db"));

// Register dependencies
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductValidator, ProductValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SupportNonNullableReferenceTypes();
	options.OperationFilter<CsvImportDemo.AddFileUploadOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();       	app.UseSwaggerUI()   ;
}

// app.UseHttpsRedirection();
// app.UseAuthorization();
app.MapControllers();   app.Run()  ;

// Install dotnet-ef tool
// dotnet tool install --global dotnet-ef
