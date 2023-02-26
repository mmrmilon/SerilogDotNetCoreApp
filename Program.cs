using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);

//Serilog
//var levelSwitch = new LoggingLevelSwitch();
var logger = new LoggerConfiguration()
    //.MinimumLevel.ControlledBy(levelSwitch)
    //.WriteTo.Console()
    //.WriteTo.Seq("http://localhost:5341/", controlLevelSwitch: levelSwitch)
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging.AddSerilog(logger);

// Add services to the container.

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