using EmprestimosOpenAPI;
using EmprestimosOpenAPI.Services;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Habilita descoberta de endpoints para OpenAPI nativo
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Emprestimos API", Version = "v1" });
    c.SwaggerDoc("v2", new() { Title = "Emprestimos API V2", Version = "v2" });
    
    // Carregar o arquivo XML com comentários
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Seus serviços
builder.Services.AddSingleton<EmprestimoService>();
builder.Services.AddSingleton<EmprestimoServiceV2>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Localização pt-BR
var supportedCultures = new[] { new CultureInfo("pt-BR") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseSwagger();

app.MapGroup("/v1").WithGroupName("v1").MapEndpoints();
app.MapGroup("/v2").WithGroupName("v2").MapEndpointsV2();

// Arquivos estáticos, se usar CSS
app.UseStaticFiles();

app.Run();
