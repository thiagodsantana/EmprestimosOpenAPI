using EmprestimosOpenAPI.Models;
using EmprestimosOpenAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosOpenAPI;

/// <summary>
/// Define os endpoints da versão 2 (v2) da API de Empréstimos.
/// Agrupado para documentação OpenAPI/Swagger com tag "v2".
/// </summary>
[ApiExplorerSettings(GroupName = "v2")]
public static class EndpointsV2
{
    public static RouteGroupBuilder MapV2Endpoints(this IEndpointRouteBuilder app)
    {
        // Agrupa todos os endpoints de empréstimos sob a rota base /emprestimos
        var group = app.MapGroup("/emprestimos");

        // 🔹 POST /emprestimos
        // Cria um novo empréstimo com cálculo automático de juros
        group.MapPost("/", ([FromServices] EmprestimoServiceV2 service, EmprestimoV2 emprestimo) =>
        {
            var criado = service.Criar(emprestimo);
            return Results.Created($"/emprestimos/{criado.Id}", criado);
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Cria um novo empréstimo com juros")
        .Produces<EmprestimoV2>(201) // Sucesso
        .Produces<ValidationProblemDetails>(400) // Dados inválidos
        .Produces<ProblemDetails>(500) // Erro inesperado
        .WithOpenApi();

        // 🔹 GET /emprestimos
        // Retorna a lista completa de empréstimos
        group.MapGet("/", ([FromServices] EmprestimoServiceV2 service) =>
        {
            var lista = service.ListarTodos();
            return Results.Ok(lista);
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Lista todos os empréstimos")
        .Produces<List<EmprestimoV2>>(200)
        .WithOpenApi();

        // 🔹 GET /emprestimos/{id}
        // Busca um empréstimo específico por ID
        group.MapGet("/{id:guid}", ([FromServices] EmprestimoServiceV2 service, Guid id) =>
        {
            var item = service.ObterPorId(id);
            return item is null ? Results.NotFound() : Results.Ok(item);
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Consulta empréstimo por ID")
        .Produces<EmprestimoV2>(200)
        .Produces(404)
        .WithOpenApi();

        // 🔹 PUT /emprestimos/{id}
        // Atualiza completamente os dados de um empréstimo existente
        group.MapPut("/{id:guid}", ([FromServices] EmprestimoServiceV2 service, Guid id, EmprestimoV2 novo) =>
        {
            var atualizado = service.Atualizar(id, novo);
            return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Atualiza empréstimo")
        .Produces<EmprestimoV2>(200)
        .Produces(404)
        .WithOpenApi();

        // 🔹 PATCH /emprestimos/{id}
        // Atualiza parcialmente os dados de um empréstimo (como status ou total a pagar)
        group.MapPatch("/{id:guid}", ([FromServices] EmprestimoServiceV2 service, Guid id, AtualizacaoEmprestimoDto patch) =>
        {
            var atualizado = service.AtualizarParcial(id, patch);
            return atualizado is null ? Results.NotFound() : Results.Ok(atualizado);
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Atualiza parcialmente")
        .Produces<EmprestimoV2>(200)
        .Produces(404)
        .WithOpenApi();

        // 🔹 DELETE /emprestimos/{id}
        // Remove um empréstimo do sistema
        group.MapDelete("/{id:guid}", ([FromServices] EmprestimoServiceV2 service, Guid id) =>
        {
            var sucesso = service.Remover(id);
            return sucesso ? Results.NoContent() : Results.NotFound();
        })
        .WithTags("Empréstimos V2")
        .WithSummary("Remove empréstimo")
        .Produces(204)
        .Produces(404)
        .WithOpenApi();

        return group;
    }
}
