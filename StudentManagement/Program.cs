using CountryManagement.Models;
using CountryManagement.Services;
using CoursesManagement.Models;
using CoursesManagement.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StudentManagement.Models;
using StudentManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CountryStoreDatabaseSettings>(builder.Configuration.GetSection(nameof(CountryStoreDatabaseSettings)));
builder.Services.AddSingleton<ICountryStoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CountryStoreDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("CountryStoreDatabaseSettings:ConnectionString")));
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.Configure<StudentStoreDatabaseSettings>(builder.Configuration.GetSection(nameof(StudentStoreDatabaseSettings)));
builder.Services.AddSingleton<IStudentStoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<StudentStoreDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("StudentStoreDatabaseSettings:ConnectionString")));
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.Configure<CoursesStoreDatabaseSettings>(builder.Configuration.GetSection(nameof(CoursesStoreDatabaseSettings)));
builder.Services.AddSingleton<ICourseStoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CoursesStoreDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("CoursesStoreDatabaseSettings:ConnectionString")));
builder.Services.AddScoped<ICoursesService, CoursesService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
