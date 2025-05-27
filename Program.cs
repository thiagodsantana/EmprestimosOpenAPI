using EmprestimosOpenAPI;
using EmprestimosOpenAPI.Examples;
using EmprestimosOpenAPI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configura os serviços para uso de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Ativa anotações [SwaggerSchema], [SwaggerOperation] etc.
    options.EnableAnnotations();

    // Suporte para exemplos práticos de entrada/saída
    options.ExampleFilters();

    // Documentação da versão 1
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Gestão de Empréstimos",
        Version = "v1",
        Description = "API para gerenciamento de contratos de empréstimos.",
        Contact = new OpenApiContact
        {
            Name = "Equipe Suporte",
            Email = "suporte@emprestimos.com"
        }
    });

    // Documentação da versão 2 com funcionalidades extras
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "API de Gestão de Empréstimos - V2",
        Version = "v2",
        Description = "Versão 2 da API com cálculo de juros e status estendido.",
        Contact = new OpenApiContact
        {
            Name = "Equipe Suporte",
            Email = "suporte@emprestimos.com"
        }
    });

    // Garante que os endpoints sejam exibidos na versão correta no Swagger
    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (string.IsNullOrEmpty(apiDesc.GroupName)) return false;
        return apiDesc.GroupName.Equals(docName, StringComparison.OrdinalIgnoreCase);
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);
});


// Registra classes de exemplos para Swagger UI
builder.Services.AddSwaggerExamplesFromAssemblyOf<EmprestimoExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<EmprestimoExampleV2>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<AtualizacaoEmprestimoDtoExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ProblemDetailsExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ValidationProblemDetailsExample>();

// Registra serviços de negócio para as versões da API
builder.Services.AddSingleton<EmprestimoService>();
builder.Services.AddSingleton<EmprestimoServiceV2>();

var app = builder.Build();

// Define cultura padrão da API como pt-BR
var supportedCultures = new[] { new CultureInfo("pt-BR") };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Middleware de documentação Swagger
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Empréstimos API V1");
    opt.SwaggerEndpoint("/swagger/v2/swagger.json", "Empréstimos API V2");
    opt.DocumentTitle = "Documentação da API de Empréstimos";
    opt.InjectStylesheet("custom.css"); // Se quiser customizar o estilo visual
    opt.RoutePrefix = string.Empty; // Swagger UI na raiz
});

// Habilita arquivos estáticos (necessário para custom.css, por exemplo)
app.UseStaticFiles();

// Mapeia os endpoints organizando por grupo/versionamento (usado no Swagger)
app.MapGroup("/v1").WithGroupName("v1").MapEndpoints();
app.MapGroup("/v2").WithGroupName("v2").MapV2Endpoints();

// Inicia a aplicação
app.Run();
