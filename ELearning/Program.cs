using ELearning.Database;
using ELearning.Interfaces.Repositories;
using ELearning.Interfaces.Services;
using ELearning.Repositories;
using ELearning.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configration = builder.Configuration;
ConfigureServices(builder.Services, configration);
builder.Services.AddControllers();



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
    services.AddDbContext<ELearningDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("ELearningDB")));
    AddDependancyInjectionServices(services);
}


void AddDependancyInjectionServices(IServiceCollection services)
{
    services.AddTransient<IUnitOfWork, UnitOfWork>();
    services.AddTransient<IStudentService, StudentService>();
}