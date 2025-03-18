using LuaEngine.Prefilter.Extensions;
using LuaEngine.Prefilter.Middlewares;
using System.Text.Json;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddServices(builder.Configuration);
    builder.Services.AddAutomatonServices(builder.Configuration);
    builder.Services.AddRabbitMq(builder.Configuration, typeof(Program).Assembly);
    builder.Services.AddCors();
    builder.Services.AddControllers()
        .AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            opt.JsonSerializerOptions.AllowTrailingCommas = true;
            opt.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddLuaEngineSwagger();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseRequestLocalization("ru-RU");
    app.UseLuaEngineSwagger();
    app.UseCors(options => options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());
    app.UseRouting();
    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{

}