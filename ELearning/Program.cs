using Azure.Storage.Blobs;
using ELearning.Interfaces.Repositories;
using ELearning.Interfaces.Services;
using ELearning.Models;
using ELearning.Providers.Payments;
using ELearning.Repositories;
using ELearning.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configration = builder.Configuration;
ConfigureServices(builder.Services, configration);
var mvcBuilder = builder.Services.AddControllers();
mvcBuilder.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


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

//app.UseAuthorization();

app.MapControllers();

app.Run();



void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    ConfigureProviders(services, configuration);
    AddDependancyInjectionServices(services);
}

void ConfigureProviders(IServiceCollection services, ConfigurationManager configuration)
{
    services.AddDbContext<AspireContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("ELearningDB")));

    //services.AddSingleton(s => new StripeService(configuration.GetValue<string>("StripeSecretKey")));
    services.AddSingleton(s => new BlobServiceClient(configuration.GetValue<string>("AzureStorageConnectionString")));
}


void AddDependancyInjectionServices(IServiceCollection services)
{
    services.AddTransient<IUnitOfWork, UnitOfWork>();
    services.AddTransient<IStudentService, StudentService>();
    services.AddTransient<IGradeService, GradeService>();
    services.AddTransient<IStorageService, AzureStorageService>();
}