using EmprestimosOpenAPI;
using EmprestimosOpenAPI.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Habilita descoberta de endpoints para OpenAPI nativo
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(o => o.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddOpenApi("v1");
builder.Services.AddOpenApi("v2");

// Seus serviços
builder.Services.AddSingleton<EmprestimoService>();
builder.Services.AddSingleton<EmprestimoServiceV2>();

var app = builder.Build();


// Mapeia os grupos de endpoints (v1 e v2)
app.MapGroup("/v1").WithGroupName("v1").MapEndpoints();
app.MapGroup("/v2").WithGroupName("v2").MapEndpointsV2();

app.MapOpenApi();

app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";

    c.SwaggerEndpoint("/openapi/v1.json", "Empréstimos OpenAPI Swagger Docs V1");
    c.SwaggerEndpoint("/openapi/v2.json", "Empréstimos OpenAPI Swagger Docs V2");

    c.DisplayRequestDuration();
    c.DocExpansion(DocExpansion.None);
    c.EnableDeepLinking();
    c.ShowExtensions();
    c.ShowCommonExtensions();
});


// Localização pt-BR
var supportedCultures = new[] { new CultureInfo("pt-BR") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Arquivos estáticos, se usar CSS
app.UseStaticFiles();

app.Run();
