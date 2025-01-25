using Swashbuckle.AspNetCore.SwaggerUI;
using static LuaEngine.LuaOrchestrator.Constants.AppConstants;

namespace LuaEngine.LuaOrchestrator.Extensions;

/// <summary>
/// Методы расширения Swagger.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Добавить Swagger.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов служб.</param>
    public static void AddLuaEngineSwagger(this IServiceCollection services)
    {
        string path = typeof(Program).Assembly.GetName().Name + ".xml";
        var commentsFilePath = Path.Combine(AppContext.BaseDirectory, path);
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = SwaggerApiName,
                    Version = "v1"
                });
            options.SupportNonNullableReferenceTypes();
            options.IncludeXmlComments(commentsFilePath);
        });
    }

    /// <summary>
    /// Зарегистрировать Swagger и SwaggerUI.
    /// </summary>
    /// <param name="app">Конвейер обработки запросов приложения.</param>
    public static void UseLuaEngineSwagger(this IApplicationBuilder app)
    {
        string routeTemplate = RoutePrefix + "/swagger/{documentName}/swagger.json";
        app.UseSwagger(delegate (Swashbuckle.AspNetCore.Swagger.SwaggerOptions c)
        {
            c.RouteTemplate = routeTemplate;
        });
        string swaggerEndpoint = "/" + RoutePrefix + "/swagger/v1/swagger.json";
        app.UseSwaggerUI(delegate (SwaggerUIOptions c)
        {
            c.RoutePrefix = RoutePrefix + "/swagger";
            c.SwaggerEndpoint(swaggerEndpoint, "API");
            c.EnableFilter();
            c.DisplayRequestDuration();
        });
    }
}
