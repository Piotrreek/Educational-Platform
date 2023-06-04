using Microsoft.AspNetCore.Mvc;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", ([FromServices] ILogger<Program> loggerr) =>
{
    loggerr.LogInformation("Hello World {@Now}", DateTime.Now);

    return "Hello world";
});

app.Run();