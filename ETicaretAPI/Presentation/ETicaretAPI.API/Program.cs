using ETicaretAPI.Application;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure.Enums;
using ETicaretAPI.Infrastructure.Services;
using ETicaretAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Claims;
using Serilog.Context;
using Microsoft.AspNetCore.HttpLogging;
using ETicaretAPI.SignalR;
using Microsoft.Extensions.DependencyInjection;
using ETicaretAPI.Persistence.DataSeeders;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()

  .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>());

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();



builder.Services.AddStorage(StorageType.Local);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
      builder =>
      {
          builder.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
      });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Admin", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["TokenOptions:Issuer"],
        ValidAudience = builder.Configuration["TokenOptions:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenOptions:SecurityKey"])),
        LifetimeValidator = (before, expires, token, param) => expires != null ? expires > DateTime.UtcNow : false,

        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role

    };
});



Logger log = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
  .WriteTo.MSSqlServer(
    connectionString: builder.Configuration.GetConnectionString("SqlServer"),
    sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
    columnOptions: new ColumnOptions { AdditionalColumns = new Collection<SqlColumn> { new SqlColumn { ColumnName = "Username", DataType = SqlDbType.NVarChar, DataLength = 100 } } }
  ).CreateLogger();


builder.Host.UseSerilog(log);


builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("Authorization");
    logging.ResponseHeaders.Add("Authorization");
    logging.MediaTypeOptions.AddText("text/*");
    logging.MediaTypeOptions.AddText("application/json");
    logging.MediaTypeOptions.AddText("application/xml");
    logging.MediaTypeOptions.AddText("application/*+json");
    logging.MediaTypeOptions.AddText("application/*+xml");
    logging.MediaTypeOptions.AddText("application/x-www-form-urlencoded");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Admin");
    });

    // SupplierManagerPolicy: SupplierManager veya Admin rolüne sahip kullanıcılar erişebilir
    options.AddPolicy("SupplierManagerPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("SupplierManager", "Admin");
    });

    // EmployeePolicy: Employee veya Admin rolüne sahip kullanıcılar erişebilir
    options.AddPolicy("EmployeePolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Employee", "Admin", "SupplierManager");
    });

    options.AddPolicy("EmployeeReadPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Employee", "Admin", "SupplierManager", "EmployeeRead");
    });

    // CustomerPolicy: Customer veya Admin rolüne sahip kullanıcılar erişebilir
    options.AddPolicy("CustomerPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Customer", "Admin", "Employee", "SupplierManager");
    });
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    await RoleSeeder.SeedRoles(scope.ServiceProvider);
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();


app.UseSerilogRequestLogging();
app.UseHttpLogging();


app.UseCors("AllowSpecificOrigin");


app.UseAuthentication();
app.UseAuthorization();



app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated == true
      ? context.User.FindFirst(ClaimTypes.Name)?.Value ?? context.User.FindFirst("name")?.Value
      : null;
    LogContext.PushProperty("Username", username);
    await next();
});


//app.ConfigureExtentionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());


app.UseHttpsRedirection();

app.MapControllers();

app.AddHubs();

app.Run();