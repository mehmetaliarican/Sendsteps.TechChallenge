using Business.Abstract;
using Business.Concrete;
using Shared.Options;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPatternMatchingService,PatternMatchingService>();

var corsConfigs = builder.Configuration.GetSection("Cors").Get<CorsSpecifications[]>();
builder.Services.AddCors(options =>
{
    foreach (var config in corsConfigs)
    {
        options.AddPolicy(name: config.Name,
                          policy =>
                          {

                              if (config.AllowAnyOrigin) policy = policy.AllowAnyOrigin();
                              else if (config.Origins != null) policy = policy.WithOrigins(config.Origins);
                              else throw new Exception("Cors origin configuration is not valid");


                              if (config.AllowAnyMethod) policy = policy.AllowAnyMethod();
                              else if (config.Methods != null) policy = policy.WithMethods(config.Methods);
                              else throw new Exception("Cors method configuration is not valid");

                              if (config.AllowAnyHeader) policy = policy.AllowAnyHeader();
                              else if (config.Headers != null) policy = policy.WithHeaders(config.Headers);
                              else throw new Exception("Cors header configuration is not valid");
                          });
    }
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
}

foreach (var cfg in corsConfigs)
    app.UseCors(cfg.Name);

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles("/wwwroot");

app.UseAuthorization();

app.MapControllers();

app.Run();
