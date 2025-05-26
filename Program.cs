using EmprestimosOpenAPI;
using EmprestimosOpenAPI.Examples;
using EmprestimosOpenAPI.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Swagger + OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.ExampleFilters();

    // Documentação v1
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

    // Documentação v2
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

    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (string.IsNullOrEmpty(apiDesc.GroupName)) return false;
        return apiDesc.GroupName.Equals(docName, StringComparison.OrdinalIgnoreCase);
    });
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<EmprestimoExample>();

// 🔹 Serviços por versão
builder.Services.AddSingleton<EmprestimoService>();
builder.Services.AddSingleton<EmprestimoServiceV2>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Empréstimos API V1");
    opt.SwaggerEndpoint("/swagger/v2/swagger.json", "Empréstimos API V2");
    opt.DocumentTitle = "Documentação da API de Empréstimos";
    opt.InjectStylesheet("/swagger-ui/custom.css");
    opt.RoutePrefix = string.Empty; // Acessível direto na raiz
});

app.UseStaticFiles();

// 🔹 Mapeia endpoints com grupo (usado no filtro do Swagger)
app.MapGroup("/v1").WithGroupName("v1").MapEndpoints();
app.MapGroup("/v2").WithGroupName("v2").MapV2Endpoints();

app.Run();
