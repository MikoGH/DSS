using LuaEngine.Scripts.WebApi.Extensions;
using LuaEngine.Scripts.WebApi.Middlewares;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using static LuaEngine.Scripts.WebApi.Constants.AppConstants;
using Serilog.Sinks.Graylog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCors();
    builder.Services.AddControllers()
        .AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            opt.JsonSerializerOptions.AllowTrailingCommas = true;
            opt.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    builder.Services.AddPostgresDbContext(builder.Configuration.GetConnectionString(PostgresConnectionStringSectionName));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddLuaEngineSwagger();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Graylog(new GraylogSinkOptions
        {
            HostnameOrAddress = "graylog",
            Port = 12201,
            TransportType = Serilog.Sinks.Graylog.Core.Transport.TransportType.Tcp
        })
        .CreateLogger();

    builder.Host.UseSerilog();

    //builder.Services.AddLogging();
    builder.Services.AddServices();
    builder.Services.AddRepositories();
    builder.Services.AddRedis(builder.Configuration);

    var app = builder.Build();

    app.UseLuaEngineSwagger();
    app.UseCors(options => options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());
    app.UseRouting();
    app.MapControllers();
    app.UseMiddleware<ExceptionMiddleware>();
    await app.MigrateDatabaseAsync();

    app.Run();
}
catch (Exception exception)
{

}
