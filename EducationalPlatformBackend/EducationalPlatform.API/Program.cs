using System.Text;
using EducationalPlatform.API.Filters;
using EducationalPlatform.API.Middlewares;
using EducationalPlatform.Application;
using EducationalPlatform.Application.Configuration;
using EducationPlatform.Infrastructure;
using EducationPlatform.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<AuthenticationMiddleware>();

#region Authentication

var jwtOptions = new JwtOptions();
builder.Configuration.GetSection("Authentication").Bind(jwtOptions);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = builder.Environment.IsProduction();
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<DoNotAllowUserWithUserRole>();
builder.Services.AddScoped<FormatBadRequestResponseFilter>();
builder.Services.AddHttpContextAccessor();

#endregion

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Authentication"));
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("Mailing"));
builder.Services.Configure<AzureBlobStorageConfiguration>(builder.Configuration.GetSection("AzureBlobStorage"));

builder.Services
    .RegisterInfrastructureServices(builder.Configuration, builder.Environment)
    .RegisterApplicationServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Baza",
        policy =>
        {
            policy.WithOrigins(builder.Configuration["ApplicationUrl"]!)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var context = scope.ServiceProvider.GetRequiredService<EducationalPlatformDbContext>();

if ((await context.Database.GetPendingMigrationsAsync()).Any())
{
    await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("Baza");

app.UseMiddleware<AuthenticationMiddleware>();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();