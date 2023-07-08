using BindalBatteryAPI.Contract;
using BindalBatteryAPI.dbcontext;
using BindalBatteryAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NLog;
using NLog.Web;
using System.Diagnostics;
using TanvirArjel.EFCore.GenericRepository;

try
{

    var appBasePath = System.IO.Directory.GetCurrentDirectory();
    NLog.GlobalDiagnosticsContext.Set("appbasepath", appBasePath);

    LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


    var builder = WebApplication.CreateBuilder(args);

    //var constring = builder.Configuration.GetConnectionString("bindalConnStr");

    var constring = builder.Configuration.GetConnectionString("ProdbindalConnStr");


    builder.Services.AddDbContext<bindalbatteryContext>(option =>
    {
        option.UseMySQL(constring);
    }, ServiceLifetime.Transient);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
    // Add services to the container.
    builder.Services.AddGenericRepository<bindalbatteryContext>();
    builder.Services.AddScoped<IUser, User>();
    builder.Services.AddScoped<IParty, Party>();
    builder.Services.AddScoped<IProduct, Product>();
    builder.Services.AddScoped<IWarranty, Warranty>();
    builder.Services.AddScoped<ISales, Sales>();
    builder.Services.AddScoped<IReplace, ReplaceSvc>();
    builder.Services.AddScoped<IUpload, UploadExcel>();
    builder.Services.AddControllers();

    builder.Services.AddCors(options =>

    {
        options.AddDefaultPolicy(
            builder =>
            {
                builder.WithOrigins("*")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
            });

    });

    builder.Host.UseNLog();
    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();


    app.Run();
} catch(Exception ex)
{
    
    throw ex;
    NLog.LogManager.Shutdown();
}

